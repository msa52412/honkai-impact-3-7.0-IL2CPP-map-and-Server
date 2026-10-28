using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BwWorldCampActivityCampType
{
	[ProtoEnum(Name = "BW_WORLD_CAMP_ACTIVITY_CAMP_NONE")]
	BwWorldCampActivityCampNone,
	[ProtoEnum(Name = "BW_WORLD_CAMP_ACTIVITY_CAMP_WHITE")]
	BwWorldCampActivityCampWhite,
	[ProtoEnum(Name = "BW_WORLD_CAMP_ACTIVITY_CAMP_BLACK")]
	BwWorldCampActivityCampBlack
}
