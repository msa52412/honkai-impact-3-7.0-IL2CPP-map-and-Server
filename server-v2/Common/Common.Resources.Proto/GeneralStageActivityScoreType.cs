using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralStageActivityScoreType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCORE_ACCUM")]
	GeneralActivityScoreAccum = 1,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCORE_TOP")]
	GeneralActivityScoreTop,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCORE_SINGLE_TOP")]
	GeneralActivityScoreSingleTop,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCORE_MATERIAL_NUM")]
	GeneralActivityScoreMaterialNum
}
