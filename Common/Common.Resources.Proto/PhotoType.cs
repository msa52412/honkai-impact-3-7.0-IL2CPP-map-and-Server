using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PhotoType
{
	[ProtoEnum(Name = "PHOTO_TYPE_ALL")]
	PhotoTypeAll,
	[ProtoEnum(Name = "PHOTO_TYPE_CITY_ACTIVITY")]
	PhotoTypeCityActivity,
	[ProtoEnum(Name = "PHOTO_TYPE_ANNIVERSARY")]
	PhotoTypeAnniversary,
	[ProtoEnum(Name = "PHOTO_TYPE_RPG_TALE")]
	PhotoTypeRpgTale,
	[ProtoEnum(Name = "PHOTO_TYPE_FARM")]
	PhotoTypeFarm,
	[ProtoEnum(Name = "PHOTO_TYPE_KINGDOM_WAR")]
	PhotoTypeKingdomWar,
	[ProtoEnum(Name = "PHOTO_TYPE_DLC")]
	PhotoTypeDlc,
	[ProtoEnum(Name = "PHOTO_TYPE_GLOBAL_WAR")]
	PhotoTypeGlobalWar,
	[ProtoEnum(Name = "PHOTO_TYPE_NINJA")]
	PhotoTypeNinja,
	[ProtoEnum(Name = "PHOTO_TYPE_CHAPTER_HIDDEN")]
	PhotoTypeChapterHidden,
	[ProtoEnum(Name = "PHOTO_TYPE_OW_AVATAR")]
	PhotoTypeOwAvatar,
	[ProtoEnum(Name = "PHOTO_TYPE_SLG")]
	PhotoTypeSlg,
	[ProtoEnum(Name = "PHOTO_TYPE_GOD_WAR")]
	PhotoTypeGodWar,
	[ProtoEnum(Name = "PHOTO_TYPE_OPENWORLD_CHAPTER")]
	PhotoTypeOpenworldChapter,
	[ProtoEnum(Name = "PHOTO_TYPE_SC_DLC")]
	PhotoTypeScDlc,
	[ProtoEnum(Name = "PHOTO_TYPE_OW_HUNT")]
	PhotoTypeOwHunt,
	[ProtoEnum(Name = "PHOTO_TYPE_GLOBAL_EXPLORE")]
	PhotoTypeGlobalExplore
}
