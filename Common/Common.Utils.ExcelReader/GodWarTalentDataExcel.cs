using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarTalentDataExcel
{
	[JsonProperty("MaxLevel")]
	public int MaxLevel { get; set; }

	[JsonProperty("TalentID")]
	public int TalentId { get; set; }
}
