using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>OWEndlessMonsterGroupScore.json 读取器。</summary>
public class OWEndlessMonsterGroupScore : BaseExcelReader<OWEndlessMonsterGroupScore, OWEndlessMonsterGroupScoreExcel>
{
	public override string FileName => "OWEndlessMonsterGroupScore.json";

	public OWEndlessMonsterGroupScoreExcel? FromKey(int monsterGroupId)
	{
		return All.FirstOrDefault((OWEndlessMonsterGroupScoreExcel row) => row.MonsterGroupID == monsterGroupId);
	}

	/// <summary>取该组基础分；表里没有就返回 0（不让一场战斗因为缺一行而失败）。</summary>
	public uint ScoreOf(uint monsterGroupId)
	{
		OWEndlessMonsterGroupScoreExcel? row = FromKey((int)monsterGroupId);
		return row == null ? 0u : (uint)row.Score;
	}
}
