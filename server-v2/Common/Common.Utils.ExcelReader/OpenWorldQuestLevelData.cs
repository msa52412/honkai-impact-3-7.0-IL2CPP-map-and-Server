namespace Common.Utils.ExcelReader;

/// <summary>
/// OpenWorldQuestLevelData.json（30 行，questLevel 1..30）。
/// levelupNeedStar 是「从本等级升到下一级需要的星数」，不是累计值。
/// </summary>
public class OpenWorldQuestLevelData : BaseExcelReader<OpenWorldQuestLevelData, OpenWorldQuestLevelDataExcel>
{
	public override string FileName => "OpenWorldQuestLevelData.json";
}
