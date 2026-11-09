using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum IslandVentureDispatchCond
{
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_ID")]
	IslandVentureAvatarId = 1,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_EACH_LEVEL")]
	IslandVentureAvatarEachLevel,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_ONE_LEVEL")]
	IslandVentureAvatarOneLevel,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_TOTAL_LEVEL")]
	IslandVentureAvatarTotalLevel,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_EACH_STAR")]
	IslandVentureAvatarEachStar,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_ONE_STAR")]
	IslandVentureAvatarOneStar,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_NUM")]
	IslandVentureAvatarNum,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_ATTR_NUM")]
	IslandVentureAvatarAttrNum,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_CLASS_NUM")]
	IslandVentureAvatarClassNum,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_ATTR_NOT_SAME")]
	IslandVentureAvatarAttrNotSame,
	[ProtoEnum(Name = "ISLAND_VENTURE_AVATAR_CLASS_NOT_SAME")]
	IslandVentureAvatarClassNotSame
}
