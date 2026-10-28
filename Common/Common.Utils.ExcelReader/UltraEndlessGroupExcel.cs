using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class UltraEndlessGroupExcel
{
	[JsonProperty("GroupID")]
	public int GroupId { get; set; }

	[JsonProperty("GroupName")]
	public string GroupName { get; set; }

	[JsonProperty("NeedCupNum")]
	public int NeedCupNum { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("GroupLevel")]
	public int GroupLevel { get; set; }
}
