using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonsterCardSelectCardCondition
{
	[ProtoEnum(Name = "MONSTER_CARD_SELECT_CARD_CONDITION_NONE")]
	MonsterCardSelectCardConditionNone,
	[ProtoEnum(Name = "MONSTER_CARD_SELECT_CARD_CONDITION_LEVEL_GREATER_EQUAL")]
	MonsterCardSelectCardConditionLevelGreaterEqual
}
