using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarSkillTriggerType
{
	[ProtoEnum(Name = "QAVATAR_SKILL_TRIGGER_NONE")]
	QavatarSkillTriggerNone,
	[ProtoEnum(Name = "QAVATAR_SKILL_TRIGGER_DOWN")]
	QavatarSkillTriggerDown,
	[ProtoEnum(Name = "QAVATAR_SKILL_TRIGGER_UP")]
	QavatarSkillTriggerUp,
	[ProtoEnum(Name = "QAVATAR_SKILL_TRIGGER_HOLDUP")]
	QavatarSkillTriggerHoldup,
	[ProtoEnum(Name = "QAVATAR_SKILL_TRIGGER_HOLDLOOP")]
	QavatarSkillTriggerHoldloop
}
