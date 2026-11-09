using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyBattleTriggerEventType
{
	[ProtoEnum(Name = "QCANDY_BATTLE_TRIGGER_EVENT_TYPE_ADD_SCORE")]
	QcandyBattleTriggerEventTypeAddScore = 1,
	[ProtoEnum(Name = "QCANDY_BATTLE_TRIGGER_EVENT_TYPE_SUB_SKILL_USE_TIMES")]
	QcandyBattleTriggerEventTypeSubSkillUseTimes
}
