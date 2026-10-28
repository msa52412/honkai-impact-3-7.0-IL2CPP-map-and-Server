using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RaffleRewardGrade
{
	[ProtoEnum(Name = "RAFFLE_REWARD_NONE")]
	RaffleRewardNone,
	[ProtoEnum(Name = "RAFFLE_REWARD_FIRST")]
	RaffleRewardFirst,
	[ProtoEnum(Name = "RAFFLE_REWARD_SECOND")]
	RaffleRewardSecond,
	[ProtoEnum(Name = "RAFFLE_REWARD_THIRD")]
	RaffleRewardThird
}
