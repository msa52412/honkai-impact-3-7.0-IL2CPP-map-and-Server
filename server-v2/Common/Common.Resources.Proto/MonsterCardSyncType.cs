using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonsterCardSyncType
{
	[ProtoEnum(Name = "MONSTER_CARD_SYNC_TYPE_NONE")]
	MonsterCardSyncTypeNone,
	[ProtoEnum(Name = "MONSTER_CARD_SYNC_TYPE_LEVEL_UP")]
	MonsterCardSyncTypeLevelUp,
	[ProtoEnum(Name = "MONSTER_CARD_SYNC_TYPE_STAR_UP")]
	MonsterCardSyncTypeStarUp,
	[ProtoEnum(Name = "MONSTER_CARD_SYNC_TYPE_LEARN_TALENT")]
	MonsterCardSyncTypeLearnTalent,
	[ProtoEnum(Name = "MONSTER_CARD_SYNC_TYPE_NEW_CARD")]
	MonsterCardSyncTypeNewCard,
	[ProtoEnum(Name = "MONSTER_CARD_SYNC_TYPE_REFRESH")]
	MonsterCardSyncTypeRefresh,
	[ProtoEnum(Name = "MONSTER_CARD_SYNC_TYPE_ADD_FRAGMENT")]
	MonsterCardSyncTypeAddFragment
}
