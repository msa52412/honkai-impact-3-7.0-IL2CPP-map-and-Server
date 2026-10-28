using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarTriggerType
{
	[ProtoEnum(Name = "GOD_WAR_TRIGGER_NORMAL")]
	GodWarTriggerNormal,
	[ProtoEnum(Name = "GOD_WAR_TRIGGER_STAGE_REPORT")]
	GodWarTriggerStageReport,
	[ProtoEnum(Name = "GOD_WAR_TRIGGER_NPC_ACTION")]
	GodWarTriggerNpcAction,
	[ProtoEnum(Name = "GOD_WAR_TRIGGER_OVERALL_REPORT")]
	GodWarTriggerOverallReport
}
