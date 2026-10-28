using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class TagList
{
	[JsonProperty("Strength")]
	public int Strength { get; set; }

	[JsonProperty("TagID")]
	public int TagId { get; set; }

	[JsonProperty("TagComment")]
	public HashName TagComment { get; set; }
}
