using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarEffectType
{
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ABILITY")]
	GodWarEffectAbility = 1,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ADD_TELEPORT_REFRESH_TIMES")]
	GodWarEffectAddTeleportRefreshTimes = 3,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_MATERIAL_EVENT_ADD_RATIO")]
	GodWarEffectMaterialEventAddRatio = 4,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ADD_INIT_COIN_NUM")]
	GodWarEffectAddInitCoinNum = 5,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_BUFF_GOODS_DISCOUNT")]
	GodWarEffectBuffGoodsDiscount = 6,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_LEVELUP_BUFF_GOODS_DISCOUNT")]
	GodWarEffectLevelupBuffGoodsDiscount = 7,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ADD_BUFF_POOL_QUALITY")]
	GodWarEffectAddBuffPoolQuality = 10,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ADD_SHOP_BUFF_GOODS_NUM")]
	GodWarEffectAddShopBuffGoodsNum = 11,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ADD_SHOP_LEVELUP_BUFF_GOODS_NUM")]
	GodWarEffectAddShopLevelupBuffGoodsNum = 12,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ADD_REVIVE_TIMES")]
	GodWarEffectAddReviveTimes = 13,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_REVIVE_HP_SP_PERCENT")]
	GodWarEffectReviveHpSpPercent = 14,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ADD_BUFF_SUIT_INIT_LEVEL")]
	GodWarEffectAddBuffSuitInitLevel = 15,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_SHOP_SPECIAL_DISCOUNT")]
	GodWarEffectShopSpecialDiscount = 16,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_MATERIAL_EVENT_SUB_RATIO")]
	GodWarEffectMaterialEventSubRatio = 17,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_ADD_SPECIFIED_NUM_BUFF_INIT_LEVEL")]
	GodWarEffectAddSpecifiedNumBuffInitLevel = 18,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_SPECIFIED_QUALITY_BUFF_GOODS_DISCOUNT")]
	GodWarEffectSpecifiedQualityBuffGoodsDiscount = 19,
	[ProtoEnum(Name = "GOD_WAR_EFFECT_SPECIFIED_QUALITY_LEVEL_UP_BUFF_GOODS_DISCOUNT")]
	GodWarEffectSpecifiedQualityLevelUpBuffGoodsDiscount = 20
}
