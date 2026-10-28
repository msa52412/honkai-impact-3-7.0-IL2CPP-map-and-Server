using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TileFloorStatus
{
	[ProtoEnum(Name = "TILE_FLOOR_STATUS_NONE")]
	TileFloorStatusNone,
	[ProtoEnum(Name = "TILE_FLOOR_STATUS_LOCKED")]
	TileFloorStatusLocked,
	[ProtoEnum(Name = "TILE_FLOOR_STATUS_UNLOCKED")]
	TileFloorStatusUnlocked,
	[ProtoEnum(Name = "TILE_FLOOR_STATUS_VISTIED")]
	TileFloorStatusVistied
}
