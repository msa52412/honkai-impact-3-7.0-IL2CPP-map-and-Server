using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WeekdayActivityEnterTimesType
{
	[ProtoEnum(Name = "WEEKDAY_ACTIVITY_ENTER_TIMES_DAILY")]
	WeekdayActivityEnterTimesDaily,
	[ProtoEnum(Name = "WEEKDAY_ACTIVITY_ENTER_TIMES_WEEKLY")]
	WeekdayActivityEnterTimesWeekly
}
