using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TileBarrierType
{
	[ProtoEnum(Name = "TILE_BARRIER_TYPE_NONE")]
	TileBarrierTypeNone,
	[ProtoEnum(Name = "TILE_BARRIER_TYPE_PERMANENT")]
	TileBarrierTypePermanent,
	[ProtoEnum(Name = "TILE_BARRIER_TYPE_BLOCK")]
	TileBarrierTypeBlock,
	[ProtoEnum(Name = "TILE_BARRIER_TYPE_STOP_ON_PLAYER")]
	TileBarrierTypeStopOnPlayer,
	[ProtoEnum(Name = "TILE_BARRIER_TYPE_STOP_ON_ONCE")]
	TileBarrierTypeStopOnOnce,
	[ProtoEnum(Name = "TILE_BARRIER_TYPE_NON_ROADBLOCK")]
	TileBarrierTypeNonRoadblock
}
