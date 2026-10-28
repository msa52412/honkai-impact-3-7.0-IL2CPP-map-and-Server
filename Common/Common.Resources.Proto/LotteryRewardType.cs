using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LotteryRewardType
{
	[ProtoEnum(Name = "LOTTERY_REWARD_TYPE_INVALID")]
	LotteryRewardTypeInvalid,
	[ProtoEnum(Name = "LOTTERY_REWARD_TYPE_FIRST_REWARD")]
	LotteryRewardTypeFirstReward,
	[ProtoEnum(Name = "LOTTERY_REWARD_TYPE_SECOND_REWARD")]
	LotteryRewardTypeSecondReward,
	[ProtoEnum(Name = "LOTTERY_REWARD_TYPE_THIRD_REWARD")]
	LotteryRewardTypeThirdReward
}
