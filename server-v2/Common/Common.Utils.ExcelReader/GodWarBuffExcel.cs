using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarBuffExcel
{
	[JsonProperty("BuffSuit")]
	public int BuffSuit { get; set; }

	[JsonProperty("BuffQuality")]
	public int BuffQuality { get; set; }

	[JsonProperty("BuffID")]
	public int BuffId { get; set; }

	[JsonProperty("BuffLevel")]
	public int BuffLevel { get; set; }
}
