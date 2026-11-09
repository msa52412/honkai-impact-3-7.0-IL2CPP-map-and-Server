using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleOverallCmpType
{
	[ProtoEnum(Name = "RPG_TALE_CMP_INVALID")]
	RpgTaleCmpInvalid,
	[ProtoEnum(Name = "RPG_TALE_CMP_LESS")]
	RpgTaleCmpLess,
	[ProtoEnum(Name = "RPG_TALE_CMP_LESS_OR_EQUAL")]
	RpgTaleCmpLessOrEqual,
	[ProtoEnum(Name = "RPG_TALE_CMP_EQUAL")]
	RpgTaleCmpEqual,
	[ProtoEnum(Name = "RPG_TALE_CMP_GREATER")]
	RpgTaleCmpGreater,
	[ProtoEnum(Name = "RPG_TALE_CMP_GREATER_OR_EQUAL")]
	RpgTaleCmpGreaterOrEqual
}
