using System.Linq;

namespace Common.Utils.ExcelReader;

public class OpenWorldStoryData : BaseExcelReader<OpenWorldStoryData, OpenWorldStoryDataExcel>
{
	public override string FileName => "OpenWorldStoryData.json";

	public OpenWorldStoryDataExcel? FromId(int id)
	{
		return base.All.Where((OpenWorldStoryDataExcel story) => story.StoryId == id).FirstOrDefault();
	}
}
