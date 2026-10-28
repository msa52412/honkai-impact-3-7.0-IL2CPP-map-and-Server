using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LobbyStatus
{
	[ProtoEnum(Name = "LOBBY_NONE")]
	LobbyNone,
	[ProtoEnum(Name = "LOBBY_PREPARING")]
	LobbyPreparing,
	[ProtoEnum(Name = "LOBBY_CONNECTING")]
	LobbyConnecting,
	[ProtoEnum(Name = "LOBBY_FIGHTING")]
	LobbyFighting,
	[ProtoEnum(Name = "LOBBY_FINISHING")]
	LobbyFinishing,
	[ProtoEnum(Name = "LOBBY_WAIT_DESTROY")]
	LobbyWaitDestroy
}
