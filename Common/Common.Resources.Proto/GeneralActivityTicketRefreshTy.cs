using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralActivityTicketRefreshType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_TICKET_REFRESH_NONE")]
	GeneralActivityTicketRefreshNone,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_TICKET_REFRESH_DAILY")]
	GeneralActivityTicketRefreshDaily,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_TICKET_REFRESH_WEEKLY")]
	GeneralActivityTicketRefreshWeekly
}
