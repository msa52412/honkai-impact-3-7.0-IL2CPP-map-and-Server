using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleTicketRefreshType
{
	[ProtoEnum(Name = "RPG_TALE_TICKET_REFRESH_DAILY")]
	RpgTaleTicketRefreshDaily = 1,
	[ProtoEnum(Name = "RPG_TALE_TICKET_REFRESH_WEEKLY")]
	RpgTaleTicketRefreshWeekly,
	[ProtoEnum(Name = "RPG_TALE_TICKET_REFRESH_CUSTOM")]
	RpgTaleTicketRefreshCustom,
	[ProtoEnum(Name = "RPG_TALE_TICKET_REFRESH_DAILY_WITH_STORAGE")]
	RpgTaleTicketRefreshDailyWithStorage
}
