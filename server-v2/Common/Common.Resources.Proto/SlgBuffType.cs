using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SlgBuffType
{
	[ProtoEnum(Name = "SLG_BUFF_TYPE_NONE")]
	SlgBuffTypeNone,
	[ProtoEnum(Name = "SLG_BUFF_TYPE_ACTIVITY_STAMINA_COST")]
	SlgBuffTypeActivityStaminaCost,
	[ProtoEnum(Name = "SLG_BUFF_TYPE_STAGE_SCORE")]
	SlgBuffTypeStageScore,
	[ProtoEnum(Name = "SLG_BUFF_TYPE_SETTLE_SCORE")]
	SlgBuffTypeSettleScore,
	[ProtoEnum(Name = "SLG_BUFF_TYPE_STAGE")]
	SlgBuffTypeStage
}
