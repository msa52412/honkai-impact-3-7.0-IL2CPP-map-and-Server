using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreEntityType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_REWARD")]
	GlobalExploreEntityReward = 1,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_DICE")]
	GlobalExploreEntityDice,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_STAGE")]
	GlobalExploreEntityStage,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_PLOT")]
	GlobalExploreEntityPlot,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_TELEPORT")]
	GlobalExploreEntityTeleport,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ENTITY_EVENT")]
	GlobalExploreEntityEvent
}
