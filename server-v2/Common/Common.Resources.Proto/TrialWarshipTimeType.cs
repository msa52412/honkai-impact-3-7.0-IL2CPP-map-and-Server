using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TrialWarshipTimeType
{
	[ProtoEnum(Name = "TRIAL_WARSHIP_TIME_UNTIL")]
	TrialWarshipTimeUntil = 1,
	[ProtoEnum(Name = "TRIAL_WARSHIP_TIME_DURATION")]
	TrialWarshipTimeDuration
}
