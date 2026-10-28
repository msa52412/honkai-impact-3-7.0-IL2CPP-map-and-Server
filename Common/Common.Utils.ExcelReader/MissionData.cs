using System.Linq;

namespace Common.Utils.ExcelReader;

public class MissionData : BaseExcelReader<MissionData, MissionDataExcel>
{
	public override string FileName => "MissionData.json";

	public MissionDataExcel? FromId(int id)
	{
		return base.All.Where((MissionDataExcel mission) => mission.Id == id).FirstOrDefault();
	}
}
