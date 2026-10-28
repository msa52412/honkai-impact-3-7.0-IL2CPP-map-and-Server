using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RanchSynthesisType
{
	[ProtoEnum(Name = "RANCH_SYNSHESIS_WITHOUT_FORMULA")]
	RanchSynshesisWithoutFormula = 1,
	[ProtoEnum(Name = "RANCH_SYNSHESIS_WITH_FORMULA")]
	RanchSynshesisWithFormula
}
