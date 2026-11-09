using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 家园小队（Sanctuary）的「玩家等级 → 分组」映射表。
///
/// 表里只有 3 行（SanctuaryPlayerGroup.json）：
///   Lv81~85 → PlayerGroupID 101
///   Lv61~80 → PlayerGroupID 102
///   Lv30~60 → PlayerGroupID 103
/// ActivityID 三行都是 1001 —— 家园小队活动的固定 id。
///
/// 客户端 SanctuaryActivityData.player_level_group_id 填的就是这里的 PlayerGroupID，
/// 它会拿去查「本组的分组配置」（KLDGODLLNFJ.CPOPAAGJKEC 里第二个查表动作，
/// 0x180E3D2D6 → 0x1842c4e10，入参 (player_level_group_id, activity_id)）。
///
/// 注意 JSON 里的 DataImpl 是客户端元数据的空壳，这里不声明，Newtonsoft 默认忽略未知字段。
/// </summary>
public class SanctuaryPlayerGroupExcel
{
	[JsonProperty("MinPlayerLevel")]
	public int MinPlayerLevel { get; set; }

	[JsonProperty("MaxPlayerLevel")]
	public int MaxPlayerLevel { get; set; }

	[JsonProperty("PlayerGroupID")]
	public int PlayerGroupId { get; set; }

	[JsonProperty("ActivityID")]
	public int ActivityId { get; set; }
}
