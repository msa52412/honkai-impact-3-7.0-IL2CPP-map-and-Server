using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FurnitureDirection
{
	[ProtoEnum(Name = "FURNITURE_DIRECTION_DOWN")]
	FurnitureDirectionDown,
	[ProtoEnum(Name = "FURNITURE_DIRECTION_LEFT")]
	FurnitureDirectionLeft,
	[ProtoEnum(Name = "FURNITURE_DIRECTION_UP")]
	FurnitureDirectionUp,
	[ProtoEnum(Name = "FURNITURE_DIRECTION_RIGHT")]
	FurnitureDirectionRight
}
