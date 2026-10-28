using System.Linq;

namespace Common.Utils.ExcelReader;

public class ExtraStoryAct : BaseExcelReader<ExtraStoryAct, ExtraStoryActExcel>
{
	public override string FileName => "ExtraStoryAct.json";

	public ExtraStoryActExcel? FromId(int id)
	{
		return base.All.Where((ExtraStoryActExcel Act) => Act.ActId == id).FirstOrDefault();
	}
}
