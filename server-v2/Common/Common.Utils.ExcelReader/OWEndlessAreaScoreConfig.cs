using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessAreaScoreConfig.json 读取器。
///
/// 2026-10-26 为什么需要它：量子奇点的进度不是「一个整数」，
/// 而是一条 SplitBattles 序列。客户端 QTInLevelData.Refresh 只接受
/// rsp.type == 3 的分支，并且把 rsp.quantum_endless.cur_monster_group
/// 直接当作当前 groupScoreId；之后 3424 回包也靠它把 UI 推进到下一场。
/// 服务端若只会回 0，客户端就永远停在第一场/准备页。
/// </summary>
public class OWEndlessAreaScoreConfig : BaseExcelReader<OWEndlessAreaScoreConfig, OWEndlessAreaScoreConfigExcel>
{
	public override string FileName => "OWEndlessAreaScoreConfig.json";

	/// <summary>按主键取行。Type 非 0 时同时校验模式，防止把迪拉克的 1001 给量子用。</summary>
	public OWEndlessAreaScoreConfigExcel? FromKey(int areaScoreConfigId, int type = 0)
	{
		return All.FirstOrDefault((OWEndlessAreaScoreConfigExcel row) =>
			row.AreaScoreConfigID == areaScoreConfigId && (type == 0 || row.Type == type));
	}

	/// <summary>
	/// 指定模式的整条战斗序列；查不到时返回空数组（调用方按「无下一场」处理）。
	/// </summary>
	public OWEndlessSplitBattle[] SplitBattles(int areaScoreConfigId, int type)
	{
		OWEndlessAreaScoreConfigExcel? row = FromKey(areaScoreConfigId, type);
		return row?.SplitBattles ?? System.Array.Empty<OWEndlessSplitBattle>();
	}

	public static string Describe(OWEndlessAreaScoreConfigExcel row)
	{
		string ids = row.SplitBattles == null
			? "-"
			: string.Join(",", row.SplitBattles.Select((OWEndlessSplitBattle b) =>
				(b.IsBoss ? "B" : "M") + b.GroupScoreId));
		return $"AreaScoreConfigID={row.AreaScoreConfigID} type={row.Type} battles=[{ids}]";
	}
}
