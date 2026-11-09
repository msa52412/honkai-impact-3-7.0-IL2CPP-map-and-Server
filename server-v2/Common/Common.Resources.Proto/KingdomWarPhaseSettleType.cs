using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum KingdomWarPhaseSettleType
{
	[ProtoEnum(Name = "KINGDOM_WAR_PHASE_SETTLE_TYPE_NONE")]
	KingdomWarPhaseSettleTypeNone,
	[ProtoEnum(Name = "KINGDOM_WAR_PHASE_SETTLE_TYPE_FAME")]
	KingdomWarPhaseSettleTypeFame,
	[ProtoEnum(Name = "KINGDOM_WAR_PHASE_SETTLE_TYPE_BELIEF")]
	KingdomWarPhaseSettleTypeBelief
}
