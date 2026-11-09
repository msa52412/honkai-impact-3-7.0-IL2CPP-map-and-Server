using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LobbyMemberStatus
{
	[ProtoEnum(Name = "LOBBY_MEMBER_UNREADY")]
	LobbyMemberUnready = 1,
	[ProtoEnum(Name = "LOBBY_MEMBER_READY")]
	LobbyMemberReady,
	[ProtoEnum(Name = "LOBBY_MEMBER_FIGHTING")]
	LobbyMemberFighting
}
