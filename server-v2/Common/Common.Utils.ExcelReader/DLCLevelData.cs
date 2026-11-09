namespace Common.Utils.ExcelReader;

/// <summary>
/// DLCLevelData.json（30 行，level 1..30）—— 后崩坏书的「调查等级」表。
/// exp 是「从本等级升到下一级需要的经验」。
/// </summary>
public class DLCLevelData : BaseExcelReader<DLCLevelData, DLCLevelDataExcel>
{
	public override string FileName => "DLCLevelData.json";

	/// <summary>表里的最高等级。</summary>
	public uint MaxLevel
	{
		get
		{
			uint max = 1u;
			foreach (DLCLevelDataExcel row in All)
			{
				if ((uint)row.Level > max)
				{
					max = (uint)row.Level;
				}
			}
			return max;
		}
	}
}
