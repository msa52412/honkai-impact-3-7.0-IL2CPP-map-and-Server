using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ExtraStoryChapterExcel
{
	[JsonProperty("minPlayerLevel")]
	public int MinPlayerLevel { get; set; }

	[JsonProperty("currenceIDList")]
	public int[] CurrenceIDList { get; set; }

	[JsonProperty("chapterType")]
	public int ChapterType { get; set; }

	[JsonProperty("shopType")]
	public int ShopType { get; set; }

	[JsonProperty("chapterId")]
	public int ChapterId { get; set; }
}
