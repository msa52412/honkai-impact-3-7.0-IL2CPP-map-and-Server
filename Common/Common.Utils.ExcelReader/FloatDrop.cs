using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class FloatDrop
{
	[JsonProperty("materialId")]
	public long MaterialId { get; set; }

	[JsonProperty("maxNum")]
	public long MaxNum { get; set; }

	[JsonProperty("minNum")]
	public long MinNum { get; set; }
}
