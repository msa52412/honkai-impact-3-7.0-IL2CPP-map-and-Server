using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ChallengeList
{
	[JsonProperty("challengeId")]
	public int ChallengeId { get; set; }

	[JsonProperty("rewardId")]
	public int RewardId { get; set; }
}
