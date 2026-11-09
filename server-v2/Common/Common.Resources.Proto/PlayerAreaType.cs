using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlayerAreaType
{
	[ProtoEnum(Name = "AREA_NO_LIMIT")]
	AreaNoLimit,
	[ProtoEnum(Name = "AREA_NORTH_CHINA")]
	AreaNorthChina,
	[ProtoEnum(Name = "AREA_NORTH_EAST")]
	AreaNorthEast,
	[ProtoEnum(Name = "AREA_EAST_CHINA")]
	AreaEastChina,
	[ProtoEnum(Name = "AREA_CENTRAL_CHINA")]
	AreaCentralChina,
	[ProtoEnum(Name = "AREA_CENTRAL_SOUTH")]
	AreaCentralSouth,
	[ProtoEnum(Name = "AREA_NORTH_WEST")]
	AreaNorthWest,
	[ProtoEnum(Name = "AREA_SOUTH_WEST")]
	AreaSouthWest,
	[ProtoEnum(Name = "AREA_HK_MC_TW")]
	AreaHkMcTw
}
