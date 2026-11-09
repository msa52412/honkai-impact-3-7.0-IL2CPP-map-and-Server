using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum IslandEventCondType
{
	[ProtoEnum(Name = "ISLAND_EVENT_COND_ONE_WEATHER")]
	IslandEventCondOneWeather = 1,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_AVATAR_GOODFEEL")]
	IslandEventCondAvatarGoodfeel = 2,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_HAS_AVATAR")]
	IslandEventCondHasAvatar = 3,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_ROLE_AFFINITY")]
	IslandEventCondRoleAffinity = 4,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_ALL_AFFINITY")]
	IslandEventCondAllAffinity = 5,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_HAS_ONE_EQUIPMENT")]
	IslandEventCondHasOneEquipment = 201,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_AVATAR")]
	IslandEventCondAvatar = 202,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_FINISH_ONE_EVENT")]
	IslandEventCondFinishOneEvent = 203,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_POS")]
	IslandEventCondPos = 204,
	[ProtoEnum(Name = "ISLAND_EVENT_COND_SITE")]
	IslandEventCondSite = 205
}
