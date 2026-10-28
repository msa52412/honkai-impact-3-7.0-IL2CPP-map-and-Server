using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpSkillType
{
	[ProtoEnum(Name = "MP_SKILL_NONE")]
	MpSkillNone,
	[ProtoEnum(Name = "MP_SKILL_ATTACK")]
	MpSkillAttack,
	[ProtoEnum(Name = "MP_SKILL_DEFENCE")]
	MpSkillDefence,
	[ProtoEnum(Name = "MP_SKILL_OTHER")]
	MpSkillOther
}
