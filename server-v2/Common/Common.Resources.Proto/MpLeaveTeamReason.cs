using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpLeaveTeamReason
{
	[ProtoEnum(Name = "LEAVE_TEAM_REASON_NONE")]
	LeaveTeamReasonNone,
	[ProtoEnum(Name = "LEAVE_TEAM_REASON_KICK")]
	LeaveTeamReasonKick
}
