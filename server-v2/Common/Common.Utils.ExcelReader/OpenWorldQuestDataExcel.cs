using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OpenWorldQuestData.json（7052 行）里委托服务端真正用得到的字段。
/// 只声明需要的：这张表字段有 30 个，全映射会让反序列化变慢且容易因为类型猜错而炸。
/// 主键是 questId（表里没有单独的 ID 列）。
/// </summary>
public class OpenWorldQuestDataExcel
{
	[JsonProperty("questId")]
	public int QuestId { get; set; }

	[JsonProperty("questMap")]
	public int QuestMap { get; set; }

	[JsonProperty("questType")]
	public int QuestType { get; set; }

	[JsonProperty("questSubType")]
	public int QuestSubType { get; set; }

	[JsonProperty("questArea")]
	public int QuestArea { get; set; }

	[JsonProperty("rarity")]
	public int Rarity { get; set; }

	[JsonProperty("LevelLockNeed")]
	public int LevelLockNeed { get; set; }

	[JsonProperty("slotCost")]
	public int SlotCost { get; set; }

	[JsonProperty("rewardId")]
	public int RewardId { get; set; }

	[JsonProperty("rewardStar")]
	public int RewardStar { get; set; }

	[JsonProperty("staminaCost")]
	public int StaminaCost { get; set; }

	[JsonProperty("IsActivityQuest")]
	public bool IsActivityQuest { get; set; }

	[JsonProperty("ActivityScore")]
	public int ActivityScore { get; set; }

	[JsonProperty("challengeList")]
	public int[] ChallengeList { get; set; }

	[JsonProperty("finishWay")]
	public int FinishWay { get; set; }

	[JsonProperty("progress")]
	public int Progress { get; set; }
}
