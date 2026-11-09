using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 主页「补给」抽卡（GachaReq 4700）的服务端实现。
///
/// 卡池内容、UP 角色、掉落构成、概率、背景图 **全部来自 <c>run\gacha.json</c>**
/// （见 <see cref="GachaConfig"/>），这里只负责执行：
///   1. Purchase校验并扣除对应补给卡或水晶；
///   2. <see cref="Draw"/>：按配置的权重抽，落库；
///   3. 抽到的装备/碎片写进存档（UniqueId 用 AutoIncrement 发），重登也在。
///
/// 为什么「仓库满」曾经报个不停
/// ----------------------------
/// 客户端在发 GachaReq 之前会自己算一次：已持有装备件数 >= equipment_size_limit 就拦住，
/// 包都不发。这个存档是 1290 圣痕 + 1268 武器 = 2558 件，而服务端把 limit 写死 1000，
/// 于是永远「仓库满」。现在下限取配置里的 equipment_limit_base，并留 slack 个空位。
internal static partial class GachaService
{
	/// <summary>按 (kind, rarity) 缓存的随机池，懒加载自 StigmataData / WeaponData。</summary>
	private static readonly Dictionary<(string Kind, uint Rarity), uint[]> RarityPools =
		new Dictionary<(string Kind, uint Rarity), uint[]>();

	private static readonly Random Rng = new Random();

	/// <summary>
	/// 本次应下发的装备仓容量。
	/// 客户端只用它跟「已持有装备件数」比大小，用来判断能不能再抽卡，
	/// 所以按现持有量动态抬高 —— 始终留出 slack 个空位，抽卡永远不会被仓库拦住。
	/// </summary>
	internal static uint EquipmentSizeLimit(EquipmentScheme equipment)
	{
		GachaConfigFile cfg = GachaConfig.Current;
		uint owned = (uint)((equipment.StigmataList?.Length ?? 0) + (equipment.WeaponList?.Length ?? 0));
		return Math.Max(cfg.EquipmentLimitBase, owned + cfg.EquipmentLimitSlack);
	}

	/// <summary>
    /// 保留旧登录调用接口；正式扣费模式不再自动发放补给卡。
    /// </summary>
    internal static void EnsureTicket(Player player) { } // Compatibility call site; no automatic minting.

	/// <summary>
	/// 抽 <paramref name="times"/> 次。
	/// </summary>
	/// <param name="session">会话</param>
	/// <param name="times">次数，客户端单抽=1、十连=10</param>
	/// <param name="gachaType">请求里的 type，用来挑卡池</param>
	/// <returns>下发给客户端的 item_list；同时已写入存档。</returns>
    internal sealed class PurchaseError : Exception
    {
        internal GachaRsp.Retcode Code;
        internal PurchaseError(GachaRsp.Retcode code):base(code.ToString()){Code=code;}
    }
    internal static uint TicketFor(GachaPool pool) => pool.TicketMaterialId!=0 ? pool.TicketMaterialId : pool.GachaType switch
    { 5=>1103u,3=>1102u,7=>1107u,20=>1110u,2=>1101u,_=>GachaConfig.TicketMaterialId };
    internal static EquipmentScheme.GachaProgress Progress(EquipmentScheme equipment,uint type)
    {
        equipment.GachaProgressByType??=new();
        if(equipment.GachaProgressByType.TryGetValue(type,out var progress))return progress;
        return new EquipmentScheme.GachaProgress{Low=type==20||equipment.LegacyGachaProgressMigrated?0:equipment.GachaGuaranteeCounter,Up=type==20||equipment.LegacyGachaProgressMigrated?0:equipment.GachaUpGuaranteeCounter};
    }
    internal static string Banner(GachaPool pool)=>pool.GachaId+":"+(string.IsNullOrWhiteSpace(pool.PeriodKey)?"":pool.PeriodKey+":")+string.Join(",",pool.Drops.Where(x=>x.Featured).Select(x=>x.Id).Distinct().OrderBy(x=>x));
    internal static bool SameBanner(GachaPool pool,string saved)
    {
        if(saved==Banner(pool))return true;
        // Migrate the existing phase0 without discarding progress on first enabling rotation.
        string legacy=pool.GachaId+":"+string.Join(",",pool.Drops.Where(x=>x.Featured).Select(x=>x.Id).Distinct().OrderBy(x=>x));
        return !string.IsNullOrEmpty(pool.RotationGroup) && pool.RotationSlot==0 && saved==legacy;
    }
    internal static EquipmentScheme.GachaProgress PoolProgress(EquipmentScheme equipment,GachaPool pool)
    {
        var saved=Progress(equipment,pool.GachaType);
        if(saved.Banner.Length>0 && !SameBanner(pool,saved.Banner))
        {
            if(pool.CollectUpEquipment)return new EquipmentScheme.GachaProgress{Low=saved.Low,Total=saved.Total};
            // Expansion ten-pull guarantee is per banner, while S protection carries over.
            // Apply before displaying the NEXT draw, also when no fragment-energy feature is configured.
            if(pool.GachaType==5)return new EquipmentScheme.GachaProgress{Banner=saved.Banner,Low=0,Up=saved.Up,Total=saved.Total};
        }
        return saved;
    }
    internal static uint DisplayEnergy(EquipmentScheme equipment,GachaPool pool)
    {
        var progress=Progress(equipment,pool.GachaType);
        return progress.Banner.Length>0 && !SameBanner(pool,progress.Banner)?0:equipment.GachaExEnergy;
    }
    internal static List<GachaItem> Purchase(Session session,GachaReq req)
    {
        var player=session.Player;
        lock(player.Equipment)
        {
            if(req.Num!=1 && req.Num!=10)throw new PurchaseError(GachaRsp.Retcode.MaxTimesLimit);
            var pool=GachaConfig.Pool((uint)req.Type);
            if(pool==null || pool.Drops.Count==0)throw new PurchaseError(GachaRsp.Retcode.GachaClosed);
            if(!GachaRotation.CurrentDisplay(session,pool))throw new PurchaseError(GachaRsp.Retcode.GachaClosed);
            if(req.IsUseFreeGacha)throw new PurchaseError(GachaRsp.Retcode.FreeTypeMismatch);
            if(!GachaConfig.GrantAvatar && pool.Drops.Any(x=>string.Equals(x.Kind,"avatarCard",StringComparison.OrdinalIgnoreCase)))throw new PurchaseError(GachaRsp.Retcode.FeatureClosed);
            uint ticket=TicketFor(pool);long cost=(long)pool.TicketHcoinCost*req.Num;
            var materials=player.Equipment.MaterialList.Where(x=>x.Id==ticket).ToArray();
            if(req.IsUseHcoin && (cost<=0 || cost>int.MaxValue || player.User.Hcoin<cost))throw new PurchaseError(GachaRsp.Retcode.HcoinLack);
            // 单机版：补给卡锁定 999，等于无限 —— 这类卡不做数量校验也不真扣。
            bool freeTicket=GmLocks.IsTicket(ticket);
            if(!req.IsUseHcoin && !freeTicket && materials.Sum(x=>(long)x.Num)<req.Num)throw new PurchaseError(GachaRsp.Retcode.TicketLack);
            _=Common.Database.User.collection;_=Common.Database.Avatar.collection;_=AutoIncrement.collection;_=GachaRecords.collection;
            if(pool.GachaType==20)_=DormSupply.Load(player.User.Uid);
            string equipment=JsonConvert.SerializeObject(player.Equipment),avatars=JsonConvert.SerializeObject(player.AvatarList);
            int hcoin=player.User.Hcoin;
            try
            {
                List<GachaItem> result=null;
                Global.Store.InTransaction(()=>
                {
                    if(req.IsUseHcoin)player.User.Hcoin-=(int)cost;
                    else if(!freeTicket) {uint remaining=req.Num;foreach(var material in materials){uint debit=Math.Min(remaining,material.Num);material.Num-=debit;remaining-=debit;}}
                    result=DrawFrozen(session,req.Num,(uint)req.Type,pool);
                    player.Equipment.Save();if(req.IsUseHcoin)player.User.Save();
                });
                return result;
            }
            catch
            {
                player.Equipment=JsonConvert.DeserializeObject<EquipmentScheme>(equipment);player.AvatarList=JsonConvert.DeserializeObject<AvatarScheme[]>(avatars);player.User.Hcoin=hcoin;throw;
            }
        }
    }
    internal static List<GachaItem> Draw(Session session,uint times,uint gachaType)=>DrawFrozen(session,times,gachaType,null);
    private static List<GachaItem> DrawFrozen(Session session,uint times,uint gachaType,GachaPool snapshotPool)
    {
        var player=session.Player;
        lock(player.Equipment)
        {
            if(times!=1 && times!=10)throw new InvalidOperationException("Only one or ten draws are supported");
            // Initialize schemas outside the encompassing transaction, including UID allocation.
            _=Common.Database.Avatar.collection;_=AutoIncrement.collection;_=GachaRecords.collection;
            if(gachaType==20)_=DormSupply.Load(player.User.Uid);
            var equipment=JsonConvert.SerializeObject(player.Equipment);
            var avatars=JsonConvert.SerializeObject(player.AvatarList);
            try
            {
                List<GachaItem> result=null;
                Global.Store.InTransaction(()=>{result=DrawCore(session,times,gachaType,snapshotPool);if(result.Count!=times)throw new InvalidOperationException("Incomplete draw");player.Equipment.Save();});
                return result;
            }
            catch
            {
                player.Equipment=JsonConvert.DeserializeObject<EquipmentScheme>(equipment);
                player.AvatarList=JsonConvert.DeserializeObject<AvatarScheme[]>(avatars);
                throw;
            }
        }
    }
    private static List<GachaItem> DrawCore(Session session,uint times,uint gachaType,GachaPool snapshotPool)
    {
		Times(ref times);

		List<GachaItem> result = new List<GachaItem>();
		EquipmentScheme equipment = session.Player.Equipment;

		GachaPool? pool = snapshotPool ?? GachaConfig.Pool(gachaType);
		if (pool == null || pool.Drops.Count == 0)
		{
			Packet.c.Warn($"[Gacha] 没有可用的卡池（type={gachaType}），检查 run\\gacha.json");
			return result;
		}

        if(gachaType==20)return DrawDorm(session,times,pool);
		int total = 0;
		foreach (GachaDrop d in pool.Drops)
		{
			total = checked(total + Math.Max(0, d.Weight));
		}
		if (total <= 0)
		{
			Packet.c.Warn($"[Gacha] 卡池 #{pool.Id} 的 drops 权重全是 0");
			return result;
		}

		// 两档保底：
		//   低档 pityPool  = 只认 pity（角色池 = 4 个 A 级；装备池 = 本期全部 5★）
		//   高档 upPool    = 只认 featured（本期 UP）
		// 副产物两档都不进，所以保底永远不会出到它们身上。
		// 同一抽两档都触发时 UP 优先 —— 否则低档先满足会把高档进度白白推掉。
		//
		// 摇号池 ≠ 清零条件：
		//   摇号池只放 pity，所以「每 10 抽必出 A 级角色」摇出来的就是 A 级角色，
		//   不会摇成 UP 的 S 级碎片（UP 有自己那档 100 抽）。
		//   但清零条件仍含 featured —— 自然抽到 S 级也算达成了「A 级及以上」。
		List<GachaDrop> pityPool = pool.Drops.Where(d => d.Pity).ToList();
		if (pityPool.Count == 0)
		{
			// 配置里一个 pity 都没打时退化到 featured，别让保底静默失效
			pityPool = pool.Drops.Where(d => d.Featured).ToList();
		}
		List<GachaDrop> upPool = pool.Drops.Where(d => d.Featured).ToList();
		uint pity = pool.GuaranteeCount;
		uint upPity = pool.GuaranteeUpCount;
		bool pityOn = pity > 0u && pityPool.Count > 0;
		bool upPityOn = upPity > 0u && upPool.Count > 0;
		var progress=PoolProgress(equipment,pool);
        string banner=Banner(pool);
        if(progress.Banner.Length>0 && !SameBanner(pool,progress.Banner) && pool.ExId!=0) {equipment.GachaExEnergy=0;progress.ExFragments=0;progress.Low=0;}
        progress.Banner=banner;
        progress.ObtainedUpItems??=new();
        progress.LastEnergyAdd=0;progress.LastFragmentAdd=0;
        equipment.GachaProgressByType[gachaType]=progress;
        equipment.LegacyGachaProgressMigrated=true;
        uint counter=progress.Low;
        uint upCounter=progress.Up;
		
		int pityHit = 0;
		int upPityHit = 0;

		for (uint i = 0; i < times; i++)
		{
			var missing=pool.CollectUpEquipment?upPool.Where(x=>!progress.ObtainedUpItems.Contains(x.Id)).ToList():upPool;
            bool highActive=upPityOn && missing.Count>0;
            bool forceUp = highActive && upCounter + 1u >= upPity;
			bool forcePity = pityOn && counter + 1u >= pity;

			GachaDrop drop;
			if (forceUp)
			{
				drop = RollWeighted(missing);
				upPityHit++;
			}
			else
            {
                drop=Roll(pool.Drops,total);
                if(forcePity && !drop.Pity && !drop.Featured){drop=RollWeighted(pityPool);pityHit++;}
            }

			if (TryGrant(drop, out GachaItem? item) && item != null)
			{
				GrantInventory(session.Player,drop,item);
                ConvertDuplicate(session.Player,drop,item,pool);
                GrantGift(session.Player,pool,item);
                result.Add(item);
                GachaRecords.Append(session.Player.User.Uid,gachaType,item);
			}

			if(item==null)throw new InvalidOperationException("Unresolvable gacha reward");

			// 抽到角色卡 → 真的把女武神解锁掉。
			// 只展示的话客户端永远拿不到「新角色」，结算页也就没有解锁动画
			// （客户端靠 isNew 决定走 3D 登场动画还是 2D 卡面）。
			UnlockAvatar(session.Player, drop);

			// 高档：只有 UP 能清
			if (drop.Featured && (!pool.CollectUpEquipment || !progress.ObtainedUpItems.Contains(drop.Id)))
            {
                if(pool.CollectUpEquipment)progress.ObtainedUpItems.Add(drop.Id);
                upCounter = 0u;
			}
			else if (highActive)
			{
				upCounter++;
			}

			// 低档：UP 也算（S 级 ≥ A 级）
			if (drop.Pity || drop.Featured)
			{
				counter = 0u;
			}
			else if (pityOn)
			{
				counter++;
			}
		}
		progress.Low=counter;progress.Up=upCounter;progress.Total=checked(progress.Total+times);
        equipment.GachaGuaranteeCounter = counter;
		equipment.GachaUpGuaranteeCounter = upCounter;

        // 5.9+ expansion: every draw grants 50 secondary energy; 210 converts to one UP fragment.
        if(pool.ExId!=0)
        {
            var owner=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().FromId((int)pool.UpAvatarId);
            if(owner==null || pool.ExEnergyNeed==0)throw new InvalidOperationException("Invalid expansion energy target");
            uint add=checked(times*pool.ExEnergyPerDraw);
            uint energy=checked(equipment.GachaExEnergy+add);
            uint fragments=energy/pool.ExEnergyNeed;
            equipment.GachaExEnergy=energy%pool.ExEnergyNeed;
            progress.LastEnergyAdd=add;progress.LastFragmentAdd=fragments;
            progress.ExFragments=checked(progress.ExFragments+fragments);
            if(fragments>0)GrantInventory(session.Player,new GachaDrop{Kind="avatarFragment",Id=(uint)owner.AvatarFragmentId},new GachaItem{ItemId=(uint)owner.AvatarFragmentId,Num=fragments});
        }

		Packet.c.Log($"[Gacha] uid={session.Player.User.Uid} 池#{pool.Id}(type={gachaType}) 抽 {times} 次 -> {result.Count} 件"
			+ $" ids=[{string.Join(",", result.Select(x => x.ItemId))}]"
			+ (pool.ExId != 0u ? $" 碎片 {equipment.GachaExEnergy}/{pool.ExEnergyNeed}" : "")
			+ (pityOn ? $" 低档保底 {counter}/{pity}" + (pityHit > 0 ? $"（触发 {pityHit} 次）" : "") : "")
			+ (upPityOn ? $" UP保底 {upCounter}/{upPity}" + (upPityHit > 0 ? $"（触发 {upPityHit} 次）" : "") : ""));

		return result;
	}

	/// <summary>
	/// 拼抽卡结算里的碎片信息（<c>GachaRsp.ex_fragment</c>）。
	/// 不填这个字段的话，结算页不会出现「获得碎片」，补给页的 0/210 也永远不动。
	/// </summary>
	internal static GachaExFragment? ExFragment(EquipmentScheme equipment, uint energyAdd, GachaPool? pool)
	{
		if (pool == null || pool.ExId == 0u)
		{
			return null;
		}

		var progress=PoolProgress(equipment,pool);
        energyAdd=progress.LastEnergyAdd;
        uint energy = equipment.GachaExEnergy;
		uint need = pool.ExEnergyNeed != 0u ? pool.ExEnergyNeed : 210u;
		return new GachaExFragment
		{
			ExId = pool.ExId,
			ExEnergy = energy,
			// 已经能兑换几次
			ExNum = progress.ExFragments,
			ExEnergyAdd = energyAdd,
			// 这次实际到手的碎片数
			FragmentAdd = progress.LastFragmentAdd,
			ExEnergyAddOrig = energyAdd,
			ExEnergyAddBonus = 0u,
			AvatarId = pool.UpAvatarId
		};
	}

	/// <summary>
	/// 要塞进 <c>display_ext</c> 的 JSON。
	///
	/// 补给页中间那块**不是静态图，是 CG 视频**：
	/// <c>video_cg_key</c> → <c>GachaCGBGCmpt.TryPlayCg</c> → 按 <c>CgExtraKey</c> 在
	/// <c>CgData</c> 里找行 → 播 <c>StreamingAssets/Video/&lt;CgPath&gt;.usm</c>。
	/// 这一项不给，客户端会回落到 <c>ConfigGacha.GachaVideoDict[gacha_type]</c> 的内置 key，
	/// 而官服那些 key 指向的 .usm 只在 CDN 上，私服拿不到 → 中间一直是「加载中....」。
	///
	/// <c>common_gacha_bg</c> 只是视频底下的静帧，视频没起来时才露出来；
	/// 它取不到图会显示占位图 <c>SpriteOutput/Gacha/CaChaNew01/GachaBg/GachaCurrency</c>。
	/// </summary>
	internal static string DisplayExt(GachaPool pool)
	{
		List<string> parts = new List<string>(2);
		if (!string.IsNullOrWhiteSpace(pool.BgImage))
		{
			parts.Add($"\"common_gacha_bg\":\"{pool.BgImage}\"");
		}
		if (!string.IsNullOrWhiteSpace(pool.VideoCgKey))
		{
			parts.Add($"\"video_cg_key\":\"{pool.VideoCgKey}\"");
		}
		return parts.Count == 0 ? "{}" : "{" + string.Join(",", parts) + "}";
	}

	/// <summary>修正请求次数：客户端只会发 1 或 10，但别信它。</summary>
	private static void Times(ref uint times)
	{
		if (times == 0u)
		{
			times = 1u;
		}
		if (times > 10u)
		{
			times = 10u;
		}
	}

	/// <summary>按权重摇一条掉落规则。</summary>
	private static GachaDrop RollWeighted(List<GachaDrop> drops)
	{
		int total = 0;
		foreach (GachaDrop d in drops)
		{
			total = checked(total + Math.Max(0, d.Weight));
		}
		return total <= 0 ? drops[0] : Roll(drops, total);
	}

	/// <summary>按权重摇一条掉落规则。</summary>
	private static GachaDrop Roll(List<GachaDrop> drops, int total)
	{
		int pick;lock(Rng)pick = Rng.Next(total);
		foreach (GachaDrop d in drops)
		{
			int w = Math.Max(0, d.Weight);
			if (pick < w)
			{
				return d;
			}
			pick -= w;
		}
		return drops[drops.Count - 1];
	}

	/// <summary>
	/// 把一条掉落规则变成下发给客户端的 <see cref="GachaItem"/>。
	///
	/// 此方法只生成展示对象；真正入库由GrantInventory在同一事务内完成。
	/// 展示、物品入库和 <c>GachaExEnergy</c>（抽卡碎片进度），
	/// 由 <see cref="Draw"/> 负责落盘。
	///
	/// 但仍然要校验 id 在对应表里存在 —— 客户端拿到表里没有的 id 会在渲染时炸，
	/// 而且配置写错（比如 typo 成 5 位数）时应该报日志而不是静默出一张空卡。
	/// </summary>
	private static bool TryGrant(GachaDrop drop, out GachaItem? item)
	{
		item = null;
		string kind = (drop.Kind ?? "").Trim().ToLowerInvariant();
		uint num = Math.Max(1u, drop.Num);
		uint id = drop.Id;

		if (id == 0u && (kind == "stigmata" || kind == "weapon"))
		{
			id = PickByRarity(kind, drop.Rarity);
		}
		if (id == 0u)
		{
			Packet.c.Warn($"[Gacha] 掉落解析不出 id：kind={drop.Kind} id={drop.Id} rarity={drop.Rarity}");
			return false;
		}

		if (!Exists(kind, id))
		{
			Packet.c.Warn($"[Gacha] 掉落 id={id}（kind={kind}）不在配置表里，跳过");
			return false;
		}

		item = new GachaItem
		{
			ItemId = id,
			Level = (kind=="weapon" || kind=="stigmata") && (drop.Pity || drop.Featured)?10u:1u,
			Num = num,
			IsRareDrop = IsRare(kind, id)
		};
		return true;
	}

	/// <summary>
	/// 抽到角色卡时解锁对应女武神。
	///
	/// 为什么必须真解锁
	/// ----------------
	/// 客户端结算页用 <c>isNew</c> 决定画「3D 角色登场动画」还是「2D 卡面」
	/// （<c>GachaSequencePageContext.GetMainItemDisplayType</c> → UIManager3D / Dialog2D）。
	/// 只发 <see cref="GachaItem"/> 而角色不在 <c>AvatarList</c> 里，客户端永远拿不到「新角色」，
	/// 所以卡面能画出来、但没有解锁动画。
	///
	/// **不进仓库**：这里调 <c>Avatar.Create(..., grantInitialWeapon: false)</c>，
	/// 不塞初始武器、<c>WeaponUniqueId</c> 留 0，所以 WeaponList / StigmataList 一件都不会多。
	/// 重复卡由ConvertDuplicate转换后，已有角色跳过解锁。
	/// 由 <c>gacha.json</c> 的 <c>grant_avatar</c> 控制，关掉就退回「只展示」。
	/// </summary>
	private static void UnlockAvatar(Player player, GachaDrop drop)
	{
		if (!GachaConfig.GrantAvatar)
		{
			return;
		}
		if (!string.Equals((drop.Kind ?? "").Trim(), "avatarCard", StringComparison.OrdinalIgnoreCase))
		{
			return;
		}
		AvatarDataExcel? row = FindByCardId((int)drop.Id);
		if (row == null)
		{
			return;
		}
        var existing=player.AvatarList?.FirstOrDefault(a=>a.AvatarId==(uint)row.AvatarId);
        if(existing!=null)
        {
            if(existing.Star>0)return;
            existing.Star=(uint)row.UnlockStar;
            existing.Level=Math.Max(1u,existing.Level);
            existing.DressId=(uint)row.DefaultDressId;
            existing.DressLists=(existing.DressLists??Array.Empty<uint>()).Append((uint)row.DefaultDressId).Distinct().ToArray();
            existing.SkillLists??=new();
            foreach(int skill in row.SkillList??Array.Empty<int>())
                if(!existing.SkillLists.Any(x=>x.SkillId==(uint)skill))existing.SkillLists.Add(new AvatarScheme.AvatarSkill{SkillId=(uint)skill});
            existing.Save();
            return;
        }

		// Avatar 有歧义（Common.Database.Avatar / Common.Resources.Proto.Avatar），写全名
		AvatarScheme created = Common.Database.Avatar.Create(row.AvatarId, player.User.Uid, player.Equipment, false);
		player.AvatarList = (player.AvatarList ?? Array.Empty<AvatarScheme>()).Append(created).ToArray();
		player.Equipment.Save();
		Packet.c.Log($"[Gacha] 解锁女武神 {row.AvatarId}（uid={player.User.Uid}）");
	}

    private static void ConvertDuplicate(Player player,GachaDrop drop,GachaItem item,GachaPool pool)
    {
        if(!string.Equals(drop.Kind,"avatarCard",StringComparison.OrdinalIgnoreCase))return;
        var row=FindByCardId((int)item.ItemId)??throw new InvalidOperationException("Unknown card");
        var avatar=player.AvatarList?.FirstOrDefault(x=>x.AvatarId==(uint)row.AvatarId);
        if(avatar==null || avatar.Star==0)return;
        uint perCard=row.UnlockStar switch{1=>7u,2=>row.AvatarType switch{1=>30u,2=>30u,_=>18u},3=>row.AvatarType==1?70u:30u,_=>throw new InvalidOperationException("Unknown duplicate rank")};
        uint count=checked(perCard*item.Num);
        uint id=(uint)row.AvatarFragmentId;
        string kind="avatarFragment";
        // Expansion token mode: max-rank initial A cards become Battlesuit Shards (material998).
        if(pool.GachaType==5 && avatar.Star>=5 && row.UnlockStar==2 && row.AvatarType!=2)
        {id=998;count=checked(count*(row.AvatarType==1?10u:15u));kind="material";}
        GrantInventory(player,new GachaDrop{Kind=kind,Id=id},new GachaItem{ItemId=id,Num=count});
        if(kind=="avatarFragment")item.SplitFragmentNum=count;
        else {item.IsClientDisplayTransfer=true;item.TransferItemLists.Add(new GenericItemNum{Id=id,Num=count,Level=1});}
    }
    private static void GrantGift(Player player,GachaPool pool,GachaItem main)
    {
        if(pool.GiftDrops==null || pool.GiftDrops.Count==0)return;
        var candidates=pool.GiftDrops.Where(x=>x.Weight>0).ToList();
        if(candidates.Count==0 || candidates.Any(x=>!string.Equals(x.Kind,"material",StringComparison.OrdinalIgnoreCase) || x.Id==0 || x.Num==0))throw new InvalidOperationException("Invalid independent gift catalogue");
        var drop=RollWeighted(candidates);
        if(!TryGrant(drop,out var gift) || gift==null)throw new InvalidOperationException("Invalid gift reward");
        var config=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId(gift.ItemId);
        long held=player.Equipment.MaterialList.Where(x=>x.Id==gift.ItemId).Sum(x=>(long)x.Num);
        long cap=config.QuantityLimit>0?Math.Min(config.QuantityLimit,int.MaxValue):int.MaxValue;
        if(held+gift.Num>cap)throw new InvalidOperationException("Gift material limit");
        GrantInventory(player,drop,gift);
        main.GiftItemId=gift.ItemId;main.GiftLevel=gift.Level;main.GiftNum=gift.Num;
    }
    private static void GrantInventory(Player player,GachaDrop drop,GachaItem item)
    {
        string kind=(drop.Kind??"").Trim().ToLowerInvariant();
        if(item.Num==0 || item.Num>int.MaxValue)throw new InvalidOperationException("Invalid reward count");
        switch(kind)
        {
            case "material":
                long current=player.Equipment.MaterialList.Where(x=>x.Id==item.ItemId).Sum(x=>(long)x.Num);
                if(current+item.Num>uint.MaxValue)throw new InvalidOperationException("Material overflow");
                player.Equipment.AddMaterial((int)item.ItemId,(int)item.Num);break;
            case "weapon":for(uint i=0;i<item.Num;i++){player.Equipment.AddWeapon((int)item.ItemId);player.Equipment.WeaponList.Last().Level=item.Level;}break;
            case "stigmata":for(uint i=0;i<item.Num;i++){player.Equipment.AddStigmata((int)item.ItemId);player.Equipment.StigmataList.Last().Level=item.Level;}break;
            case "avatarfragment":
                var row=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All.FirstOrDefault(x=>x.AvatarFragmentId==item.ItemId);
                if(row==null)throw new InvalidOperationException("Unknown fragment owner");
                var avatar=player.AvatarList?.FirstOrDefault(x=>x.AvatarId==(uint)row.AvatarId);
                bool create=avatar==null;
                if(create)avatar=new AvatarScheme{OwnerUid=player.User.Uid,AvatarId=(uint)row.AvatarId,Star=0,Level=1,SkillLists=new(),DressLists=Array.Empty<uint>()};
                if((ulong)avatar.Fragment+item.Num>uint.MaxValue)throw new InvalidOperationException("Fragment overflow");
                avatar.Fragment+=item.Num;
                if(create){Common.Database.Avatar.collection.InsertOne(avatar);player.AvatarList=(player.AvatarList??Array.Empty<AvatarScheme>()).Append(avatar).ToArray();}
                else avatar.Save();
                break;
            case "avatarcard":if(item.Num!=1)throw new InvalidOperationException("A card draw must contain exactly one card");break; // Existing character unlock path runs in the same transaction.
            default:throw new InvalidOperationException("Unsupported gacha inventory kind: "+kind);
        }
    }
	/// <summary>id 是否在对应的配置表里（只校验，不读其他字段）。</summary>
	private static bool Exists(string kind, uint id)
	{
		int key = (int)id;
		switch (kind)
		{
			case "stigmata":
				return BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().FromId(key) != null;
			case "weapon":
				return BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().FromId(key) != null;
			case "avatar":
				return BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().FromId(key) != null;
			case "avatarcard":
				// 角色卡：id 是 AvatarData.avatarCardID，落在 AvatarCardData 表里。
				// 客户端靠这张表把掉落画成「角色卡」，所以必须校验它在。
				return FindByCardId(key) != null;
			case "avatarfragment":
				return BaseExcelReader<AvatarFragmentData, AvatarFragmentDataExcel>.GetInstance().FromId(key) != null;
			case "material":
				// MaterialData.FromId 的入参是 uint，别的读取器是 int
				return BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId(id) != null;
			default:
				// 未知 kind 一律放行（当材料处理），只在 id 解析不出来时才拦
				return false;
		}
	}

	/// <summary>
	/// 按角色卡 id 反查角色行。
	///
	/// 抽卡掉的是 <c>AvatarData.avatarCardID</c>（60102 这种），不是 avatarID ——
	/// 客户端只有拿到角色卡 id 才会把它画成角色卡；给 avatarID（102）时
	/// 那张 id 在任何表里都不存在，客户端只能退化成默认道具（看起来是 4★ 物品）。
	/// </summary>
	private static AvatarDataExcel? FindByCardId(int cardId)
	{
		return BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All
			.FirstOrDefault((AvatarDataExcel a) => a.AvatarCardId == cardId);
	}

	/// <summary>按 (kind, rarity) 从表里随机取一个 id；同 (kind,rarity) 的池只建一次。</summary>
	private static uint PickByRarity(string kind, uint rarity)
	{
		(string Kind, uint Rarity) key = (kind, rarity);
		if (!RarityPools.TryGetValue(key, out uint[]? pool))
		{
			IEnumerable<(int Id, int Rarity)> rows = kind == "weapon"
				? BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().All
					.Select((WeaponDataExcel row) => (row.Id, row.Rarity))
				: BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().All
					.Select((StigmataDataExcel row) => (row.Id, row.Rarity));

			List<int> ids = rows.Where(x => x.Id > 0 && (rarity == 0u || x.Rarity == (int)rarity))
				.Select(x => x.Id).ToList();
			if (ids.Count == 0)
			{
				// 这一档没有数据（或 rarity 写了表里不存在的值），退化成全表，别让卡池抽空。
				ids = rows.Where(x => x.Id > 0).Select(x => x.Id).ToList();
			}
			pool = ids.Select(x => (uint)x).ToArray();
			RarityPools[key] = pool;
		}

		if (pool.Length == 0)
		{
			return 0u;
		}
		lock(Rng)return pool[Rng.Next(pool.Length)];
	}

	/// <summary>
	/// 稀有掉落 → 客户端走彩光/特写。
	///
	/// 判据一律取自表里的品级字段，不写死 id、也不靠 id 段猜：
	///   avatar      → <c>AvatarData.unlockStar &gt;= 3</c>（3 = S 级女武神）
	///   avatarFragment → <c>AvatarFragmentData.GachaDisplayRarity &gt;= 15</c>
	///                    （15 = S 级碎片，A 级是 4、B 级是 3）
	///   weapon      → <c>WeaponData.maxRarity &gt;= 5</c>
	///   stigmata    → <c>StigmataData.maxRarity &gt;= 5</c>
	///
	/// 必须按 kind 分派而不是按 id 段：材料 id 有 1103、3316 这种三位/四位数，
	/// 单看 id 段会被误判成角色或碎片。
	/// </summary>
	private static bool IsRare(string kind, uint itemId)
	{
		int key = (int)itemId;
		switch (kind)
		{
			case "avatar":
			{
				AvatarDataExcel? avatar = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().FromId(key);
				return avatar != null && avatar.UnlockStar >= 3;
			}
			case "avatarcard":
			{
				// 角色卡走彩光 = 该角色是 S 级（unlockStar 3）。
				// 不用 AvatarCardData.GachaDisplayRarity，因为那张表还没建读取器，
				// 而 AvatarData 已经读进来了，两者在 90 个角色上是等价的（15↔3、4↔2、3↔1）。
				AvatarDataExcel? owner = FindByCardId(key);
				return owner != null && owner.UnlockStar >= 3;
			}
			case "avatarfragment":
			{
				AvatarFragmentDataExcel? fragment =
					BaseExcelReader<AvatarFragmentData, AvatarFragmentDataExcel>.GetInstance().FromId(key);
				return fragment != null && fragment.GachaDisplayRarity >= 15;
			}
			case "weapon":
			{
				WeaponDataExcel? weapon = BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().FromId(key);
				return weapon != null && weapon.MaxRarity >= 5;
			}
			case "stigmata":
			{
				StigmataDataExcel? row = BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().FromId(key);
				return row != null && row.MaxRarity >= 5;
			}
			default:
				return false;
		}
	}
}
