using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FlopActivityRewardState
{
	[ProtoEnum(Name = "FLOP_ACTIVITY_REWARD_STATE_LOCK")]
	FlopActivityRewardStateLock,
	[ProtoEnum(Name = "FLOP_ACTIVITY_REWARD_STATE_UNLOCK")]
	FlopActivityRewardStateUnlock,
	[ProtoEnum(Name = "FLOP_ACTIVITY_REWARD_STATE_HAS_GOT")]
	FlopActivityRewardStateHasGot
}
