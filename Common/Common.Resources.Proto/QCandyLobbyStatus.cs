using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyLobbyStatus
{
	[ProtoEnum(Name = "QCANDY_LOBBY_STATUS_INVALID")]
	QcandyLobbyStatusInvalid,
	[ProtoEnum(Name = "QCANDY_LOBBY_STATUS_INIT")]
	QcandyLobbyStatusInit,
	[ProtoEnum(Name = "QCANDY_LOBBY_STATUS_FIGHT")]
	QcandyLobbyStatusFight,
	[ProtoEnum(Name = "QCANDY_LOBBY_STATUS_SETTLE")]
	QcandyLobbyStatusSettle,
	[ProtoEnum(Name = "QCANDY_LOBBY_STATUS_DELETE")]
	QcandyLobbyStatusDelete
}
