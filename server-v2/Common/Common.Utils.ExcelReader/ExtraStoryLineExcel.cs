using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ExtraStoryLineExcel
{
	[JsonProperty("storyChapterId")]
	public int storyChapterId { get; set; }

	[JsonProperty("challengeChapterId")]
	public int challengeChapterId { get; set; }

	[JsonProperty("unlockStageId")]
	public int unlockStageId { get; set; }

	[JsonProperty("storyFinshReward")]
	public int storyFinshReward { get; set; }

	[JsonProperty("RewardShow")]
	public int RewardShow { get; set; }

	[JsonProperty("challengeRewardShow")]
	public int challengeRewardShow { get; set; }

	[JsonProperty("storyLineId")]
	public int storyLineId { get; set; }
}
