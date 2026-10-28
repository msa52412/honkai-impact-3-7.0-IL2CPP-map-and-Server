using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarEventCacheType
{
	[ProtoEnum(Name = "GOD_WAR_EVENT_CACHE_REWARD")]
	GodWarEventCacheReward = 1,
	[ProtoEnum(Name = "GOD_WAR_EVENT_CACHE_OVERALL")]
	GodWarEventCacheOverall,
	[ProtoEnum(Name = "GOD_WAR_EVENT_CACHE_MATERIAL")]
	GodWarEventCacheMaterial,
	[ProtoEnum(Name = "GOD_WAR_EVENT_CACHE_SCORE")]
	GodWarEventCacheScore,
	[ProtoEnum(Name = "GOD_WAR_EVENT_CACHE_LOBBY_NPC")]
	GodWarEventCacheLobbyNpc,
	[ProtoEnum(Name = "GOD_WAR_EVENT_CACHE_COIN")]
	GodWarEventCacheCoin
}
