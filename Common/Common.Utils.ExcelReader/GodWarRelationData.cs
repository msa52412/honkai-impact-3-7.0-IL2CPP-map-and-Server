using System.Linq;

namespace Common.Utils.ExcelReader;

public class GodWarRelationData : BaseExcelReader<GodWarRelationData, GodWarRelationDataExcel>
{
	public override string FileName => "GodWarRelationData.json";

	public GodWarRelationDataExcel? FromId(int id)
	{
		return base.All.Where((GodWarRelationDataExcel gwr) => gwr.RoleId == id).FirstOrDefault();
	}
}
