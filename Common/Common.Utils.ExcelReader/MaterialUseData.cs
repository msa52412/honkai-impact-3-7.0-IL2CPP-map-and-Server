using System.Linq;

namespace Common.Utils.ExcelReader;

public class MaterialUseData : BaseExcelReader<MaterialUseData, MaterialUseDataExcel>
{
	public override string FileName => "MaterialUseData.json";

	public MaterialUseDataExcel? FromId(int id)
	{
		return base.All.Where((MaterialUseDataExcel x) => x.UseId == id).FirstOrDefault();
	}
}
