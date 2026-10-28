using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DisplayCurrencyType
{
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_NONE")]
	DisplayCurrencyTypeNone = 0,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_SCOIN")]
	DisplayCurrencyTypeScoin = 1,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_STAMINA")]
	DisplayCurrencyTypeStamina = 2,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_HCOIN")]
	DisplayCurrencyTypeHcoin = 3,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_PURPLE_JADE")]
	DisplayCurrencyTypePurpleJade = 5,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_MCOIN")]
	DisplayCurrencyTypeMcoin = 6,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_PLAIN_DRESS_TICKET")]
	DisplayCurrencyTypePlainDressTicket = 7,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_LUXURY_DRESS_TICKET")]
	DisplayCurrencyTypeLuxuryDressTicket = 8,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_VITALITY")]
	DisplayCurrencyTypeVitality = 12,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_ETHER_PHLOGISTON")]
	DisplayCurrencyTypeEtherPhlogiston = 13,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_BRILLIANT_PURE_CRYSTAL")]
	DisplayCurrencyTypeBrilliantPureCrystal = 14,
	[ProtoEnum(Name = "DISPLAY_CURRENCY_TYPE_HONOR_MEDAL")]
	DisplayCurrencyTypeHonorMedal = 15
}
