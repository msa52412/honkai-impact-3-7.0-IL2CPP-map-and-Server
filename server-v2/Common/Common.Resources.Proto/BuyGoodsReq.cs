using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyGoodsReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6714
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ShopId;

	private uint? __pbn__GoodsId;

	private uint? __pbn__CouponMaterialId;

	private uint? __pbn__CouponNum;

	private uint? __pbn__McoinCouponMaterialId;

	private uint? __pbn__GoodsNum;

	private uint? __pbn__GiftPackSelectRewardId;

	private uint? __pbn__DiscountSaveNum;

	[ProtoMember(1, Name = "shop_id")]
	public uint ShopId
	{
		get
		{
			return __pbn__ShopId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShopId = value;
		}
	}

	[ProtoMember(2, Name = "goods_id")]
	public uint GoodsId
	{
		get
		{
			return __pbn__GoodsId.GetValueOrDefault();
		}
		set
		{
			__pbn__GoodsId = value;
		}
	}

	[ProtoMember(3, Name = "coupon_material_id")]
	public uint CouponMaterialId
	{
		get
		{
			return __pbn__CouponMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__CouponMaterialId = value;
		}
	}

	[ProtoMember(4, Name = "coupon_num")]
	public uint CouponNum
	{
		get
		{
			return __pbn__CouponNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CouponNum = value;
		}
	}

	[ProtoMember(5, Name = "mcoin_coupon_material_id")]
	public uint McoinCouponMaterialId
	{
		get
		{
			return __pbn__McoinCouponMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__McoinCouponMaterialId = value;
		}
	}

	[ProtoMember(6, Name = "goods_num")]
	public uint GoodsNum
	{
		get
		{
			return __pbn__GoodsNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GoodsNum = value;
		}
	}

	[ProtoMember(7, Name = "gift_pack_select_reward_id")]
	public uint GiftPackSelectRewardId
	{
		get
		{
			return __pbn__GiftPackSelectRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__GiftPackSelectRewardId = value;
		}
	}

	[ProtoMember(8, Name = "discount_save_num")]
	public uint DiscountSaveNum
	{
		get
		{
			return __pbn__DiscountSaveNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DiscountSaveNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShopId()
	{
		return __pbn__ShopId.HasValue;
	}

	public void ResetShopId()
	{
		__pbn__ShopId = null;
	}

	public bool ShouldSerializeGoodsId()
	{
		return __pbn__GoodsId.HasValue;
	}

	public void ResetGoodsId()
	{
		__pbn__GoodsId = null;
	}

	public bool ShouldSerializeCouponMaterialId()
	{
		return __pbn__CouponMaterialId.HasValue;
	}

	public void ResetCouponMaterialId()
	{
		__pbn__CouponMaterialId = null;
	}

	public bool ShouldSerializeCouponNum()
	{
		return __pbn__CouponNum.HasValue;
	}

	public void ResetCouponNum()
	{
		__pbn__CouponNum = null;
	}

	public bool ShouldSerializeMcoinCouponMaterialId()
	{
		return __pbn__McoinCouponMaterialId.HasValue;
	}

	public void ResetMcoinCouponMaterialId()
	{
		__pbn__McoinCouponMaterialId = null;
	}

	public bool ShouldSerializeGoodsNum()
	{
		return __pbn__GoodsNum.HasValue;
	}

	public void ResetGoodsNum()
	{
		__pbn__GoodsNum = null;
	}

	public bool ShouldSerializeGiftPackSelectRewardId()
	{
		return __pbn__GiftPackSelectRewardId.HasValue;
	}

	public void ResetGiftPackSelectRewardId()
	{
		__pbn__GiftPackSelectRewardId = null;
	}

	public bool ShouldSerializeDiscountSaveNum()
	{
		return __pbn__DiscountSaveNum.HasValue;
	}

	public void ResetDiscountSaveNum()
	{
		__pbn__DiscountSaveNum = null;
	}
}
