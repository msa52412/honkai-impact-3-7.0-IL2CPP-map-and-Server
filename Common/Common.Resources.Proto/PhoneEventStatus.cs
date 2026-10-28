using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PhoneEventStatus
{
	[ProtoEnum(Name = "PHONE_EVENT_NONE")]
	PhoneEventNone,
	[ProtoEnum(Name = "PHONE_EVENT_FINISH")]
	PhoneEventFinish,
	[ProtoEnum(Name = "PHONE_EVENT_REFUSE")]
	PhoneEventRefuse,
	[ProtoEnum(Name = "PHONE_EVENT_HOLD")]
	PhoneEventHold,
	[ProtoEnum(Name = "PHONE_EVENT_OVER")]
	PhoneEventOver
}
