using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ExtraStoryAreaExcel
{
	[JsonProperty("chapterId")]
	public int ChapterId { get; set; }

	[JsonProperty("withMap")]
	public int WithMap { get; set; }

	[JsonProperty("AreaId")]
	public int AreaId { get; set; }
}
