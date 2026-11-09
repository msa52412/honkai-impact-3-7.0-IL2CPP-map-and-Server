using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpTeamStatus
{
	[ProtoEnum(Name = "TEAM_STATUS_NONE")]
	TeamStatusNone,
	[ProtoEnum(Name = "TEAM_STATUS_IN_TEAM")]
	TeamStatusInTeam,
	[ProtoEnum(Name = "TEAM_STATUS_IN_LOBBY")]
	TeamStatusInLobby
}
