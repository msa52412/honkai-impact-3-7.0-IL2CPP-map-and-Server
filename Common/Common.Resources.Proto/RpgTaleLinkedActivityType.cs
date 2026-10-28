using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleLinkedActivityType
{
	[ProtoEnum(Name = "RPG_TALE_LINKED_NONE")]
	RpgTaleLinkedNone,
	[ProtoEnum(Name = "RPG_TALE_LINKED_ROGUE_TOWER")]
	RpgTaleLinkedRogueTower,
	[ProtoEnum(Name = "RPG_TALE_LINKED_NEWBIE")]
	RpgTaleLinkedNewbie,
	[ProtoEnum(Name = "RPG_TALE_LINKED_RPG_DUNGEON")]
	RpgTaleLinkedRpgDungeon,
	[ProtoEnum(Name = "RPG_TALE_LINKED_RANCH")]
	RpgTaleLinkedRanch,
	[ProtoEnum(Name = "RPG_TALE_LINKED_RESTAURANT")]
	RpgTaleLinkedRestaurant,
	[ProtoEnum(Name = "RPG_TALE_LINKED_TOWN")]
	RpgTaleLinkedTown,
	[ProtoEnum(Name = "RPG_TALE_LINKED_MONSTER_CARD")]
	RpgTaleLinkedMonsterCard,
	[ProtoEnum(Name = "RPG_TALE_LINKED_DICEY_DUNGEON")]
	RpgTaleLinkedDiceyDungeon,
	[ProtoEnum(Name = "RPG_TALE_LINKED_PVZ")]
	RpgTaleLinkedPvz,
	[ProtoEnum(Name = "RPG_TALE_LINKED_QCANDY_BATTLE")]
	RpgTaleLinkedQcandyBattle,
	[ProtoEnum(Name = "RPG_TALE_LINKED_GLOBAL_EXPLORE")]
	RpgTaleLinkedGlobalExplore,
	[ProtoEnum(Name = "RPG_TALE_LINKED_PLATFORM_SHOOTER")]
	RpgTaleLinkedPlatformShooter,
	[ProtoEnum(Name = "RPG_TALE_LINKED_AVATAR_CULTIVATE")]
	RpgTaleLinkedAvatarCultivate,
	[ProtoEnum(Name = "RPG_TALE_LINKED_SUSANNA_TRIAL")]
	RpgTaleLinkedSusannaTrial,
	[ProtoEnum(Name = "RPG_TALE_LINKED_MATCH_THREE")]
	RpgTaleLinkedMatchThree,
	[ProtoEnum(Name = "RPG_TALE_LINKED_SHIGURE_KIRA")]
	RpgTaleLinkedShigureKira
}
