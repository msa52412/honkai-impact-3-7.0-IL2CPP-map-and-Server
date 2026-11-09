using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarSkillType
{
	[ProtoEnum(Name = "QAVATAR_SKILL_NONE")]
	QavatarSkillNone,
	[ProtoEnum(Name = "QAVATAR_SKILL_ATK")]
	QavatarSkillAtk,
	[ProtoEnum(Name = "QAVATAR_SKILL_SKL01")]
	QavatarSkillSkl01,
	[ProtoEnum(Name = "QAVATAR_SKILL_SKL02")]
	QavatarSkillSkl02
}
