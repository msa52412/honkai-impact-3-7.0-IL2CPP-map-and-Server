using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RanchMonsterSkillType
{
	[ProtoEnum(Name = "RANCH_MONSTER_SKILL_ACTIVE")]
	RanchMonsterSkillActive = 1,
	[ProtoEnum(Name = "RANCH_MONSTER_SKILL_PASSIVE")]
	RanchMonsterSkillPassive,
	[ProtoEnum(Name = "RANCH_MONSTER_SKILL_FAST_PRODUCTION")]
	RanchMonsterSkillFastProduction,
	[ProtoEnum(Name = "RANCH_MONSTER_SKILL_RARITY_UP")]
	RanchMonsterSkillRarityUp
}
