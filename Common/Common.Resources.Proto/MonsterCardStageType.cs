using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonsterCardStageType
{
	[ProtoEnum(Name = "MONSTER_CARD_STAGE_TYPE_NONE")]
	MonsterCardStageTypeNone,
	[ProtoEnum(Name = "MONSTER_CARD_STAGE_TYPE_NORMAL")]
	MonsterCardStageTypeNormal,
	[ProtoEnum(Name = "MONSTER_CARD_STAGE_TYPE_BOSS")]
	MonsterCardStageTypeBoss,
	[ProtoEnum(Name = "MONSTER_CARD_STAGE_TYPE_EXP")]
	MonsterCardStageTypeExp,
	[ProtoEnum(Name = "MONSTER_CARD_STAGE_TYPE_TOWER")]
	MonsterCardStageTypeTower
}
