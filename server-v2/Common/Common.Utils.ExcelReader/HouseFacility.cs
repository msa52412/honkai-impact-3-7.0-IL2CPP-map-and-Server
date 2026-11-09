using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class HouseFacility
{
	[JsonProperty("col")]
	public int Col { get; set; }

	[JsonProperty("dir")]
	public int Dir { get; set; }

	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("row")]
	public int Row { get; set; }
}
