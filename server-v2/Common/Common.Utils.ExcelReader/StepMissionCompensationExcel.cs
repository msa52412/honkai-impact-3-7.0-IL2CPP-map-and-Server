using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class StepMissionCompensationExcel
{
	[JsonProperty("UnlockLevel")]
	public int UnlockLevel { get; set; }

	[JsonProperty("MainLineStepIDList")]
	public int[] MainLineStepIdList { get; set; }

	[JsonProperty("NewChallengeStepIDList")]
	public int[] NewChallengeStepIdList { get; set; }

	[JsonProperty("OldChallengeStepIDList")]
	public int[] OldChallengeStepIdList { get; set; }

	[JsonProperty("CompensationID")]
	public int CompensationId { get; set; }
}
