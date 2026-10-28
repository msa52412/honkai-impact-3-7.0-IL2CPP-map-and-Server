using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FastPassLimitType
{
	[ProtoEnum(Name = "FAST_PASS_LIMIT_NONE")]
	FastPassLimitNone,
	[ProtoEnum(Name = "FAST_PASS_LIMIT_DAILY")]
	FastPassLimitDaily,
	[ProtoEnum(Name = "FAST_PASS_LIMIT_WEEKLY")]
	FastPassLimitWeekly
}
