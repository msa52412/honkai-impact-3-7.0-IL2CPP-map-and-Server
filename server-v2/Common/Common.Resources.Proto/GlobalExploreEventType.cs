using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreEventType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_DICE")]
	GlobalExploreEventDice = 1,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_DROP_MATERIAL")]
	GlobalExploreEventDropMaterial,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_REPLACE_ENTITY")]
	GlobalExploreEventReplaceEntity,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_DESTROY_ENTITY")]
	GlobalExploreEventDestroyEntity,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_TELEPORT_RANDOM")]
	GlobalExploreEventTeleportRandom,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_CHANGE_GRID")]
	GlobalExploreEventChangeGrid,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_ACCEPT_QUEST")]
	GlobalExploreEventAcceptQuest,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_TELEPORT_SPECIFIC")]
	GlobalExploreEventTeleportSpecific,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_RECOVER_STAMINA")]
	GlobalExploreEventRecoverStamina,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_ENTER_STAGE")]
	GlobalExploreEventEnterStage,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_ADD_QUEST_PROGRESS")]
	GlobalExploreEventAddQuestProgress,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_CHANGE_GRID_RANDOM")]
	GlobalExploreEventChangeGridRandom,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_DROP_MATERIAL_RANDOM")]
	GlobalExploreEventDropMaterialRandom,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_CREATE_ENTITY_RANDOM")]
	GlobalExploreEventCreateEntityRandom,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_MOVE_CAMERA")]
	GlobalExploreEventMoveCamera,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_TEXT_MAP_HINT")]
	GlobalExploreEventTextMapHint,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_TRIGGER_PLOT")]
	GlobalExploreEventTriggerPlot,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_START_TUTORIAL")]
	GlobalExploreEventStartTutorial,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_ZOOM_MAP")]
	GlobalExploreEventZoomMap
}
