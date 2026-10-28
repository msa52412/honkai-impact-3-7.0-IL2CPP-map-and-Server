using System.Linq;

namespace Common.Utils.ExcelReader;

public class GodWarTalentData : BaseExcelReader<GodWarTalentData, GodWarTalentDataExcel>
{
	public override string FileName => "GodWarTalentData.json";

	public GodWarTalentDataExcel? FromId(int id)
	{
		return base.All.Where((GodWarTalentDataExcel gw) => gw.TalentId == id).FirstOrDefault();
	}
}
