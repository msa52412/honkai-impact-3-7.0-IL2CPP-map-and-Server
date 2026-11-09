using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetGachaDisplayReq)]
internal class GetGachaDisplayReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		var display=Build(session.Player.Equipment);GachaRotation.Remember(session,display);session.Send(Packet.FromProto(display, CmdId.GetGachaDisplayRsp));
	}

	/// <summary>
	/// 构造补给页展示数据 —— 卡池列表全部来自 <c>run\gacha.json</c>。
	///
	/// GachaRsp(4701) 里也有一个同结构的 <c>display</c> 字段
	/// （抽完卡要让客户端刷新剩余次数 / 保底进度 / 碎片进度），所以抽成静态方法两边共用。
	/// 每次进来都重新读配置，改完 gacha.json 在客户端退出补给页再进就能看到。
	/// </summary>
	internal static GetGachaDisplayRsp Build(EquipmentScheme equipment)
	{
		GetGachaDisplayRsp getGachaDisplayRsp = new GetGachaDisplayRsp
		{
			retcode = GetGachaDisplayRsp.Retcode.Succ,
			IsAll = true,
			Type = GachaType.GachaTypeError,
			GachaRandom = (uint)Global.GetUnixInSeconds()
		};

		List<GachaPool> pools = GachaConfig.ActivePools;
		if (pools.Count == 0)
		{
			Packet.c.Warn("[Gacha] gacha.json 里没有启用的卡池，补给页会是空的");
			return getGachaDisplayRsp;
		}

		foreach (GachaPool pool in pools)
		{
			getGachaDisplayRsp.GachaDisplayInfoLists.Add(BuildOne(pool, equipment));
		}

		return getGachaDisplayRsp;
	}

	private static GachaDisplayInfo BuildOne(GachaPool pool, EquipmentScheme equipment)
	{
		GachaDisplayCommonData commonData = new GachaDisplayCommonData
		{
			// TitleImage 只是卡池标题旁的小图标，走 SpriteOutput 同步图集；
			// 中间那张大背景是 display_ext.common_gacha_bg（见 GachaService.DisplayExt）。
			TitleImage = pool.TitleImage,
			Title = pool.Title,
			Content = pool.Content,
			IsEnablePrompt = true,
			GachaId = pool.GachaId,
			DataBeginTime = GachaRotation.Window(pool).Begin,
			DataEndTime = GachaRotation.Window(pool).End,
			DisplayExt = GachaService.DisplayExt(pool)
		};

		// 只下发当期 UP 的那一个角色。
		// 之前把 avatarId < 9000 的 94 个全塞进来，客户端要为每个角色加载立绘/模型，
		// 一直加载不完 —— 补给页中间那行「加载中....」有一半原因在这里。
		if (pool.UpAvatarId != 0u)
		{
			commonData.UpAvatarLists = new[] { pool.UpAvatarId };
		}

        if(pool.GachaType==20)return new GachaDisplayInfo{GachaType=(GachaType)20,CommonData=commonData,AdventureGachaData=DormSupply.Display(equipment,pool)};

		CustomGachaData customGachaData = new CustomGachaData
		{
			TicketHcoinCost = pool.TicketHcoinCost,
			TicketMaterialId = GachaService.TicketFor(pool),
			IsEnableBaodi = true,
			GachaType = (GachaType)pool.GachaType,
			GachaTimes = GachaService.PoolProgress(equipment,pool).Total,
			DisplayMaxTimes = pool.CollectUpEquipment?pool.GuaranteeUpCount:pool.DisplayMaxTimes,
			NoProtectGachaTimes = GachaService.PoolProgress(equipment,pool).Up,
			// 补给页 CG 视频按哪个角色出；0 会让客户端自己去猜，直接给 UP 角色。
			DisplayVideoAvatar = pool.DisplayVideoAvatar != 0u ? pool.DisplayVideoAvatar : pool.UpAvatarId,
			ShiningType = 1u,
			// 碎片兑换活动；0 的话客户端直接跳过整块碎片逻辑（KEHFPGLKCDA 里 ex_id==0 就 return）。
			ExId = pool.ExId,
			// 当前碎片数 —— 补给页右上角「0/210」的分子。
			ExEnergy = GachaService.DisplayEnergy(equipment,pool),
			ExGachaNum = 0u
		};

		if (pool.ExId != 0u)
		{
			customGachaData.ProtectDisplayInfo = new GachaProtectDisplayInfo
			{
				NoProtectGachaTimes = GachaService.PoolProgress(equipment,pool).Up,
				// 碎片兑换的产物。客户端在 DGBDBGIBHIK 构造里拿它去 TryGetAvatarByID()，
				// 传 0 就取不到角色 → 界面上那个兑换结果只能画成一个问号「? x1」。
				DisplayKeyAvatar = pool.UpAvatarId,
				protect_display_type = GachaProtectDisplayInfo.ProtectDisplayType.NoDisplay
			};
		}

		// 精准补给（type=3）走的是「水晶池」分支，客户端读的是 hcoin_gacha_data，
		// 不是 custom_gacha_data：
		//
		//   HPGEGKBGMOA.BLGGEJBGHEB @ 0x18054E500：
		//     if (info.gacha_type == 30) this->special = info.special_gacha_data;
		//     else                       this->hcoin   = info.hcoin_gacha_data;
		//                                ^^^^^^^^^^^ 为 null 直接抛 NullReferenceException
		//
		// 抛异常的位置是 HCHDIBFGCAL.OnGetGachaDisplayRsp 里，异常一抛整条刷新链就断了，
		// 补给页永远停在转圈。type=5（自定义角色池）走的是 custom 分支，所以之前一直正常。
		//
		// 两个字段都填，客户端按 gacha_type 自己挑，谁读都有值。
		HcoinGachaData hcoinGachaData = new HcoinGachaData
		{
			TicketHcoinCost = pool.TicketHcoinCost,
			TicketMaterialId = GachaService.TicketFor(pool),
			ShiningType = 1u,
            WishWellData=GachaTransactions.WishDisplay(equipment,pool),
			ProtectLeftTimes=pool.CollectUpEquipment?System.Math.Max(1u,pool.GuaranteeUpCount-System.Math.Min(pool.GuaranteeUpCount,GachaService.PoolProgress(equipment,pool).Up)):System.Math.Max(1u,pool.GuaranteeCount-System.Math.Min(pool.GuaranteeCount,GachaService.PoolProgress(equipment,pool).Low)),
            DisplayMaxTimes = pool.CollectUpEquipment?pool.GuaranteeUpCount:pool.DisplayMaxTimes
		};

		// 精准补给页的「本期 UP」展示区读这两个列表（GachaDetailPageContext.SetupUpWeaponPanel /
		// SetupUpStigmataPanel）。留空不会崩，但那块是空的，所以按池里 featured 的条目填。
		// 同一批 id 后面还要给 hcoin 的保底面板用（见下方的 ProtectDisplayInfo）。
		List<uint> featuredIds = new List<uint>();

		foreach (GachaDrop drop in pool.Drops)
		{
			if (!drop.Featured || drop.Id == 0u)
			{
				continue;
			}

			featuredIds.Add(drop.Id);

			if (drop.Kind == "weapon")
			{
				commonData.UpWeaponLists.Add(new WeaponDetailData { Id = drop.Id, Level = 1u });
			}
			else if (drop.Kind == "stigmata")
			{
				commonData.UpStigmataLists.Add(new StigmataDetailData { Id = drop.Id, Level = 1u });
			}
		}

		// ------------------------------------------------------------------
		// hcoin_gacha_data 必须带 protect_display_info，否则 type=3（精准补给）
		// 整页卡在「加载中....」，视频区永远出不来。
		//
		// 客户端 output_log.txt 原文：
		//   NullReferenceException
		//     at MonoSpecialGachaCmpt.SetupHcoinGachaDataProtectedInfo()
		//     at MonoSpecialGachaCmpt.SetupView()
		//     at MonoGachaContentCmpt.AttachSpecialCmpt(proto.GachaType)
		//     at MonoGachaContentCmpt.AttachGachaCmpt(...)
		//
		// 反汇编 0x182CEE6B0 SetupHcoinGachaDataProtectedInfo：
		//   pHVar3 = HPGEGKBGMOA.JMNDBOPCHMJ()            // 取 hcoin_gacha_data
		//   if (pHVar3->protect_display_info == 0) raise_null_reference_exception();   ← 这里
		//   然后 IsProtectAllGot(...) → display_max_times - no_protect_gacha_times
		//   再调 NDJIKCKIDCM.GOEHIFJFOOE @0x181F60E50 画保底面板
		//     —— 里面直接解引用 display_key_item_list / droped_up_item_list，两个都不能是 null
		//
		// 异常是在 AttachSpecialCmpt 里抛的，SetupView 后半段和视频组件的挂载因此全部跳过，
		// GachaCGBGCmpt 就一直是预制体的默认「加载中」状态 ——
		// 所以换多少次 video_cg_key 都没用（之前几轮排查就卡在这）。
		// ------------------------------------------------------------------
		if (featuredIds.Count > 0)
		{
			hcoinGachaData.ProtectDisplayInfo = new GachaProtectDisplayInfo
			{
				protect_display_type = GachaProtectDisplayInfo.ProtectDisplayType.EquipDisplay,
				DisplayKeyItemLists = featuredIds.ToArray(),
				// 已获得的 UP 装备。空数组在 protobuf 里等于「字段缺失」，
				// 客户端拿到 null 会在 GOEHIFJFOOE 的 Contains 里再崩一次；
				// 塞一个不存在的 id 0 即可，界面上不会有任何条目被标成已获得。
				DropedUpItemLists = GachaService.PoolProgress(equipment,pool).ObtainedUpItems.Count>0?GachaService.PoolProgress(equipment,pool).ObtainedUpItems.ToArray():new[] { 0u },
				NoProtectGachaTimes = pool.CollectUpEquipment?GachaService.PoolProgress(equipment,pool).Up:GachaService.PoolProgress(equipment,pool).Low
			};
		}

		Packet.c.Log($"[Gacha] 下发池 #{pool.Id} type={pool.GachaType} hcoin={(hcoinGachaData == null ? "null" : "ok")} "
			+ $"custom={(customGachaData == null ? "null" : "ok")} upW={commonData.UpWeaponLists.Count} "
			+ $"upS={commonData.UpStigmataLists.Count} avatar={pool.UpAvatarId} gachaId={pool.GachaId} "
			+ $"protect={(hcoinGachaData?.ProtectDisplayInfo == null ? "null" : "ok")}");

		return new GachaDisplayInfo
		{
			GachaType = (GachaType)pool.GachaType,
			CommonData = commonData,
			CustomGachaData = customGachaData,
			HcoinGachaData = hcoinGachaData
		};
	}
}
