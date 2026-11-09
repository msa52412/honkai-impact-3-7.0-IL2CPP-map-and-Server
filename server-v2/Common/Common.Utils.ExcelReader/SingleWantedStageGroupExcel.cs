using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class SingleWantedStageGroupExcel
{
	[JsonProperty("StageIDList")]
	public int[] StageIdList { get; set; }

	[JsonProperty("MPStageIDList")]
	public int[] MpStageIdList { get; set; }

	[JsonProperty("StageGroupThemeID")]
	public int StageGroupThemeId { get; set; }

	[JsonProperty("StageGroupDesc")]
	public HashName StageGroupDesc { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("StageGroupID")]
	public int StageGroupId { get; set; }
}
