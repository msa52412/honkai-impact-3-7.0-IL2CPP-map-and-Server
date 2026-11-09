using System.Linq;

namespace Common.Utils.ExcelReader;

public class ExtraStoryChapter : BaseExcelReader<ExtraStoryChapter, ExtraStoryChapterExcel>
{
	public override string FileName => "ExtraStoryChapter.json";

	public ExtraStoryChapterExcel? FromId(int id)
	{
		return base.All.Where((ExtraStoryChapterExcel Chapter) => Chapter.ChapterId == id).FirstOrDefault();
	}
}
