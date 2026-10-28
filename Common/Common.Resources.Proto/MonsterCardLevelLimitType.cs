using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonsterCardLevelLimitType
{
	[ProtoEnum(Name = "MONSTER_CARD_LEVEL_LIMIT_TYPE_NONE")]
	MonsterCardLevelLimitTypeNone,
	[ProtoEnum(Name = "MONSTER_CARD_LEVEL_LIMIT_TYPE_PASS_RPG_SITE")]
	MonsterCardLevelLimitTypePassRpgSite
}
