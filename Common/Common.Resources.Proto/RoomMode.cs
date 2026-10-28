using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RoomMode
{
	[ProtoEnum(Name = "ROOM_MODE_INVALID")]
	RoomModeInvalid,
	[ProtoEnum(Name = "ROOM_MODE_TVT")]
	RoomModeTvt,
	[ProtoEnum(Name = "ROOM_MODE_MASSIVE_WAR")]
	RoomModeMassiveWar,
	[ProtoEnum(Name = "ROOM_MODE_QAVATAR_BATTLE")]
	RoomModeQavatarBattle,
	[ProtoEnum(Name = "ROOM_MODE_QCANDY_BATTLE")]
	RoomModeQcandyBattle
}
