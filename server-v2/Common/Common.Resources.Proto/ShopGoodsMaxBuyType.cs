using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ShopGoodsMaxBuyType
{
	[ProtoEnum(Name = "SHOP_GOODS_MAX_BUY_TYPE_SCHEDULE_MAX_LIMIT")]
	ShopGoodsMaxBuyTypeScheduleMaxLimit,
	[ProtoEnum(Name = "SHOP_GOODS_MAX_BUY_TYPE_BACKPACK_MAX_LIMIT")]
	ShopGoodsMaxBuyTypeBackpackMaxLimit,
	[ProtoEnum(Name = "SHOP_GOODS_MAX_BUY_TYPE_NO_LIMIT")]
	ShopGoodsMaxBuyTypeNoLimit
}
