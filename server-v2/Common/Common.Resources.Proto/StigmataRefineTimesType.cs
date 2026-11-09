using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StigmataRefineTimesType
{
	[ProtoEnum(Name = "STIGMATA_REFINE_TIMES_ONE")]
	StigmataRefineTimesOne = 1,
	[ProtoEnum(Name = "STIGMATA_REFINE_TIMES_TEN")]
	StigmataRefineTimesTen = 10
}
