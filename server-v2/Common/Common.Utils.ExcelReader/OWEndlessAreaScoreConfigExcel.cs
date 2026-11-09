using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessAreaScoreConfig.json 的一行 —— 旧深渊「这一轮按什么顺序打哪些怪组」。
///
/// 客户端 QTInLevelData 只认这张表：
///     MonsterGroupCount()      = SplitBattles.Length
///     IndexToMonsterGroup(ix)  = SplitBattles[ix].groupScoreId
///     MonsterGroupToIndex(gid) = 在 SplitBattles 里线性找 groupScoreId
///     GetBattle(groupId)       = 按 groupScoreId 去 MonsterGroup 表取波次
///
/// 所以服务端推进进度时必须跟这张表同序：SplitBattles 的第 0 项是第一场，
/// 每完成一场就往后挪一格；groupScoreId 同时是 FINISH(3424) 的 group_id。
/// </summary>
public class OWEndlessAreaScoreConfigExcel
{
	/// <summary>主键，由 OWEndlessBattleConfig.AreaScoreConfigID 引用。</summary>
	[JsonProperty("AreaScoreConfigID")]
	public int AreaScoreConfigID { get; set; }

	/// <summary>深渊类型：2 迪拉克之海 / 3 量子奇点。</summary>
	[JsonProperty("Type")]
	public int Type { get; set; }

	/// <summary>战斗顺序（小怪 + BOSS 混排），顺序即客户端 index。</summary>
	[JsonProperty("SplitBattles")]
	public OWEndlessSplitBattle[] SplitBattles { get; set; }

	/// <summary>每层小怪组，形如 "10000:10100:10200:10300"。</summary>
	[JsonProperty("MonsterScoreConfig")]
	public string[] MonsterScoreConfig { get; set; }

	/// <summary>每层 BOSS 组。</summary>
	[JsonProperty("BossScoreConfig")]
	public int[] BossScoreConfig { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }
}

public class OWEndlessSplitBattle
{
	/// <summary>对应 OWEndlessMonsterGroupScore.MonsterGroupID。</summary>
	[JsonProperty("groupScoreId")]
	public int GroupScoreId { get; set; }

	[JsonProperty("isBoss")]
	public bool IsBoss { get; set; }

	[JsonProperty("layerId")]
	public int LayerId { get; set; }
}
