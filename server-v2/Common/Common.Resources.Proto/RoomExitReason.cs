using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RoomExitReason
{
	[ProtoEnum(Name = "ROOM_EXIT_NONE")]
	RoomExitNone,
	[ProtoEnum(Name = "ROOM_EXIT_KICK")]
	RoomExitKick,
	[ProtoEnum(Name = "ROOM_EXIT_DISMISSED")]
	RoomExitDismissed
}
