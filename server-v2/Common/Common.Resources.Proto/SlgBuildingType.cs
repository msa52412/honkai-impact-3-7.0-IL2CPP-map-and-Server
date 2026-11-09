using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SlgBuildingType
{
	[ProtoEnum(Name = "SLG_BUILDING_TYPE_OCCUPY_OWNER_BUFF")]
	SlgBuildingTypeOccupyOwnerBuff = 1,
	[ProtoEnum(Name = "SLG_BUILDING_TYPE_OCCUPY_OTHERS_BUFF")]
	SlgBuildingTypeOccupyOthersBuff,
	[ProtoEnum(Name = "SLG_BUILDING_TYPE_SMALL_BOSS")]
	SlgBuildingTypeSmallBoss,
	[ProtoEnum(Name = "SLG_BUILDING_TYPE_BIG_BOSS")]
	SlgBuildingTypeBigBoss
}
