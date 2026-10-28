using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessMonsterGroupScore.json 的一行 —— 每个怪物组的计分。
///
/// 3424 FinishOpenworldEndlessMonsterGroupRsp.Score 是「本周期总分」，
/// 客户端只拿它和 OldScore 做飘分/结算动画，不会自己累加；
/// 所以服务端必须按 groupScoreId 查出这一场的分，再加上客户端带上来的
/// ExtraScore（天气/扰动加成）。
/// </summary>
public class OWEndlessMonsterGroupScoreExcel
{
	/// <summary>主键，对应 SplitBattles.groupScoreId。</summary>
	[JsonProperty("MonsterGroupID")]
	public int MonsterGroupID { get; set; }

	/// <summary>本组基础分（量子：小怪 1000~1800，BOSS 2000）。</summary>
	[JsonProperty("Score")]
	public int Score { get; set; }

	[JsonProperty("MonsterInitType")]
	public int MonsterInitType { get; set; }

	[JsonProperty("RandomType")]
	public int RandomType { get; set; }

	[JsonProperty("ExtraInitTypes")]
	public string[] ExtraInitTypes { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }
}
