using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class MissionDataExcel
{
	[JsonProperty("type")]
	public int Type { get; set; }

	[JsonProperty("subType")]
	public int SubType { get; set; }

	[JsonProperty("title")]
	public HashName Title { get; set; }

	[JsonProperty("description")]
	public HashName Description { get; set; }

	[JsonProperty("thumb")]
	public string Thumb { get; set; }

	[JsonProperty("finishWay")]
	public int FinishWay { get; set; }

	[JsonProperty("finishParaInt")]
	public int FinishParaInt { get; set; }

	[JsonProperty("finishParaStr")]
	public string FinishParaStr { get; set; }

	[JsonProperty("totalProgress")]
	public int TotalProgress { get; set; }

	[JsonProperty("rewardId")]
	public int RewardId { get; set; }

	[JsonProperty("LinkType")]
	public int LinkType { get; set; }

	[JsonProperty("LinkParams")]
	public int[] LinkParams { get; set; }

	[JsonProperty("TextmapTag")]
	public HashName TextmapTag { get; set; }

	[JsonProperty("LinkParamStr")]
	public string LinkParamStr { get; set; }

	[JsonProperty("PreviewTime")]
	public int PreviewTime { get; set; }

	[JsonProperty("NoDisplay")]
	public bool NoDisplay { get; set; }

	[JsonProperty("IsNeedDisplay")]
	public bool IsNeedDisplay { get; set; }

	[JsonProperty("Priority")]
	public int Priority { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("id")]
	public int Id { get; set; }
}
