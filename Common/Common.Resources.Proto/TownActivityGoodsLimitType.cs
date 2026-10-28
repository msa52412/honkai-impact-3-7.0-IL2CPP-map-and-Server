using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownActivityGoodsLimitType
{
	[ProtoEnum(Name = "TOWN_ACTIVITY_GOODS_LIMIT_SHOP")]
	TownActivityGoodsLimitShop = 1,
	[ProtoEnum(Name = "TOWN_ACTIVITY_GOODS_LIMIT_GLOBAL")]
	TownActivityGoodsLimitGlobal
}
