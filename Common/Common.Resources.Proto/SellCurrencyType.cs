using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SellCurrencyType
{
	[ProtoEnum(Name = "SELL_CURRENCY_TYPE_NONE")]
	SellCurrencyTypeNone = 0,
	[ProtoEnum(Name = "SELL_CURRENCY_TYPE_SCOIN")]
	SellCurrencyTypeScoin = 1,
	[ProtoEnum(Name = "SELL_CURRENCY_TYPE_PURPLE_JADE")]
	SellCurrencyTypePurpleJade = 5
}
