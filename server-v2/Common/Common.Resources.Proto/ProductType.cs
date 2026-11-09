using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ProductType
{
	[ProtoEnum(Name = "PRODUCT_NORMAL")]
	ProductNormal = 1,
	[ProtoEnum(Name = "PRODUCT_LIMIT")]
	ProductLimit,
	[ProtoEnum(Name = "PRODUCT_CARD")]
	ProductCard,
	[ProtoEnum(Name = "PRODUCT_SUBSCRIPTION")]
	ProductSubscription,
	[ProtoEnum(Name = "PRODUCT_RANDOM_BOX")]
	ProductRandomBox,
	[ProtoEnum(Name = "PRODUCT_FIXED_BOX")]
	ProductFixedBox,
	[ProtoEnum(Name = "PRODUCT_MCOIN")]
	ProductMcoin,
	[ProtoEnum(Name = "PRODUCT_FOUNDATION")]
	ProductFoundation,
	[ProtoEnum(Name = "PRODUCT_SUBSCRIPTION_NON_RENEWING")]
	ProductSubscriptionNonRenewing,
	[ProtoEnum(Name = "PRODUCT_MAIL_REWARD")]
	ProductMailReward,
	[ProtoEnum(Name = "PRODUCT_PROMO_CODE")]
	ProductPromoCode
}
