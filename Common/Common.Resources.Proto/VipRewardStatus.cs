using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum VipRewardStatus
{
	[ProtoEnum(Name = "VIP_REWARD_LOCKED")]
	VipRewardLocked = 1,
	[ProtoEnum(Name = "VIP_REWARD_NOT_GET")]
	VipRewardNotGet,
	[ProtoEnum(Name = "VIP_REWARD_HAS_GOT")]
	VipRewardHasGot,
	[ProtoEnum(Name = "VIP_REWARD_HAS_GOT_BUT_SPECIAL")]
	VipRewardHasGotButSpecial
}
