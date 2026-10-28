using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ClientDataType
{
	[ProtoEnum(Name = "CLIENT_DATA_NONE")]
	ClientDataNone,
	[ProtoEnum(Name = "CLIENT_DATA_ALL")]
	ClientDataAll,
	[ProtoEnum(Name = "CLIENT_DATA_RPG_TALE")]
	ClientDataRpgTale,
	[ProtoEnum(Name = "CLIENT_DATA_WORLDMAP")]
	ClientDataWorldmap,
	[ProtoEnum(Name = "CLIENT_DATA_KINGDOM_WAR")]
	ClientDataKingdomWar,
	[ProtoEnum(Name = "CLIENT_DATA_AUGMENT_PERFORMANCE")]
	ClientDataAugmentPerformance,
	[ProtoEnum(Name = "CLIENT_DATA_AVATAR")]
	ClientDataAvatar,
	[ProtoEnum(Name = "CLIENT_DATA_OPENWORLD_CHAPTER")]
	ClientDataOpenworldChapter,
	[ProtoEnum(Name = "CLIENT_DATA_OPENWORLD")]
	ClientDataOpenworld,
	[ProtoEnum(Name = "CLIENT_DATA_NINJA")]
	ClientDataNinja,
	[ProtoEnum(Name = "CLIENT_DATA_AVATAR_TUTORIAL")]
	ClientDataAvatarTutorial,
	[ProtoEnum(Name = "CLIENT_DATA_PHONE_NOTICE")]
	ClientDataPhoneNotice,
	[ProtoEnum(Name = "CLIENT_DATA_NEWBIE_ACTIVITY")]
	ClientDataNewbieActivity,
	[ProtoEnum(Name = "CLIENT_DATA_CHATWORLD")]
	ClientDataChatworld,
	[ProtoEnum(Name = "CLIENT_DATA_MAINSTORY_LEVEL_SPECIAL_JOB")]
	ClientDataMainstoryLevelSpecialJob,
	[ProtoEnum(Name = "CLIENT_DATA_MEMOIRS")]
	ClientDataMemoirs,
	[ProtoEnum(Name = "CLIENT_DATA_GODWAR")]
	ClientDataGodwar,
	[ProtoEnum(Name = "CLIENT_DATA_THEME_TOWER")]
	ClientDataThemeTower
}
