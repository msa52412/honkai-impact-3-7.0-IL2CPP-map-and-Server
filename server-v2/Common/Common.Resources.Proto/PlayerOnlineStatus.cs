using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlayerOnlineStatus
{
	[ProtoEnum(Name = "PLAYER_ONLINE_STATUS_NONE")]
	PlayerOnlineStatusNone,
	[ProtoEnum(Name = "PLAYER_ONLINE_STATUS_ONLINE")]
	PlayerOnlineStatusOnline,
	[ProtoEnum(Name = "PLAYER_ONLINE_STATUS_WAIT_SAVE")]
	PlayerOnlineStatusWaitSave,
	[ProtoEnum(Name = "PLAYER_ONLINE_STATUS_OFFLINE")]
	PlayerOnlineStatusOffline
}
