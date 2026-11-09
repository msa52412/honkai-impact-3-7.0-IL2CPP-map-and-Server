using System.Linq;

namespace Common.Utils.ExcelReader;

public class ExtraStoryArea : BaseExcelReader<ExtraStoryArea, ExtraStoryAreaExcel>
{
	public override string FileName => "ExtraStoryArea.json";

	public ExtraStoryAreaExcel? FromId(int id)
	{
		return base.All.Where((ExtraStoryAreaExcel Area) => Area.AreaId == id).FirstOrDefault();
	}
}
