using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaActivityType
{
	[ProtoEnum(Name = "ARMADA_ACTIVITY_TYPE_NONE")]
	ArmadaActivityTypeNone,
	[ProtoEnum(Name = "ARMADA_ACTIVITY_ARMADA_BOSS")]
	ArmadaActivityArmadaBoss,
	[ProtoEnum(Name = "ARMADA_ACTIVITY_MATRIX")]
	ArmadaActivityMatrix,
	[ProtoEnum(Name = "ARMADA_ACTIVITY_QUESTION")]
	ArmadaActivityQuestion,
	[ProtoEnum(Name = "ARMADA_ACTIVITY_TRADING_CARD")]
	ArmadaActivityTradingCard,
	[ProtoEnum(Name = "ARMADA_ACTIVITY_ARMADA_STAGE_SCORE_ACTIVITY")]
	ArmadaActivityArmadaStageScoreActivity,
	[ProtoEnum(Name = "ARMADA_ACTIVITY_REUNION")]
	ArmadaActivityReunion
}
