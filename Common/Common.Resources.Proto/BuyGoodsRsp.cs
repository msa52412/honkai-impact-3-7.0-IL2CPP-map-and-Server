using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyGoodsRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6715
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "GOODS_NOT_EXIST")]
		GoodsNotExist,
		[ProtoEnum(Name = "BUY_TIMES_LACK")]
		BuyTimesLack,
		[ProtoEnum(Name = "MONEY_LACK")]
		MoneyLack,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "SHOP_CLOSE")]
		ShopClose,
		[ProtoEnum(Name = "SHOP_OVERDUE")]
		ShopOverdue,
		[ProtoEnum(Name = "GIFT_GOODS_EFFECT")]
		GiftGoodsEffect,
		[ProtoEnum(Name = "GOODS_LOCKED")]
		GoodsLocked,
		[ProtoEnum(Name = "COUPON_ERROR")]
		CouponError,
		[ProtoEnum(Name = "COUPON_LACK")]
		CouponLack,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "MCOIN_LIMIT")]
		McoinLimit,
		[ProtoEnum(Name = "GLOBAL_GOODS_NUM_LIMIT")]
		GlobalGoodsNumLimit,
		[ProtoEnum(Name = "NOT_IN_DISCOUNT")]
		NotInDiscount,
		[ProtoEnum(Name = "IN_DISCOUNT")]
		InDiscount,
		[ProtoEnum(Name = "DISCOUNT_ERROR")]
		DiscountError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ShopId;

	private uint? __pbn__GoodsId;

	private uint? __pbn__GoodsBuyTimes;

	private uint? __pbn__ItemId;

	private uint? __pbn__Level;

	private uint? __pbn__Num;

	private uint? __pbn__GiftPackSelectRewardId;

	private uint? __pbn__SplitFragmentNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "shop_id")]
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

	[ProtoMember(3, Name = "goods_id")]
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

	[ProtoMember(4, Name = "goods_buy_times")]
	public uint GoodsBuyTimes
	{
		get
		{
			return __pbn__GoodsBuyTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__GoodsBuyTimes = value;
		}
	}

	[ProtoMember(5, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(6, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(7, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	[ProtoMember(8, Name = "gift_pack_select_reward_id")]
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

	[ProtoMember(9, Name = "split_fragment_num")]
	public uint SplitFragmentNum
	{
		get
		{
			return __pbn__SplitFragmentNum.GetValueOrDefault();
		}
		set
		{
			__pbn__SplitFragmentNum = value;
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

	public bool ShouldSerializeGoodsBuyTimes()
	{
		return __pbn__GoodsBuyTimes.HasValue;
	}

	public void ResetGoodsBuyTimes()
	{
		__pbn__GoodsBuyTimes = null;
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}

	public bool ShouldSerializeGiftPackSelectRewardId()
	{
		return __pbn__GiftPackSelectRewardId.HasValue;
	}

	public void ResetGiftPackSelectRewardId()
	{
		__pbn__GiftPackSelectRewardId = null;
	}

	public bool ShouldSerializeSplitFragmentNum()
	{
		return __pbn__SplitFragmentNum.HasValue;
	}

	public void ResetSplitFragmentNum()
	{
		__pbn__SplitFragmentNum = null;
	}
}
