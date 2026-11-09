using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarRefreshTicketType
{
	[ProtoEnum(Name = "GOD_WAR_REFRESH_TICKET_WEEK_DAY")]
	GodWarRefreshTicketWeekDay = 1
}
