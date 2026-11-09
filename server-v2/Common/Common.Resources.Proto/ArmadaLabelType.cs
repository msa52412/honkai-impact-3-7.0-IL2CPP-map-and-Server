using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaLabelType
{
	[ProtoEnum(Name = "ARMADA_LABEL_PREFIX")]
	ArmadaLabelPrefix = 1,
	[ProtoEnum(Name = "ARMADA_LABEL")]
	ArmadaLabel
}
