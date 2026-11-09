using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RoomMemberStatus
{
	[ProtoEnum(Name = "ROOM_UNREADY")]
	RoomUnready = 1,
	[ProtoEnum(Name = "ROOM_READY")]
	RoomReady,
	[ProtoEnum(Name = "ROOM_IN_BATTLE")]
	RoomInBattle
}
