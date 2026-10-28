using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MedalTimeType
{
	[ProtoEnum(Name = "MEDAL_TIME_FOREVER")]
	MedalTimeForever = 1,
	[ProtoEnum(Name = "MEDAL_TIME_DURATION")]
	MedalTimeDuration,
	[ProtoEnum(Name = "MEDAL_TIME_UNTIL")]
	MedalTimeUntil
}
