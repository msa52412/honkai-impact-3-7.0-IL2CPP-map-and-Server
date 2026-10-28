using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FurnitureLocation
{
	[ProtoEnum(Name = "FURNITURE_LOCATION_FLOOR")]
	FurnitureLocationFloor,
	[ProtoEnum(Name = "FURNITURE_LOCATION_WALL1")]
	FurnitureLocationWall1,
	[ProtoEnum(Name = "FURNITURE_LOCATION_WALL2")]
	FurnitureLocationWall2,
	[ProtoEnum(Name = "FURNITURE_LOCATION_WALL3")]
	FurnitureLocationWall3,
	[ProtoEnum(Name = "FURNITURE_LOCATION_WALL4")]
	FurnitureLocationWall4
}
