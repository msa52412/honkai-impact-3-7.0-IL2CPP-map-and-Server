using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownActivityCondType
{
	[ProtoEnum(Name = "TOWN_ACTIVITY_COND_NONE")]
	TownActivityCondNone,
	[ProtoEnum(Name = "TOWN_ACTIVITY_COND_TOTAL_STRENGTH")]
	TownActivityCondTotalStrength,
	[ProtoEnum(Name = "TOWN_ACTIVITY_COND_TOTAL_SPEED")]
	TownActivityCondTotalSpeed,
	[ProtoEnum(Name = "TOWN_ACTIVITY_COND_TOTAL_LUCK")]
	TownActivityCondTotalLuck,
	[ProtoEnum(Name = "TOWN_ACTIVITY_COND_COIN_MATERIAL_NUM")]
	TownActivityCondCoinMaterialNum,
	[ProtoEnum(Name = "TOWN_ACTIVITY_COND_BUILDING_NUM")]
	TownActivityCondBuildingNum
}
