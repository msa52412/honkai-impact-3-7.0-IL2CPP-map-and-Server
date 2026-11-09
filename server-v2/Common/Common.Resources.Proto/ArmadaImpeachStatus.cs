using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaImpeachStatus
{
	[ProtoEnum(Name = "ARMADA_IMPEACH_STATUS_NORMAL")]
	ArmadaImpeachStatusNormal,
	[ProtoEnum(Name = "ARMADA_IMPEACH_STATUS_IMPEACH")]
	ArmadaImpeachStatusImpeach,
	[ProtoEnum(Name = "ARMADA_IMPEACH_STATUS_ELECTION")]
	ArmadaImpeachStatusElection
}
