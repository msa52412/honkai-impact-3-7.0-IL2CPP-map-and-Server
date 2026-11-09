using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OuterRewardType
{
	[ProtoEnum(Name = "OUTER_REWARD_TYPE_NONE")]
	OuterRewardTypeNone,
	[ProtoEnum(Name = "OUTER_REWARD_TYPE_REDEEM")]
	OuterRewardTypeRedeem
}
