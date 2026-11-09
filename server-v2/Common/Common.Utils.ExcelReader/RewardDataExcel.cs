using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class RewardDataExcel
{
	[JsonProperty("RewardExp")]
	public int RewardExp { get; set; }

	[JsonProperty("RewardHCoin")]
	public int RewardHCoin { get; set; }

	[JsonProperty("RewardStamina")]
	public int RewardStamina { get; set; }

	[JsonProperty("RewardFriendPoint")]
	public int RewardFriendPoint { get; set; }

	[JsonProperty("RewardDutyPoint")]
	public int RewardDutyPoint { get; set; }

	[JsonProperty("RewardItem1ID")]
	public int RewardItem1Id { get; set; }

	[JsonProperty("RewardItem1Level")]
	public int RewardItem1Level { get; set; }

	[JsonProperty("RewardItem1Num")]
	public int RewardItem1Num { get; set; }

	[JsonProperty("RewardItem2ID")]
	public int RewardItem2Id { get; set; }

	[JsonProperty("RewardItem2Level")]
	public int RewardItem2Level { get; set; }

	[JsonProperty("RewardItem2Num")]
	public int RewardItem2Num { get; set; }

	[JsonProperty("RewardItem3ID")]
	public int RewardItem3Id { get; set; }

	[JsonProperty("RewardItem3Level")]
	public int RewardItem3Level { get; set; }

	[JsonProperty("RewardItem3Num")]
	public int RewardItem3Num { get; set; }

	[JsonProperty("RewardItem4ID")]
	public int RewardItem4Id { get; set; }

	[JsonProperty("RewardItem4Level")]
	public int RewardItem4Level { get; set; }

	[JsonProperty("RewardItem4Num")]
	public int RewardItem4Num { get; set; }

	[JsonProperty("RewardItem5ID")]
	public int RewardItem5Id { get; set; }

	[JsonProperty("RewardItem5Level")]
	public int RewardItem5Level { get; set; }

	[JsonProperty("RewardItem5Num")]
	public int RewardItem5Num { get; set; }

	[JsonProperty("RewardItem6ID")]
	public int RewardItem6Id { get; set; }

	[JsonProperty("RewardItem6Level")]
	public int RewardItem6Level { get; set; }

	[JsonProperty("RewardItem6Num")]
	public int RewardItem6Num { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("RewardID")]
	public int RewardId { get; set; }
}
