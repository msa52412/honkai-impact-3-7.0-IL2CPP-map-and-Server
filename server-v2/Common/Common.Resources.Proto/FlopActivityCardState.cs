using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FlopActivityCardState
{
	[ProtoEnum(Name = "FLOP_ACTIVITY_CARD_STATE_NOT_FLOP")]
	FlopActivityCardStateNotFlop,
	[ProtoEnum(Name = "FLOP_ACTIVITY_CARD_STATE_FLOP")]
	FlopActivityCardStateFlop,
	[ProtoEnum(Name = "FLOP_ACTIVITY_CARD_STATE_REWARD_UNLOCK")]
	FlopActivityCardStateRewardUnlock
}
