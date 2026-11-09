using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonsterCardSkillType
{
	[ProtoEnum(Name = "MONSTER_CARD_SKILL_TYPE_NONE")]
	MonsterCardSkillTypeNone = 0,
	[ProtoEnum(Name = "MONSTER_CARD_SKILL_TYPE_ATK")]
	MonsterCardSkillTypeAtk = 1,
	[ProtoEnum(Name = "MONSTER_CARD_SKILL_TYPE_LB")]
	MonsterCardSkillTypeLb = 2,
	[ProtoEnum(Name = "MONSTER_CARD_SKILL_TYPE_AUTO")]
	MonsterCardSkillTypeAuto = 3,
	[ProtoEnum(Name = "MONSTER_CARD_SKILL_TYPE_PASSIVE")]
	MonsterCardSkillTypePassive = 4,
	[ProtoEnum(Name = "MONSTER_CARD_SKILL_TYPE_WEATHER")]
	MonsterCardSkillTypeWeather = 5,
	[ProtoEnum(Name = "MONSTER_CARD_SKILL_TYPE_TALENT")]
	MonsterCardSkillTypeTalent = 6,
	[ProtoEnum(Name = "MONSTER_CARD_SKILL_TYPE_UNLOCK")]
	MonsterCardSkillTypeUnlock = 99
}
