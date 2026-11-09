using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EnterTimesType
{
	[ProtoEnum(Name = "ACITIVYT_TIMES_REF")]
	AcitivytTimesRef = 1,
	[ProtoEnum(Name = "STAGE_TIMES_REF")]
	StageTimesRef
}
