using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaStageScoreActivityRewardType
{
	[ProtoEnum(Name = "ARMADA_STAGE_SCORE_ACTIVITY_REWARD_TYPE_NONE")]
	ArmadaStageScoreActivityRewardTypeNone,
	[ProtoEnum(Name = "ARMADA_STAGE_SCORE_ACTIVITY_REWARD_TYPE_SELF")]
	ArmadaStageScoreActivityRewardTypeSelf,
	[ProtoEnum(Name = "ARMADA_STAGE_SCORE_ACTIVITY_REWARD_TYPE_ARMADA")]
	ArmadaStageScoreActivityRewardTypeArmada
}
