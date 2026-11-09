using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RoomClientStatus
{
	[ProtoEnum(Name = "ROOM_CLIENT_NONE")]
	RoomClientNone,
	[ProtoEnum(Name = "ROOM_CLIENT_SELECTING_MAP")]
	RoomClientSelectingMap,
	[ProtoEnum(Name = "ROOM_CLIENT_EQUIPING")]
	RoomClientEquiping
}
