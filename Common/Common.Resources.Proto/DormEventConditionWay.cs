using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DormEventConditionWay
{
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_PASS_STAGE")]
	DormEventConditionWayPassStage = 1,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_ISLAND_ADVANTURE")]
	DormEventConditionWayIslandAdvanture = 2,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_AVATAR_STAR_UP")]
	DormEventConditionWayAvatarStarUp = 3,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_MP_STAGE")]
	DormEventConditionWayMpStage = 4,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_OPEN_WORLD_QUEST")]
	DormEventConditionWayOpenWorldQuest = 5,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_MVP")]
	DormEventConditionWayMvp = 6,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_NO_FIGHT")]
	DormEventConditionWayNoFight = 7,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_LEVEL_UP")]
	DormEventConditionWayLevelUp = 8,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_PASS_STAGE_WITH_ROLE")]
	DormEventConditionWayPassStageWithRole = 9,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_FINISH_MISSION")]
	DormEventConditionWayFinishMission = 11,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_ENDLESS_STAGE_PASS")]
	DormEventConditionWayEndlessStagePass = 12,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_OFFLINE")]
	DormEventConditionWayOffline = 13,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_BIRTHDAY")]
	DormEventConditionWayBirthday = 14,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_GACHA")]
	DormEventConditionWayGacha = 15,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_NEW_ROLE")]
	DormEventConditionWayNewRole = 16,
	[ProtoEnum(Name = "DORM_EVENT_CONDITION_WAY_ACTIVE_DEGREE")]
	DormEventConditionWayActiveDegree = 17
}
