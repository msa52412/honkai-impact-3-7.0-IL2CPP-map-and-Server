using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LobbyClientStatus
{
	[ProtoEnum(Name = "LOBBY_CLIENT_NONE")]
	LobbyClientNone,
	[ProtoEnum(Name = "LOBBY_CLIENT_CHOOSING_AVATAR")]
	LobbyClientChoosingAvatar,
	[ProtoEnum(Name = "LOBBY_CLIENT_CHOOSING_WEAPON")]
	LobbyClientChoosingWeapon,
	[ProtoEnum(Name = "LOBBY_CLIENT_CHOOSING_STIGMATA")]
	LobbyClientChoosingStigmata
}
