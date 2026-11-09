using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreFlagType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_MATERIAL_NUM")]
	GlobalExploreFlagMaterialNum = 1,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_CUR_GRID")]
	GlobalExploreFlagCurGrid,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_ENTITY_INACTIVE")]
	GlobalExploreFlagEntityInactive,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_ENTITY_ACTIVE")]
	GlobalExploreFlagEntityActive,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_STAGE_COMPLETED")]
	GlobalExploreFlagStageCompleted,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_MISSION_COMPLETED")]
	GlobalExploreFlagMissionCompleted,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_GRID_ADJACENT")]
	GlobalExploreFlagGridAdjacent,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_QUEST_STATUS")]
	GlobalExploreFlagQuestStatus,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_FLAG_WITHOUT_MATERIAL")]
	GlobalExploreFlagWithoutMaterial
}
