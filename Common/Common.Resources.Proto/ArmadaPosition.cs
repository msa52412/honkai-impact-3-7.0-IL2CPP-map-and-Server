using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaPosition
{
	[ProtoEnum(Name = "ARMADA_POSITION_NONE")]
	ArmadaPositionNone,
	[ProtoEnum(Name = "ARMADA_POSITION_LEADER")]
	ArmadaPositionLeader,
	[ProtoEnum(Name = "ARMADA_POSITION_VICELEADER")]
	ArmadaPositionViceleader,
	[ProtoEnum(Name = "ARMADA_POSITION_MEMBER")]
	ArmadaPositionMember
}
