using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PhonePendantTimeType
{
	[ProtoEnum(Name = "PHONE_PENDANT_TIME_FOREVER")]
	PhonePendantTimeForever = 1,
	[ProtoEnum(Name = "PHONE_PENDANT_TIME_LIMIT")]
	PhonePendantTimeLimit
}
