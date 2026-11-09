using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>一条掉落规则。</summary>
internal sealed class GachaDrop
{
	/// <summary>
	/// 掉落类型：
	///   <c>avatarFragment</c> 角色碎片（id 必须是 AvatarFragmentData 里的行）
	///   <c>stigmata</c>       圣痕（<c>id</c> 指定就用它，否则按 <c>rarity</c> 随机）
	///   <c>weapon</c>         武器（同上）
	///   <c>material</c>       材料（必须给 <c>id</c>）
	/// </summary>
	[JsonProperty("kind")]
	public string Kind { get; set; } = "stigmata";

	/// <summary>固定物品 id；0 = 按 <see cref="Rarity"/> 从表里随机。</summary>
	[JsonProperty("id")]
	public uint Id { get; set; }

	/// <summary>星级。仅 <c>stigmata</c> / <c>weapon</c> 且 <see cref="Id"/> 为 0 时生效。</summary>
	[JsonProperty("rarity")]
	public uint Rarity { get; set; }

	/// <summary>一次给几个。</summary>
	[JsonProperty("num")]
	public uint Num { get; set; } = 1u;

	/// <summary>权重（相对值，不用凑成 100）。</summary>
	[JsonProperty("weight")]
	public int Weight { get; set; } = 100;

	/// <summary>备注，只给人看，不参与逻辑。</summary>
	[JsonProperty("note")]
	public string? Note { get; set; }

	/// <summary>
	/// 是否属于「本期 UP」。
	///
	/// 只影响三件事：
	///   1. 抽到它把**两档**保底计数器都清零；
	///   2. 它是 <see cref="GachaPool.GuaranteeUpCount"/> 那一档的候选池；
	///   3. 日志/统计里算「UP 出货」。
	///
	/// **概率翻倍不是靠这个字段**，是靠在配置里把它的 <see cref="Weight"/> 写成
	/// 同级其他条目的 2 倍 —— 权重是唯一决定概率的东西，flag 不参与摇号。
	/// </summary>
	[JsonProperty("featured")]
	public bool Featured { get; set; }

	/// <summary>
	/// 是否属于「低档保底**摇号池**」。
	///
	/// 池里配了 <see cref="GachaPool.GuaranteeCount"/>（比如 10）时：
	///   * 连续 N 抽没抽到任何一个 <c>pity</c>（或 <c>featured</c>）条目，
	///     第 N 抽就**跳过普通权重**，只从这个池子里摇一个；
	///   * 抽到任意一个 <c>pity</c> 或 <c>featured</c>，计数器清零。
	///
	/// 注意「摇号池」和「清零条件」不是一回事：
	///   * 角色池的摇号池**只放 4 个 A 级**（UP 的 S 级只打 featured）——
	///     「每 10 抽必出 A 级角色」就该摇出 A 级，S 级塞进来会让保底位
	///     以 ~5.8% 的概率摇成 S 碎片，十连里就看不到 A 级了。
	///   * 清零条件仍然含 featured：自然抽到 S 级也算「A 级及以上」，
	///     不该再为这一抽补一个 A。
	///
	/// 副产物**不要**打这个标记，否则保底会出到不该出的东西。
	/// </summary>
	[JsonProperty("pity")]
	public bool Pity { get; set; }
}

/// <summary>一个卡池。</summary>
internal sealed class GachaPool
{
    [JsonProperty("rotation_group")] public string RotationGroup {get;set;}="";
    [JsonProperty("rotation_slot")] public int RotationSlot {get;set;}
	/// <summary>配置内唯一编号，仅用于日志。</summary>
	[JsonProperty("id")]
	public int Id { get; set; }

	/// <summary>
	/// 协议里的 gacha_type，决定客户端把它放进哪个 tab：
	///   5  = GACHA_CUSTOM_AVATAR  角色扩充
	///   7  = GACHA_CUSTOM_EQUIP   装备扩充
	///   2  = GACHA_HCOIN          标准补给
	///   3  = GACHA_SPECIAL_HCOIN  精准补给
	/// </summary>
	[JsonProperty("gacha_type")]
	public uint GachaType { get; set; } = 5u;

	/// <summary>false = 不下发这个池（留着当模板）。</summary>
    [JsonProperty("period_key")]
    public string PeriodKey {get;set;}="";
    [JsonProperty("wish_enabled")]
    public bool WishEnabled {get;set;}
    [JsonProperty("wish_period_key")]
    public string WishPeriodKey {get;set;}="";
    [JsonProperty("wish_equipment_level")]
    public uint WishEquipmentLevel {get;set;}=1;
    [JsonProperty("normalize_equipment")]
    public bool NormalizeEquipment {get;set;}
    [JsonProperty("collect_up_equipment")]
    public bool CollectUpEquipment {get;set;}
    [JsonProperty("ex_energy_per_draw")]
    public uint ExEnergyPerDraw {get;set;}=50;
    [JsonProperty("ticket_material_id")]
    public uint TicketMaterialId {get;set;}
	[JsonProperty("enabled")]
	public bool Enabled { get; set; } = true;

	[JsonProperty("title")]
	public string Title { get; set; } = "扩充补给";

	[JsonProperty("content")]
	public string Content { get; set; } = "";

	/// <summary>卡池标题旁的小图标，走 SpriteOutput 同步图集。</summary>
	[JsonProperty("title_image")]
	public string TitleImage { get; set; } = "SpriteOutput/ShopTabIcons/IconShopGacha";

	/// <summary>
	/// 卡池大背景图。客户端 <c>GachaBgImageCmpt.SetupImg()</c> 读
	/// <c>display_ext.common_gacha_bg</c>，我们把这个值塞进去。
	/// 必须以 <c>event/Immediately</c> 或 <c>SpriteOutput</c> 开头。
	/// </summary>
	[JsonProperty("bg_image")]
	public string BgImage { get; set; } = "";

	/// <summary>
	/// 补给页中间那块动态视频的 CG key，写进 <c>display_ext["video_cg_key"]</c>。
	///
	/// 客户端 <c>GachaVideoPlayer</c> 走 <c>TryToPlayCGByKey(cgExtraKey)</c>，
	/// 拿这个 key 去 <c>CgData</c> 里按 <c>CgExtraKey</c> 找行，再播那一行的视频。
	/// 空 = 回落到客户端内置的 <c>ConfigGacha.GachaVideoDict[gacha_type].CgExtraKey</c>。
	/// 可用的 key 见 ExcelOutputAsset/CgData.json 的 <c>CgExtraKey</c> 列，
	/// 例如 <c>4.8_Gacha</c>（希儿）、<c>5.6_Gacha</c>、<c>5.7_Gacha</c>。
	/// </summary>
	[JsonProperty("video_cg_key")]
	public string VideoCgKey { get; set; } = "";

	/// <summary>
	/// <c>CustomGachaData.display_video_avatar</c>：补给页视频按哪个角色出。
	/// 0 = 自动用 <see cref="UpAvatarId"/>。
	/// </summary>
	[JsonProperty("display_video_avatar")]
	public uint DisplayVideoAvatar { get; set; }

	[JsonProperty("gacha_id")]
	public uint GachaId { get; set; } = 30115600u;

	[JsonProperty("ticket_hcoin_cost")]
	public uint TicketHcoinCost { get; set; } = 280u;

	/// <summary>保底次数（界面上「N 次内必出」的 N）。</summary>
	[JsonProperty("display_max_times")]
	public uint DisplayMaxTimes { get; set; } = 100u;

	/// <summary>
	/// 低档保底：连续这么多抽都没出 <see cref="GachaDrop.Pity"/> / <see cref="GachaDrop.Featured"/>
	/// 标记的掉落，下一抽强制从这批里摇。
	/// 0 = 不开这一档保底。
	///
	/// 角色池是 10（每 10 抽必出 A 级及以上），装备池是 10（每 10 抽必出 5★）。
	/// 计数器存在存档 <c>EquipmentScheme.GachaGuaranteeCounter</c> 里，跨登录有效。
	/// 自然概率仍然按 drops 的权重走，所以实际出货率会高于 1/GuaranteeCount。
	/// </summary>
	[JsonProperty("guarantee_count")]
	public uint GuaranteeCount { get; set; }

	/// <summary>
	/// 高档保底：连续这么多抽都没出 <see cref="GachaDrop.Featured"/>（本期 UP），
	/// 下一抽强制从 UP 里摇。0 = 不开。
	///
	/// 角色池是 100（每 100 抽必出 UP S 级）。
	///
	/// 两档可以同时开。同一抽同时触发时 **UP 优先** —— 否则先满足低档会把
	/// 高档的计数白白推掉。计数器存在 <c>EquipmentScheme.GachaUpGuaranteeCounter</c>。
	/// </summary>
	[JsonProperty("guarantee_up_count")]
	public uint GuaranteeUpCount { get; set; }

	/// <summary>
	/// 碎片兑换活动 id（<c>CustomGachaExFragment.EventID</c>）。0 = 这个池没有碎片兑换。
	/// 非 0 时客户端才会初始化「0/210 → 头像」那块 UI。
	/// </summary>
	[JsonProperty("ex_id")]
	public uint ExId { get; set; }

	/// <summary>兑换一次要多少碎片（表里 EventID 对应的 MaterialAmountRequest）。</summary>
	[JsonProperty("ex_energy_need")]
	public uint ExEnergyNeed { get; set; } = 210u;

	/// <summary>碎片兑换的产物 / 卡池 UP 角色（AvatarData.avatarID）。三处引用保持一致。</summary>
	[JsonProperty("up_avatar_id")]
	public uint UpAvatarId { get; set; }

	/// <summary>
	/// 独立卡池配置文件（相对当前目录，如 <c>gacha_pool_1.json</c>）。
	///
	/// gacha.json 里写 <c>{ "file": "gacha_pool_1.json" }</c> 这种占位对象时，
	/// 服务端会把这个文件读进来当成整个池（本占位对象自己的其它字段全部忽略）。
	/// 好处：两个池各改各的文件，互不影响；池文件改了也会热加载。
	///
	/// 也可以不用它，直接把池对象内联写在 gacha.json 里（老写法仍然有效）。
	/// </summary>
	[JsonProperty("file")]
	public string? File { get; set; }

	[JsonProperty("drops")]
	public List<GachaDrop> Drops { get; set; } = new List<GachaDrop>();

    [JsonProperty("gift_drops")]
    public List<GachaDrop> GiftDrops {get;set;}=new();

	/// <summary>
	/// 副产物清单文件（相对当前目录，通常是 <c>gacha_byproducts.json</c>）。
	///
	/// 非空时：先读这个文件里的 <c>drops</c>，再追加到本池自己的 <see cref="Drops"/> 后面。
	/// 这样「所有卡池共用的副产物」只维护一份，池内 <c>drops</c> 只留本池特有的东西
	/// （比如扩充补给的 UP 角色碎片）。
	///
	/// 文件不存在 / 解析失败只会打警告，不会让卡池空掉。
	/// 文件改了也会热加载 —— 它的 mtime 和 gacha.json 一起参与缓存判断。
	/// </summary>
	[JsonProperty("byproduct_file")]
	public string? ByproductFile { get; set; }
}

/// <summary>副产物清单文件（<c>gacha_byproducts.json</c>）的根。</summary>
internal sealed class GachaByproductFile
{
    [JsonProperty("gift_drops")]
    public List<GachaDrop> GiftDrops {get;set;}=new();
	[JsonProperty("drops")]
	public List<GachaDrop> Drops { get; set; } = new List<GachaDrop>();
}

/// <summary>gacha.json 的根。</summary>
internal sealed class GachaConfigFile
{
	[JsonProperty("ticket_material_id")]
	public uint TicketMaterialId { get; set; } = 1103u;

	[JsonProperty("ticket_keep")]
	public uint TicketKeep { get; set; } = 0u;

	[JsonProperty("equipment_limit_base")]
	public uint EquipmentLimitBase { get; set; } = 1000u;

	[JsonProperty("equipment_limit_slack")]
	public uint EquipmentLimitSlack { get; set; } = 200u;

	/// <summary>
	/// 抽到角色卡时是否**真的解锁**该女武神。
	///
	/// true（默认）：把角色写进 AvatarList，客户端才会有「获得新角色」的解锁动画 / 立绘；
	///               解锁不会往仓库塞任何抽卡产物（初始武器也拦掉了，见 GachaService）。
	/// false：只展示卡面，角色不会出现在女武神列表里 —— 也就不会有解锁动画。
	/// </summary>
	[JsonProperty("grant_avatar")]
	public bool GrantAvatar { get; set; } = true;

	[JsonProperty("pools")]
	public List<GachaPool> Pools { get; set; } = new List<GachaPool>();
}

/// <summary>
/// 补给卡池配置，**改文件即生效，不用重编译也不用重启**。
///
/// 为什么要有它
/// ------------
/// 卡池角色、UP、掉落构成、概率、背景图本来全写死在 <c>GachaService</c> 的 const 里，
/// 每调一次都要 publish + stop + start，客户端还得重登。
/// 这些全是运营参数，不该和代码绑在一起。
///
/// 文件：run\gacha.json（首次启动自动生成一份默认的）
/// 生效时机：下一次 GetConfig/GetGachaDisplay/GachaReq 就会读到新内容，
///           客户端退出补给页再进一次即可（不用重登）。
///
/// 结构见文件里自动生成的注释与样例；掉落 <c>weight</c> 是相对权重，
/// 不用凑成 100，改完保存就行。
/// </summary>
internal static class GachaConfig
{
	private const string FILE_NAME = "gacha.json";

	private static DateTime _stamp = DateTime.MinValue;
	private static GachaConfigFile _cfg = Default();

	/// <summary>
	/// 被引用文件的路径 → 上次读到的 mtime。
	/// 覆盖两类：卡池独立配置文件（<c>pool.file</c>）和副产物清单（<c>pool.byproduct_file</c>）。
	/// 它们任何一个改了都要重新加载，不能只看 gacha.json 自己的 mtime。
	/// </summary>
	private static readonly Dictionary<string, DateTime> _fileStamps =
		new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

	internal static GachaConfigFile Current
	{
		get { return Load(); }
	}

	/// <summary>抽到角色卡是否真的解锁女武神（<c>grant_avatar</c>）。</summary>
	internal static bool GrantAvatar
	{
		get { return Load().GrantAvatar; }
	}

	/// <summary>当前启用的卡池（<c>enabled=false</c> 的会被过滤掉）。</summary>
	internal static List<GachaPool> ActivePools
	{
		get
		{
			List<GachaPool> all = Load().Pools ?? new List<GachaPool>();
			return GachaRotation.Select(all.Where(p => p.Enabled && p.GachaType != 0u).ToList());
		}
	}

	/// <summary>按 gacha_type 找池；找不到回落第一个启用的池。</summary>
	internal static GachaPool? Pool(uint gachaType)
	{
		List<GachaPool> pools = ActivePools;
		if (pools.Count == 0)
		{
			return null;
		}
		foreach (GachaPool p in pools)
		{
			if (p.GachaType == gachaType)
			{
				return p;
			}
		}
		return null;
	}

	/// <summary>第一个带碎片兑换的池（4300 结算里的 ex_fragment 用它）。</summary>
	internal static GachaPool? ExPool
	{
		get
		{
			foreach (GachaPool p in ActivePools)
			{
				if (p.ExId != 0u)
				{
					return p;
				}
			}
			return ActivePools.FirstOrDefault();
		}
	}

	internal static uint TicketMaterialId
	{
		get { return Load().TicketMaterialId; }
	}

	internal static uint TicketKeep
	{
		get { return Load().TicketKeep; }
	}

	private static GachaConfigFile Load()
	{
		try
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), FILE_NAME);
			if (!File.Exists(path))
			{
				// 首次启动：把默认配置落盘，之后用户直接改这个文件。
				File.WriteAllText(path, DefaultJson);
				_stamp = File.GetLastWriteTimeUtc(path);
				_cfg = Parse(DefaultJson);
				Packet.c.Log($"[Gacha] 已生成默认卡池配置 {path}（{_cfg.Pools.Count} 个池）");
				return _cfg;
			}

			DateTime stamp = File.GetLastWriteTimeUtc(path);
			if (stamp == _stamp && !WatchedChanged())
			{
				return _cfg;
			}

			GachaConfigFile? parsed = JsonConvert.DeserializeObject<GachaConfigFile>(File.ReadAllText(path));
			_cfg = parsed ?? Default();
			_stamp = stamp;

			// 兼容：老配置里没有的字段补默认值，避免 null 引用。
			_cfg.Pools ??= new List<GachaPool>();
			_fileStamps.Clear();

			// 每个池要么直接内联写在这里，要么写 {"file": "gacha_pool_1.json"} 指向独立文件。
			ExpandPools(_cfg);

			foreach (GachaPool p in _cfg.Pools)
			{
				p.Drops ??= new List<GachaDrop>();
				if (p.GachaType == 0u)
				{
					p.GachaType = 5u;
				}
				if (p.DisplayMaxTimes == 0u)
				{
					p.DisplayMaxTimes = 100u;
				}
			}
			if (_cfg.TicketMaterialId == 0u)
			{
				_cfg.TicketMaterialId = 1103u;
			}
			if (_cfg.EquipmentLimitBase == 0u)
			{
				_cfg.EquipmentLimitBase = 1000u;
			}
			if (_cfg.EquipmentLimitSlack == 0u)
			{
				_cfg.EquipmentLimitSlack = 200u;
			}

			// 把每个池引用的副产物文件合并进来（所有卡池共用一份）。
			MergeByproducts(_cfg);
            foreach(var pool in _cfg.Pools)
            {
                if(pool.NormalizeEquipment)
                {
                    var weaponParents=BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().All.Where(x=>x.EvoId>0).GroupBy(x=>x.EvoId).ToDictionary(x=>x.Key,x=>x.First().Id);
                    var stigmataParents=BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance().All.Where(x=>x.EvoId>0).GroupBy(x=>x.EvoId).ToDictionary(x=>x.Key,x=>x.First().Id);
                    foreach(var drop in pool.Drops)
                    {
                        var parents=string.Equals(drop.Kind,"weapon",StringComparison.OrdinalIgnoreCase)?weaponParents:string.Equals(drop.Kind,"stigmata",StringComparison.OrdinalIgnoreCase)?stigmataParents:null;
                        if(parents==null)continue;
                        var seen=new HashSet<int>();int id=(int)drop.Id;
                        while(parents.TryGetValue(id,out int previous) && seen.Add(id))id=previous;
                        drop.Id=(uint)id;
                    }
                }
                pool.Drops.RemoveAll(drop => !GachaEligibility.Allowed(drop));
                if(!GachaEligibility.IsRegularAvatar((int)pool.UpAvatarId) || !GachaEligibility.IsRegularAvatar((int)pool.DisplayVideoAvatar))
                    pool.Enabled=false;
            }

			Packet.c.Log($"[Gacha] 重新读取 {FILE_NAME}：" + Summary());
		}
		catch (Exception ex)
		{
			// 配置写坏了绝不能让服务端抛异常，退回上一次（或默认）配置。
			Packet.c.Warn($"[Gacha] 读取 {FILE_NAME} 失败，沿用旧配置：{ex.Message}");
		}
		return _cfg;
	}

	private static long Sum(List<GachaDrop> drops)
	{
		long s = 0;
		foreach (GachaDrop d in drops)
		{
			s += Math.Max(0, d.Weight);
		}
		return s;
	}

	/// <summary>卡池摘要，启动时和每次热加载各打一行。</summary>
	internal static string Summary()
	{
		GachaConfigFile cfg = Load();
		string head = cfg.GrantAvatar ? "grant_avatar=on" : "grant_avatar=off";
		return head + " / " + string.Join(" / ", cfg.Pools.Select(p =>
		{
			// pityN = 低档保底**摇号池**大小（只数 pity），upN = 高档池大小（featured）。
			// 角色池应该是 pity=4(up 1)：4 个 A 级 + 1 个 UP S 级，
			// 摇号池只有那 4 个 —— 出现 pity=5 说明 UP 又被误打了 pity，保底会摇出 S 碎片。
			int pityN = p.Drops.Count(d => d.Pity);
			int upN = p.Drops.Count(d => d.Featured);
			string pity = "";
			if (p.GuaranteeCount > 0u || p.GuaranteeUpCount > 0u)
			{
				pity = $" pity={pityN}(up {upN}) guarantee={p.GuaranteeCount}";
				if (p.GuaranteeUpCount > 0u)
				{
					pity += $"/up {p.GuaranteeUpCount}";
				}
			}
			return $"#{p.Id} type={p.GachaType} {(p.Enabled ? "on" : "off")} "
				+ $"drops={p.Drops.Count} sum={Sum(p.Drops)}{pity}";
		}));
	}

	/// <summary>被引用的文件（池配置 / 副产物）有没有变。</summary>
	private static bool WatchedChanged()
	{
		foreach (KeyValuePair<string, DateTime> kv in _fileStamps)
		{
			DateTime now = File.Exists(kv.Key) ? File.GetLastWriteTimeUtc(kv.Key) : DateTime.MinValue;
			if (now != kv.Value)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>相对路径按服务端工作目录解析（就是 run\）。</summary>
	private static string Resolve(string rel)
	{
		rel = rel.Trim();
		return Path.IsPathRooted(rel) ? rel : Path.Combine(Directory.GetCurrentDirectory(), rel);
	}

	/// <summary>登记一个被引用文件的 mtime，供 <see cref="WatchedChanged"/> 判断热加载。</summary>
	private static void Watch(string full)
	{
		_fileStamps[full] = File.Exists(full) ? File.GetLastWriteTimeUtc(full) : DateTime.MinValue;
	}

	/// <summary>
	/// 把 <c>{"file": "xxx.json"}</c> 形式的池占位替换成真正的池对象。
	/// 内联写法的池原样保留，所以老配置不用改也能跑。
	/// </summary>
	private static void ExpandPools(GachaConfigFile cfg)
	{
		for (int i = 0; i < cfg.Pools.Count; i++)
		{
			GachaPool stub = cfg.Pools[i];
			if (string.IsNullOrWhiteSpace(stub.File))
			{
				continue;
			}

			string rel = stub.File!.Trim();
			string full = Resolve(rel);
			Watch(full);

			if (!File.Exists(full))
			{
				Packet.c.Warn($"[Gacha] 卡池文件不存在：{full}");
				continue;
			}

			try
			{
				GachaPool? real = JsonConvert.DeserializeObject<GachaPool>(File.ReadAllText(full));
				if (real == null)
				{
					Packet.c.Warn($"[Gacha] 卡池文件解析成空：{rel}");
					continue;
				}
				real.Drops ??= new List<GachaDrop>();
				cfg.Pools[i] = real;
				Packet.c.Log($"[Gacha] 载入卡池 {rel}（#{real.Id} type={real.GachaType} "
					+ $"{(real.Enabled ? "on" : "off")}）");
			}
			catch (Exception ex)
			{
				Packet.c.Warn($"[Gacha] 卡池文件解析失败 {rel}：{ex.Message}");
			}
		}
	}

	/// <summary>
	/// 把各池 <c>byproduct_file</c> 指向的文件读进来，追加到该池 drops 的末尾。
	/// 文件缺失或写坏只打警告，不影响池内自带的 drops。
	/// </summary>
	private static void MergeByproducts(GachaConfigFile cfg)
	{
		foreach (GachaPool p in cfg.Pools)
		{
			if (string.IsNullOrWhiteSpace(p.ByproductFile))
			{
				continue;
			}

			string rel = p.ByproductFile!.Trim();
			string full = Resolve(rel);
			Watch(full);

			if (!File.Exists(full))
			{
				Packet.c.Warn($"[Gacha] 池#{p.Id} 的副产物文件不存在：{full}");
				continue;
			}

			try
			{
				GachaByproductFile? bp = JsonConvert.DeserializeObject<GachaByproductFile>(File.ReadAllText(full));
				if(bp?.GiftDrops?.Count>0)p.GiftDrops.AddRange(bp.GiftDrops);
                int n = bp?.Drops?.Count ?? 0;
				if (n > 0)
				{
					p.Drops.AddRange(bp!.Drops);
				}
				Packet.c.Log($"[Gacha] 池#{p.Id} 载入副产物 {n} 条（{rel}）");
			}
			catch (Exception ex)
			{
				Packet.c.Warn($"[Gacha] 池#{p.Id} 副产物文件解析失败：{ex.Message}");
			}
		}
	}

	private static GachaConfigFile Parse(string json)
	{
		return JsonConvert.DeserializeObject<GachaConfigFile>(json) ?? Default();
	}

	private static GachaConfigFile Default()
	{
		return Parse(DefaultJson);
	}

	/// <summary>
	/// 默认配置（首次启动写到 run\gacha.json）。
	/// 角色池按崩3 扩充补给的构成：UP S 角色碎片 + 4 个 A 角色碎片 + 3/2/1 星圣痕武器。
	/// 权重只是起点，后续按体验调。
	/// </summary>
	private const string DefaultJson = @"{
  ""_说明"": ""补给卡池配置。改完保存即生效（不用重启服务端、不用重新编译）；进游戏重开一次补给页即可看到变化。"",

  ""ticket_material_id"": 1103,
  ""ticket_keep"": 0,
  ""equipment_limit_base"": 1000,
  ""equipment_limit_slack"": 200,

  ""pools"": [
    {
      ""id"": 1,
      ""gacha_type"": 5,
      ""enabled"": true,
      ""title"": ""扩充补给"",
      ""content"": ""本期 UP：希儿·彼岸双生"",
      ""title_image"": ""SpriteOutput/ShopTabIcons/IconShopGacha"",
      ""bg_image"": ""event/Immediately/GachaPic/7.0_wbg1-1_truecolor2048"",
      ""video_cg_key"": ""7.0_Birthday_Seele"",
      ""display_video_avatar"": 712,
      ""gacha_id"": 30115600,
      ""ticket_hcoin_cost"": 280,
      ""display_max_times"": 100,
      ""ex_id"": 1,
      ""ex_energy_need"": 210,
      ""up_avatar_id"": 712,
      ""drops"": [
        { ""kind"": ""avatarFragment"", ""id"": 10712, ""num"": 1, ""weight"": 300, ""note"": ""希儿·彼岸双生 碎片（UP）"" },

        { ""kind"": ""avatarFragment"", ""id"": 10211, ""num"": 1, ""weight"": 200, ""note"": ""八重樱·御神装·勿忘"" },
        { ""kind"": ""avatarFragment"", ""id"": 10311, ""num"": 1, ""weight"": 200, ""note"": ""布洛妮娅·雪地狙击"" },
        { ""kind"": ""avatarFragment"", ""id"": 10503, ""num"": 1, ""weight"": 200, ""note"": ""德丽莎·紫苑"" },
        { ""kind"": ""avatarFragment"", ""id"": 10601, ""num"": 1, ""weight"": 200, ""note"": ""符华·迅羽"" },

        { ""kind"": ""stigmata"", ""rarity"": 3, ""num"": 1, ""weight"": 800, ""note"": ""3 星圣痕"" },
        { ""kind"": ""weapon"",   ""rarity"": 3, ""num"": 1, ""weight"": 600, ""note"": ""3 星武器"" },
        { ""kind"": ""stigmata"", ""rarity"": 2, ""num"": 1, ""weight"": 900, ""note"": ""2 星圣痕"" },
        { ""kind"": ""weapon"",   ""rarity"": 2, ""num"": 1, ""weight"": 700, ""note"": ""2 星武器"" },
        { ""kind"": ""stigmata"", ""rarity"": 1, ""num"": 1, ""weight"": 400, ""note"": ""1 星圣痕"" },
        { ""kind"": ""weapon"",   ""rarity"": 1, ""num"": 1, ""weight"": 300, ""note"": ""1 星武器"" }
      ]
    },

    {
      ""id"": 2,
      ""gacha_type"": 7,
      ""enabled"": false,
      ""title"": ""精准补给"",
      ""content"": ""本期 UP：4★ 装备"",
      ""title_image"": ""SpriteOutput/ShopTabIcons/IconShopGacha"",
      ""bg_image"": ""event/Immediately/GachaPic/7.0_wbg2-1_truecolor2048"",
      ""gacha_id"": 30115700,
      ""ticket_hcoin_cost"": 280,
      ""display_max_times"": 50,
      ""ex_id"": 0,
      ""up_avatar_id"": 0,
      ""drops"": [
        { ""kind"": ""weapon"",   ""rarity"": 4, ""num"": 1, ""weight"": 600, ""note"": ""4 星武器"" },
        { ""kind"": ""stigmata"", ""rarity"": 4, ""num"": 1, ""weight"": 1200, ""note"": ""4 星圣痕"" },
        { ""kind"": ""weapon"",   ""rarity"": 3, ""num"": 1, ""weight"": 800, ""note"": ""3 星武器"" },
        { ""kind"": ""stigmata"", ""rarity"": 3, ""num"": 1, ""weight"": 1200, ""note"": ""3 星圣痕"" },
        { ""kind"": ""weapon"",   ""rarity"": 2, ""num"": 1, ""weight"": 600, ""note"": ""2 星武器"" },
        { ""kind"": ""stigmata"", ""rarity"": 2, ""num"": 1, ""weight"": 800, ""note"": ""2 星圣痕"" },
        { ""kind"": ""weapon"",   ""rarity"": 1, ""num"": 1, ""weight"": 400, ""note"": ""1 星武器"" },
        { ""kind"": ""stigmata"", ""rarity"": 1, ""num"": 1, ""weight"": 400, ""note"": ""1 星圣痕"" }
      ]
    }
  ]
}";
}
