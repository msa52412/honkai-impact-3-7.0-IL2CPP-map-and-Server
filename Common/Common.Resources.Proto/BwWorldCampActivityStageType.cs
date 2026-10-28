using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BwWorldCampActivityStageType
{
	[ProtoEnum(Name = "BW_WORLD_CAMP_ACTIVITY_STAGE_WHITE")]
	BwWorldCampActivityStageWhite = 1,
	[ProtoEnum(Name = "BW_WORLD_CAMP_ACTIVITY_STAGE_BLACK")]
	BwWorldCampActivityStageBlack,
	[ProtoEnum(Name = "BW_WORLD_CAMP_ACTIVITY_STAGE_ELITE")]
	BwWorldCampActivityStageElite,
	[ProtoEnum(Name = "BW_WORLD_CAMP_ACTIVITY_STAGE_BOSS")]
	BwWorldCampActivityStageBoss
}
