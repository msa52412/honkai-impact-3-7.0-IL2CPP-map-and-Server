using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownBuildingSkillCondType
{
	[ProtoEnum(Name = "TOWN_BUILDING_COND_NONE")]
	TownBuildingCondNone,
	[ProtoEnum(Name = "TOWN_BUILDING_COND_SIDE_BUILDING")]
	TownBuildingCondSideBuilding,
	[ProtoEnum(Name = "TOWN_BUILDING_COND_SIDE_BUILDING_MAX_NUM")]
	TownBuildingCondSideBuildingMaxNum,
	[ProtoEnum(Name = "TOWN_BUILDING_COND_NO_SIDE_BUILDING")]
	TownBuildingCondNoSideBuilding,
	[ProtoEnum(Name = "TOWN_BUILDING_COND_SIDE_ROAD")]
	TownBuildingCondSideRoad,
	[ProtoEnum(Name = "TOWN_BUILDING_COND_TOTAL_BUILDING_NUM")]
	TownBuildingCondTotalBuildingNum,
	[ProtoEnum(Name = "TOWN_BUILDING_COND_PASS_AVATAR")]
	TownBuildingCondPassAvatar,
	[ProtoEnum(Name = "TOWN_BUILDING_COND_SIDE_ROAD_WITH_TERRAIN")]
	TownBuildingCondSideRoadWithTerrain
}
