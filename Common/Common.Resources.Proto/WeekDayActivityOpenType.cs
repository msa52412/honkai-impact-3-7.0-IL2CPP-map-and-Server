using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WeekDayActivityOpenType
{
	[ProtoEnum(Name = "WEEK_DAY_ACTIVITY_OPEN_DAILY")]
	WeekDayActivityOpenDaily,
	[ProtoEnum(Name = "WEEK_DAY_ACTIVITY_OPEN_WEEK_DAY")]
	WeekDayActivityOpenWeekDay
}
