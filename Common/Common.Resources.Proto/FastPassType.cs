using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FastPassType
{
	[ProtoEnum(Name = "FAST_PASS_TYPE_NONE")]
	FastPassTypeNone,
	[ProtoEnum(Name = "FAST_PASS_TYPE_OW")]
	FastPassTypeOw,
	[ProtoEnum(Name = "FAST_PASS_TYPE_WEEKDAY")]
	FastPassTypeWeekday
}
