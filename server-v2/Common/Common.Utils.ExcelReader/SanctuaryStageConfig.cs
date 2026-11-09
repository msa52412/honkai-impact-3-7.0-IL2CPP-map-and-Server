using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>SanctuaryStageConfig.json 的读取器（家园小队远征关卡）。</summary>
public class SanctuaryStageConfig : BaseExcelReader<SanctuaryStageConfig, SanctuaryStageConfigExcel>
{
	public override string FileName => "SanctuaryStageConfig.json";

	public SanctuaryStageConfigExcel? FromId(int stageId)
	{
		return base.All?.FirstOrDefault((SanctuaryStageConfigExcel x) => x.StageID == stageId);
	}

	/// <summary>该关卡的经验奖励区间；查不到返回 (0, 0)。</summary>
	public (int min, int max) ExpRange(int stageId)
	{
		SanctuaryStageConfigExcel? row = FromId(stageId);
		return row == null ? (0, 0) : (row.RewardExpMin, row.RewardExpMax);
	}
}
