using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ExtraStoryActExcel
{
	[JsonProperty("areaId")]
	public int AreaId { get; set; }

	[JsonProperty("stageIdList")]
	public int[] StageIdList { get; set; }

	[JsonProperty("location")]
	public int Location { get; set; }

	[JsonProperty("CgID")]
	public int CgId { get; set; }

	[JsonProperty("EnhanceOn")]
	public int EnhanceOn { get; set; }

	[JsonProperty("EnhanceReward")]
	public int EnhanceReward { get; set; }

	[JsonProperty("EnhanceRestrictList")]
	public int[] EnhanceRestrictList { get; set; }

	[JsonProperty("actId")]
	public int ActId { get; set; }
}
