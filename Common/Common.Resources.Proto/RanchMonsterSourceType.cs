using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RanchMonsterSourceType
{
	[ProtoEnum(Name = "RANCH_MONSTER_SOURCE_STAGE")]
	RanchMonsterSourceStage = 1,
	[ProtoEnum(Name = "RANCH_MONSTER_SOURCE_MATERIAL")]
	RanchMonsterSourceMaterial,
	[ProtoEnum(Name = "RANCH_MONSTER_SOURCE_SYNTHESIS_WITH_FOMULA")]
	RanchMonsterSourceSynthesisWithFomula,
	[ProtoEnum(Name = "RANCH_MONSTER_SOURCE_PRODUCT")]
	RanchMonsterSourceProduct,
	[ProtoEnum(Name = "RANCH_MONSTER_SOURCE_SYNTHESIS_WITHOUT_FORMULA")]
	RanchMonsterSourceSynthesisWithoutFormula,
	[ProtoEnum(Name = "RANCH_MONSTER_SOURCE_GM")]
	RanchMonsterSourceGm
}
