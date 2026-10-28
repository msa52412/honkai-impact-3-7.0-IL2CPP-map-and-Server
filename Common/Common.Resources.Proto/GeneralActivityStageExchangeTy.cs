using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralActivityStageExchangeType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_EXCHANGE_TYPE_NONE")]
	GeneralActivityExchangeTypeNone,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_EXCHANGE_TYPE_TIMES")]
	GeneralActivityExchangeTypeTimes,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_EXCHANGE_TYPE_RESET")]
	GeneralActivityExchangeTypeReset
}
