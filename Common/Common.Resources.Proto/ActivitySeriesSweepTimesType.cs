using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ActivitySeriesSweepTimesType
{
	[ProtoEnum(Name = "ACTIVITY_SERIES_SWEEP_TIMES_NONE")]
	ActivitySeriesSweepTimesNone,
	[ProtoEnum(Name = "ACTIVITY_SERIES_SWEEP_TIMES_DAILY")]
	ActivitySeriesSweepTimesDaily,
	[ProtoEnum(Name = "ACTIVITY_SERIES_SWEEP_TIMES_WEEKLY")]
	ActivitySeriesSweepTimesWeekly
}
