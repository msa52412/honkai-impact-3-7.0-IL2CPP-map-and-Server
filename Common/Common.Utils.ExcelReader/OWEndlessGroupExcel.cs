using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessGroup.json 的一行 —— 旧深渊（迪拉克之海 / 量子奇点）的「分组档位」定义。
///
/// 这张表是 (GroupLevel, Type) 二元组唯一键，和客户端
/// MonoOWEndlessUIManager.RefreshPlayerGroup 里调用的
/// GetOWEndlessGroupMetaByKey(GroupLevel, mode) 完全同构 ——
/// 所以服务端查表的键必须和它一致，否则算出来的值和界面显示会对不上。
///
/// Type 取值 = EndlessType 枚举：2 迪拉克之海、3 量子奇点。
///
/// 实测有效档位（2026-10-25 扫表）：
///     Type=2  playerGroup 2 : 21 22 23 24      （4 档）
///     Type=2  playerGroup 3 : 31 32 33 34 35   （5 档 ★ 原罪/禁忌/苦痛/红莲/无限）
///     Type=3  playerGroup 1 : 11 12 13 14
///     Type=3  playerGroup 2 : 21 22 23 24
///     Type=3  playerGroup 3 : 31 32 33 34 35
///
/// GroupLevel 升序 = 段位升序；图标 IconGroup_3..7 就是那五个段位名。
/// </summary>
public class OWEndlessGroupExcel
{
	/// <summary>该档位属于哪个玩家分组（对应 OWEndlessPlayerGroup.playerGroup）。</summary>
	[JsonProperty("playerGroup")]
	public int PlayerGroup { get; set; }

	/// <summary>
	/// 扰动幅度 —— 界面上「扰动幅度 N」显示的就是这个值。
	/// 实测 Type=2 的取值：21→2 22→3 23→4 24→5 / 31→3 32→4 33→5 34→6 35→7。
	/// 服务端 GetOpenworldEndlessDataRsp.HardLevel 必须下发它，
	/// 否则写死 1 会让界面永远显示「扰动幅度 1」。
	/// </summary>
	[JsonProperty("groupHardLevel")]
	public int GroupHardLevel { get; set; }

	[JsonProperty("EliteRatio")]
	public double EliteRatio { get; set; }

	[JsonProperty("ExtraScore")]
	public int ExtraScore { get; set; }

	[JsonProperty("ExtraScoreTime")]
	public int ExtraScoreTime { get; set; }

	/// <summary>分组容量 —— 对应 Rsp.TopGroupPlayerNum。</summary>
	[JsonProperty("maxCapacity")]
	public int MaxCapacity { get; set; }

	/// <summary>晋级名次 —— 对应 Rsp.TopGroupPromoteUnlockPlayerNum。</summary>
	[JsonProperty("promoteRank")]
	public int PromoteRank { get; set; }

	[JsonProperty("demoteRank")]
	public int DemoteRank { get; set; }

	[JsonProperty("minSkipLevel")]
	public int MinSkipLevel { get; set; }

	[JsonProperty("maxSkipLevel")]
	public int MaxSkipLevel { get; set; }

	[JsonProperty("skipLevelTime")]
	public int SkipLevelTime { get; set; }

	[JsonProperty("unSelectColor")]
	public string UnSelectColor { get; set; }

	[JsonProperty("bgColor")]
	public string BgColor { get; set; }

	[JsonProperty("unselectPrefabPath")]
	public string UnselectPrefabPath { get; set; }

	[JsonProperty("selectPrefabPath")]
	public string SelectPrefabPath { get; set; }

	[JsonProperty("unopenPrefabPath")]
	public string UnopenPrefabPath { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	/// <summary>档位等级 —— 主键之一。</summary>
	[JsonProperty("GroupLevel")]
	public int GroupLevel { get; set; }

	/// <summary>深渊类型（EndlessType）：2 迪拉克之海 / 3 量子奇点。</summary>
	[JsonProperty("Type")]
	public int Type { get; set; }
}
