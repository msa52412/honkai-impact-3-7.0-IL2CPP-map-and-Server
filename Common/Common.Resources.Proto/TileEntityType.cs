using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TileEntityType
{
	[ProtoEnum(Name = "TILE_ENTITY_TYPE_NONE")]
	TileEntityTypeNone,
	[ProtoEnum(Name = "TILE_ENTITY_TYPE_PLAYER")]
	TileEntityTypePlayer,
	[ProtoEnum(Name = "TILE_ENTITY_TYPE_NPC")]
	TileEntityTypeNpc,
	[ProtoEnum(Name = "TILE_ENTITY_TYPE_ROADBLOCK")]
	TileEntityTypeRoadblock,
	[ProtoEnum(Name = "TILE_ENTITY_TYPE_TOWN_AVATAR")]
	TileEntityTypeTownAvatar,
	[ProtoEnum(Name = "TILE_ENTITY_TYPE_TOWN_BUILDING")]
	TileEntityTypeTownBuilding
}
