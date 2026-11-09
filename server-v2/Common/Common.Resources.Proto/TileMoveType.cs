using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TileMoveType
{
	[ProtoEnum(Name = "TILE_MOVE_WALK")]
	TileMoveWalk,
	[ProtoEnum(Name = "TILE_MOVE_TELEPORT")]
	TileMoveTeleport,
	[ProtoEnum(Name = "TILE_MOVE_KNOCK_OFF")]
	TileMoveKnockOff
}
