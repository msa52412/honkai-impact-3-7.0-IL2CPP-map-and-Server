using System.Linq;

namespace Common.Utils.ExcelReader;

public class StageData : BaseExcelReader<StageData, StageDataExcel>
{
	public override string FileName => "StageData_Main.json";

	public StageDataExcel? FromId(int id)
	{
		return base.All.Where((StageDataExcel stage) => stage.LevelId == id).FirstOrDefault();
	}
}
