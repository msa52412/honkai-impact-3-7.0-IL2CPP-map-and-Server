using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RestaurantSkillType
{
	[ProtoEnum(Name = "RESTAURANT_SKILL_COOK_ID_SPEED_UP")]
	RestaurantSkillCookIdSpeedUp = 1,
	[ProtoEnum(Name = "RESTAURANT_SKILL_COOK_TAG_SPEED_UP")]
	RestaurantSkillCookTagSpeedUp,
	[ProtoEnum(Name = "RESTAURANT_SKILL_COOK_ALL_SPEED_UP")]
	RestaurantSkillCookAllSpeedUp,
	[ProtoEnum(Name = "RESTAURANT_SKILL_SELL_ID_SPEED_UP")]
	RestaurantSkillSellIdSpeedUp,
	[ProtoEnum(Name = "RESTAURANT_SKILL_SELL_TAG_SPEED_UP")]
	RestaurantSkillSellTagSpeedUp,
	[ProtoEnum(Name = "RESTAURANT_SKILL_SELL_ALL_SPEED_UP")]
	RestaurantSkillSellAllSpeedUp,
	[ProtoEnum(Name = "RESTAURANT_SKILL_SELL_ID_PRICE_UP")]
	RestaurantSkillSellIdPriceUp,
	[ProtoEnum(Name = "RESTAURANT_SKILL_SELL_TAG_PRICE_UP")]
	RestaurantSkillSellTagPriceUp,
	[ProtoEnum(Name = "RESTAURANT_SKILL_SELL_ALL_PRICE_UP")]
	RestaurantSkillSellAllPriceUp
}
