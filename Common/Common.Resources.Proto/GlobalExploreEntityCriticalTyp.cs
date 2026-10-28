using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreEntityCriticalType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_CRITICAL_STAGE")]
	GlobalExploreEntityCriticalStage = 1001,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_CRITICAL_QUEST")]
	GlobalExploreEntityCriticalQuest = 1002,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_CRITICAL_WATCHTOWER")]
	GlobalExploreEntityCriticalWatchtower = 1003,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_CRITICAL_BOSS")]
	GlobalExploreEntityCriticalBoss = 1005
}
