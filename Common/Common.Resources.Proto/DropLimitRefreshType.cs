using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DropLimitRefreshType
{
	[ProtoEnum(Name = "DROP_LIMIT_REFRESH_WEEK_DAY")]
	DropLimitRefreshWeekDay = 1,
	[ProtoEnum(Name = "DROP_LIMIT_REFRESH_MONTH")]
	DropLimitRefreshMonth
}
