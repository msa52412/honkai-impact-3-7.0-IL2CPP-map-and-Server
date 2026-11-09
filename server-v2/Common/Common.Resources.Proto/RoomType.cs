using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RoomType
{
	[ProtoEnum(Name = "ROOM_TYPE_HALL")]
	RoomTypeHall = 1,
	[ProtoEnum(Name = "ROOM_TYPE_BEDROOM")]
	RoomTypeBedroom
}
