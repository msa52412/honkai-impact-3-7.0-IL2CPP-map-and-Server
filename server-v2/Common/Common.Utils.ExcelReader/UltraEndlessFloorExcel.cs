using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class UltraEndlessFloorExcel
{
	[JsonProperty("NeedScore")]
	public int NeedScore { get; set; }

	[JsonProperty("MaxScore")]
	public int MaxScore { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("FloorID")]
	public int FloorId { get; set; }

	[JsonProperty("StageID")]
	public int StageId { get; set; }
}
