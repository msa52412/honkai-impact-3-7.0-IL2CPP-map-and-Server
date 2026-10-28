using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownBuildingSkillTargetType
{
	[ProtoEnum(Name = "TOWN_TARGET_NONE")]
	TownTargetNone,
	[ProtoEnum(Name = "TOWN_TARGET_AVATAR_CHECK_IN")]
	TownTargetAvatarCheckIn,
	[ProtoEnum(Name = "TOWN_TARGET_ALL_AVATAR")]
	TownTargetAllAvatar,
	[ProtoEnum(Name = "TOWN_TARGET_ATTRIBUTE_AVATAR")]
	TownTargetAttributeAvatar,
	[ProtoEnum(Name = "TOWN_TARGET_PASSED_AVATAR_IN_CROSS")]
	TownTargetPassedAvatarInCross,
	[ProtoEnum(Name = "TOWN_TARGET_ROAD_IN_CROSS")]
	TownTargetRoadInCross,
	[ProtoEnum(Name = "TOWN_TARGET_ROAD_IN_ROUND")]
	TownTargetRoadInRound,
	[ProtoEnum(Name = "TOWN_TARGET_BUILDING")]
	TownTargetBuilding,
	[ProtoEnum(Name = "TOWN_TARGET_PASSED_AVATAR_IN_ROUND")]
	TownTargetPassedAvatarInRound,
	[ProtoEnum(Name = "TOWN_TARGET_SPECIFIC_AVATAR")]
	TownTargetSpecificAvatar,
	[ProtoEnum(Name = "TOWN_TARGET_RANDOM_EMPTY_ROAD_IN_ROUND")]
	TownTargetRandomEmptyRoadInRound,
	[ProtoEnum(Name = "TOWN_TARGET_RANDOM_MONSTER_AOE")]
	TownTargetRandomMonsterAoe,
	[ProtoEnum(Name = "TOWN_TARGET_BOSS")]
	TownTargetBoss
}
