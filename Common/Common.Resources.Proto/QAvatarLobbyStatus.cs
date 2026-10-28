using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarLobbyStatus
{
	[ProtoEnum(Name = "QAVATAR_LOBBY_STATUS_INVALID")]
	QavatarLobbyStatusInvalid,
	[ProtoEnum(Name = "QAVATAR_LOBBY_STATUS_INIT")]
	QavatarLobbyStatusInit,
	[ProtoEnum(Name = "QAVATAR_LOBBY_STATUS_PREPARE")]
	QavatarLobbyStatusPrepare,
	[ProtoEnum(Name = "QAVATAR_LOBBY_STATUS_FIGHT")]
	QavatarLobbyStatusFight,
	[ProtoEnum(Name = "QAVATAR_LOBBY_STATUS_SETTLE")]
	QavatarLobbyStatusSettle,
	[ProtoEnum(Name = "QAVATAR_LOBBY_STATUS_DELETE")]
	QavatarLobbyStatusDelete
}
