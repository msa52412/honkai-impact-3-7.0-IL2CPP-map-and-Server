using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BulletinType
{
	[ProtoEnum(Name = "BULLETIN_TYPE_EVENT")]
	BulletinTypeEvent = 0,
	[ProtoEnum(Name = "BULLETIN_TYPE_SYSTEM")]
	BulletinTypeSystem = 1,
	[ProtoEnum(Name = "BULLETIN_TYPE_BANNER")]
	BulletinTypeBanner = 2,
	[ProtoEnum(Name = "BULLETIN_TYPE_ACTIVITY_PAGE")]
	BulletinTypeActivityPage = 3,
	[ProtoEnum(Name = "BULLETIN_TYPE_LOGIN_HINT")]
	BulletinTypeLoginHint = 5,
	[ProtoEnum(Name = "BULLETIN_TYPE_VERSION")]
	BulletinTypeVersion = 6,
	[ProtoEnum(Name = "BULLETIN_TYPE_MALL_BANNER")]
	BulletinTypeMallBanner = 7,
	[ProtoEnum(Name = "BULLETIN_TYPE_BP_MALL_BANNER")]
	BulletinTypeBpMallBanner = 8,
	[ProtoEnum(Name = "BULLETIN_TYPE_MAIN_PAGE_PHONE")]
	BulletinTypeMainPagePhone = 9,
	[ProtoEnum(Name = "BULLETIN_TYPE_RETURNEE_MALL_BANNER")]
	BulletinTypeReturneeMallBanner = 10,
	[ProtoEnum(Name = "BULLETIN_TYPE_ACTIVITY_GIFT")]
	BulletinTypeActivityGift = 11,
	[ProtoEnum(Name = "BULLETIN_TYPE_MALL_RECHARGE_SUB_TAB_BANNER")]
	BulletinTypeMallRechargeSubTabBanner = 12,
	[ProtoEnum(Name = "BULLETIN_TYPE_MALL_BUNDLE_COIN_SUB_TAB_BANNER")]
	BulletinTypeMallBundleCoinSubTabBanner = 13,
	[ProtoEnum(Name = "BULLETIN_TYPE_RETURNEE_MALL_BANNER_V2")]
	BulletinTypeReturneeMallBannerV2 = 14,
	[ProtoEnum(Name = "BULLETIN_TYPE_RETURNEE_MALL_BANNER_V3")]
	BulletinTypeReturneeMallBannerV3 = 15,
	[ProtoEnum(Name = "BULLETIN_TYPE_PLACEHOLDER_01")]
	BulletinTypePlaceholder01 = 16,
	[ProtoEnum(Name = "BULLETIN_TYPE_PLACEHOLDER_02")]
	BulletinTypePlaceholder02 = 17,
	[ProtoEnum(Name = "BULLETIN_TYPE_PLACEHOLDER_03")]
	BulletinTypePlaceholder03 = 18,
	[ProtoEnum(Name = "BULLETIN_TYPE_PLACEHOLDER_04")]
	BulletinTypePlaceholder04 = 19,
	[ProtoEnum(Name = "BULLETIN_TYPE_PLACEHOLDER_05")]
	BulletinTypePlaceholder05 = 20
}
