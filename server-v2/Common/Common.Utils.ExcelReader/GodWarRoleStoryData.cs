using System.Linq;

namespace Common.Utils.ExcelReader;

public class GodWarRoleStoryData : BaseExcelReader<GodWarRoleStoryData, GodWarRoleStoryDataExcel>
{
	public override string FileName => "GodWarRoleStoryData.json";

	public GodWarRoleStoryDataExcel? FromId(int id)
	{
		return base.All.Where((GodWarRoleStoryDataExcel gwrsde) => gwrsde.RoleId == id).FirstOrDefault();
	}
}
