using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Product : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	private string __pbn__Desc;

	private ProductType? __pbn__Type;

	private uint? __pbn__PayHcoin;

	private uint? __pbn__FreeHcoin;

	private uint? __pbn__Price;

	private uint? __pbn__LeftBuyTimes;

	private uint? __pbn__CardDailyHcoin;

	private uint? __pbn__CardLeftDays;

	private bool? __pbn__IsForbidByItunesSmallOrder;

	private bool? __pbn__IsItunesBoxSubscription;

	private uint? __pbn__CardActivityDailyHcoin;

	private bool? __pbn__IsAddHcoin;

	private uint? __pbn__EndTime;

	private uint? __pbn__SortPriority;

	private bool? __pbn__IsShowPic;

	private string __pbn__PicPath;

	private string __pbn__Detail;

	private uint? __pbn__Id;

	private uint? __pbn__Mcoin;

	private uint? __pbn__ProductGoodsId;

	private string __pbn__PriceTier;

	[ProtoMember(1, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(2, Name = "desc")]
	[DefaultValue("")]
	public string Desc
	{
		get
		{
			return __pbn__Desc ?? "";
		}
		set
		{
			__pbn__Desc = value;
		}
	}

	[ProtoMember(3, Name = "type")]
	[DefaultValue(ProductType.ProductNormal)]
	public ProductType Type
	{
		get
		{
			return __pbn__Type ?? ProductType.ProductNormal;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(4, Name = "pay_hcoin")]
	public uint PayHcoin
	{
		get
		{
			return __pbn__PayHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__PayHcoin = value;
		}
	}

	[ProtoMember(5, Name = "free_hcoin")]
	public uint FreeHcoin
	{
		get
		{
			return __pbn__FreeHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeHcoin = value;
		}
	}

	[ProtoMember(6, Name = "price")]
	public uint Price
	{
		get
		{
			return __pbn__Price.GetValueOrDefault();
		}
		set
		{
			__pbn__Price = value;
		}
	}

	[ProtoMember(7, Name = "left_buy_times")]
	public uint LeftBuyTimes
	{
		get
		{
			return __pbn__LeftBuyTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftBuyTimes = value;
		}
	}

	[ProtoMember(8, Name = "card_daily_hcoin")]
	public uint CardDailyHcoin
	{
		get
		{
			return __pbn__CardDailyHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__CardDailyHcoin = value;
		}
	}

	[ProtoMember(9, Name = "card_left_days")]
	public uint CardLeftDays
	{
		get
		{
			return __pbn__CardLeftDays.GetValueOrDefault();
		}
		set
		{
			__pbn__CardLeftDays = value;
		}
	}

	[ProtoMember(10, Name = "is_forbid_by_itunes_small_order")]
	public bool IsForbidByItunesSmallOrder
	{
		get
		{
			return __pbn__IsForbidByItunesSmallOrder.GetValueOrDefault();
		}
		set
		{
			__pbn__IsForbidByItunesSmallOrder = value;
		}
	}

	[ProtoMember(11, Name = "box_reward_list")]
	public List<RewardData> BoxRewardLists { get; } = new List<RewardData>();


	[ProtoMember(12, Name = "is_itunes_box_subscription")]
	public bool IsItunesBoxSubscription
	{
		get
		{
			return __pbn__IsItunesBoxSubscription.GetValueOrDefault();
		}
		set
		{
			__pbn__IsItunesBoxSubscription = value;
		}
	}

	[ProtoMember(13, Name = "card_activity_daily_hcoin")]
	public uint CardActivityDailyHcoin
	{
		get
		{
			return __pbn__CardActivityDailyHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__CardActivityDailyHcoin = value;
		}
	}

	[ProtoMember(14, Name = "is_add_hcoin")]
	public bool IsAddHcoin
	{
		get
		{
			return __pbn__IsAddHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAddHcoin = value;
		}
	}

	[ProtoMember(15, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(16, Name = "sort_priority")]
	public uint SortPriority
	{
		get
		{
			return __pbn__SortPriority.GetValueOrDefault();
		}
		set
		{
			__pbn__SortPriority = value;
		}
	}

	[ProtoMember(17, Name = "is_show_pic")]
	public bool IsShowPic
	{
		get
		{
			return __pbn__IsShowPic.GetValueOrDefault();
		}
		set
		{
			__pbn__IsShowPic = value;
		}
	}

	[ProtoMember(18, Name = "pic_path")]
	[DefaultValue("")]
	public string PicPath
	{
		get
		{
			return __pbn__PicPath ?? "";
		}
		set
		{
			__pbn__PicPath = value;
		}
	}

	[ProtoMember(19, Name = "detail")]
	[DefaultValue("")]
	public string Detail
	{
		get
		{
			return __pbn__Detail ?? "";
		}
		set
		{
			__pbn__Detail = value;
		}
	}

	[ProtoMember(20, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(21, Name = "mcoin")]
	public uint Mcoin
	{
		get
		{
			return __pbn__Mcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Mcoin = value;
		}
	}

	[ProtoMember(22, Name = "product_goods_id")]
	public uint ProductGoodsId
	{
		get
		{
			return __pbn__ProductGoodsId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductGoodsId = value;
		}
	}

	[ProtoMember(23, Name = "price_tier")]
	[DefaultValue("")]
	public string PriceTier
	{
		get
		{
			return __pbn__PriceTier ?? "";
		}
		set
		{
			__pbn__PriceTier = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeDesc()
	{
		return __pbn__Desc != null;
	}

	public void ResetDesc()
	{
		__pbn__Desc = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializePayHcoin()
	{
		return __pbn__PayHcoin.HasValue;
	}

	public void ResetPayHcoin()
	{
		__pbn__PayHcoin = null;
	}

	public bool ShouldSerializeFreeHcoin()
	{
		return __pbn__FreeHcoin.HasValue;
	}

	public void ResetFreeHcoin()
	{
		__pbn__FreeHcoin = null;
	}

	public bool ShouldSerializePrice()
	{
		return __pbn__Price.HasValue;
	}

	public void ResetPrice()
	{
		__pbn__Price = null;
	}

	public bool ShouldSerializeLeftBuyTimes()
	{
		return __pbn__LeftBuyTimes.HasValue;
	}

	public void ResetLeftBuyTimes()
	{
		__pbn__LeftBuyTimes = null;
	}

	public bool ShouldSerializeCardDailyHcoin()
	{
		return __pbn__CardDailyHcoin.HasValue;
	}

	public void ResetCardDailyHcoin()
	{
		__pbn__CardDailyHcoin = null;
	}

	public bool ShouldSerializeCardLeftDays()
	{
		return __pbn__CardLeftDays.HasValue;
	}

	public void ResetCardLeftDays()
	{
		__pbn__CardLeftDays = null;
	}

	public bool ShouldSerializeIsForbidByItunesSmallOrder()
	{
		return __pbn__IsForbidByItunesSmallOrder.HasValue;
	}

	public void ResetIsForbidByItunesSmallOrder()
	{
		__pbn__IsForbidByItunesSmallOrder = null;
	}

	public bool ShouldSerializeIsItunesBoxSubscription()
	{
		return __pbn__IsItunesBoxSubscription.HasValue;
	}

	public void ResetIsItunesBoxSubscription()
	{
		__pbn__IsItunesBoxSubscription = null;
	}

	public bool ShouldSerializeCardActivityDailyHcoin()
	{
		return __pbn__CardActivityDailyHcoin.HasValue;
	}

	public void ResetCardActivityDailyHcoin()
	{
		__pbn__CardActivityDailyHcoin = null;
	}

	public bool ShouldSerializeIsAddHcoin()
	{
		return __pbn__IsAddHcoin.HasValue;
	}

	public void ResetIsAddHcoin()
	{
		__pbn__IsAddHcoin = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeSortPriority()
	{
		return __pbn__SortPriority.HasValue;
	}

	public void ResetSortPriority()
	{
		__pbn__SortPriority = null;
	}

	public bool ShouldSerializeIsShowPic()
	{
		return __pbn__IsShowPic.HasValue;
	}

	public void ResetIsShowPic()
	{
		__pbn__IsShowPic = null;
	}

	public bool ShouldSerializePicPath()
	{
		return __pbn__PicPath != null;
	}

	public void ResetPicPath()
	{
		__pbn__PicPath = null;
	}

	public bool ShouldSerializeDetail()
	{
		return __pbn__Detail != null;
	}

	public void ResetDetail()
	{
		__pbn__Detail = null;
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeMcoin()
	{
		return __pbn__Mcoin.HasValue;
	}

	public void ResetMcoin()
	{
		__pbn__Mcoin = null;
	}

	public bool ShouldSerializeProductGoodsId()
	{
		return __pbn__ProductGoodsId.HasValue;
	}

	public void ResetProductGoodsId()
	{
		__pbn__ProductGoodsId = null;
	}

	public bool ShouldSerializePriceTier()
	{
		return __pbn__PriceTier != null;
	}

	public void ResetPriceTier()
	{
		__pbn__PriceTier = null;
	}
}
