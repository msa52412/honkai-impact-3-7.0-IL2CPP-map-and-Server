using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LobbyEnterType
{
	[ProtoEnum(Name = "LOBBY_ENTER_TYPE_NONE")]
	LobbyEnterTypeNone,
	[ProtoEnum(Name = "LOBBY_ENTER_TYPE_PRIVATE")]
	LobbyEnterTypePrivate,
	[ProtoEnum(Name = "LOBBY_ENTER_TYPE_ENTER")]
	LobbyEnterTypeEnter,
	[ProtoEnum(Name = "LOBBY_ENTER_TYPE_FAST")]
	LobbyEnterTypeFast
}
