using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SkillDamageCollisionType
{
	[ProtoEnum(Name = "SKILL_DAMAGE_COLLISION_TYPE_POINT")]
	SkillDamageCollisionTypePoint = 1,
	[ProtoEnum(Name = "SKILL_DAMAGE_COLLISION_TYPE_CIRCLE")]
	SkillDamageCollisionTypeCircle,
	[ProtoEnum(Name = "SKILL_DAMAGE_COLLISION_TYPE_RECT")]
	SkillDamageCollisionTypeRect,
	[ProtoEnum(Name = "SKILL_DAMAGE_COLLISION_TYPE_SECTOR")]
	SkillDamageCollisionTypeSector
}
