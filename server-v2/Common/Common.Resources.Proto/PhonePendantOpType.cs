using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PhonePendantOpType
{
	[ProtoEnum(Name = "PHONE_PENDANT_WEAR")]
	PhonePendantWear = 1,
	[ProtoEnum(Name = "PHONE_PENDANT_OFF")]
	PhonePendantOff
}
