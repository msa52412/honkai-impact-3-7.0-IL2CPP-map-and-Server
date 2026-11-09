using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CustomHeadTimeType
{
	[ProtoEnum(Name = "CUSTOM_HEAD_TIME_FOREVER")]
	CustomHeadTimeForever = 1,
	[ProtoEnum(Name = "CUSTOM_HEAD_TIME_DURATION")]
	CustomHeadTimeDuration,
	[ProtoEnum(Name = "CUSTOM_HEAD_TIME_UNTIL")]
	CustomHeadTimeUntil
}
