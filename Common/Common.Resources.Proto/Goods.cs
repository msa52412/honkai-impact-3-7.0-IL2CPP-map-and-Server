using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Goods : IExtensible
{
	[ProtoContract]
	public enum RefreshTimeType
	{
		[ProtoEnum(Name = "SHOP_TYPE")]
		ShopType,
		[ProtoEnum(Name = "DAY_TYPE")]
		DayType,
		[ProtoEnum(Name = "WEEK_TYPE")]
		WeekType,
		[ProtoEnum(Name = "MONTH_TYPE")]
		MonthType,
		[ProtoEnum(Name = "VERSION_TYPE")]
		VersionType,
		[ProtoEnum(Name = "DOUBLE_WEEK_TYPE")]
		DoubleWeekType
	}

	[ProtoContract]
	public enum ShowType
	{
		[ProtoEnum(Name = "NORMAL_SHOW")]
		NormalShow = 1,
		[ProtoEnum(Name = "LOCK_SHOW")]
		LockShow
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GoodsId;

	private uint? __pbn__BuyTimes;

	private bool? __pbn__CanBeRefresh;

	private RefreshTimeType? __pbn__refresh_time_type;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private ShowType? __pbn__show_type;

	private string __pbn__UnlockTips;

	private uint? __pbn__PrepareTime;

	private uint? __pbn__SortId;

	private string __pbn__ProductName;

	private uint? __pbn__ProductId;

	private uint? __pbn__ProductPrice;

	private string __pbn__ShowSettingStr;

	private uint? __pbn__ShowNewTag;

	private bool? __pbn__IsIgnore;

	private uint? __pbn__UnlockType;

	private uint? __pbn__UnlockParaInt;

	private string __pbn__UnlockParaStr;

	private uint? __pbn__PrepareLevel;

	private uint? __pbn__MinLevel;

	private uint? __pbn__MaxLevel;

	private bool? __pbn__IsPopup;

	private bool? __pbn__IsHidePrice;

	private uint? __pbn__MaxBuyType;

	[ProtoMember(1, Name = "goods_id")]
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

	[ProtoMember(2, Name = "buy_times")]
	public uint BuyTimes
	{
		get
		{
			return __pbn__BuyTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BuyTimes = value;
		}
	}

	[ProtoMember(3, Name = "can_be_refresh")]
	public bool CanBeRefresh
	{
		get
		{
			return __pbn__CanBeRefresh.GetValueOrDefault();
		}
		set
		{
			__pbn__CanBeRefresh = value;
		}
	}

	[ProtoMember(4)]
	[DefaultValue(RefreshTimeType.ShopType)]
	public RefreshTimeType refresh_time_type
	{
		get
		{
			return __pbn__refresh_time_type.GetValueOrDefault();
		}
		set
		{
			__pbn__refresh_time_type = value;
		}
	}

	[ProtoMember(6, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(7, Name = "end_time")]
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

	[ProtoMember(8)]
	[DefaultValue(ShowType.NormalShow)]
	public ShowType show_type
	{
		get
		{
			return __pbn__show_type ?? ShowType.NormalShow;
		}
		set
		{
			__pbn__show_type = value;
		}
	}

	[ProtoMember(9, Name = "unlock_tips")]
	[DefaultValue("")]
	public string UnlockTips
	{
		get
		{
			return __pbn__UnlockTips ?? "";
		}
		set
		{
			__pbn__UnlockTips = value;
		}
	}

	[ProtoMember(10, Name = "prepare_time")]
	public uint PrepareTime
	{
		get
		{
			return __pbn__PrepareTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PrepareTime = value;
		}
	}

	[ProtoMember(11, Name = "sort_id")]
	public uint SortId
	{
		get
		{
			return __pbn__SortId.GetValueOrDefault();
		}
		set
		{
			__pbn__SortId = value;
		}
	}

	[ProtoMember(12, Name = "product_name")]
	[DefaultValue("")]
	public string ProductName
	{
		get
		{
			return __pbn__ProductName ?? "";
		}
		set
		{
			__pbn__ProductName = value;
		}
	}

	[ProtoMember(13, Name = "product_id")]
	public uint ProductId
	{
		get
		{
			return __pbn__ProductId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductId = value;
		}
	}

	[ProtoMember(14, Name = "product_price")]
	public uint ProductPrice
	{
		get
		{
			return __pbn__ProductPrice.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductPrice = value;
		}
	}

	[ProtoMember(15, Name = "show_setting_str")]
	[DefaultValue("")]
	public string ShowSettingStr
	{
		get
		{
			return __pbn__ShowSettingStr ?? "";
		}
		set
		{
			__pbn__ShowSettingStr = value;
		}
	}

	[ProtoMember(16, Name = "show_new_tag")]
	public uint ShowNewTag
	{
		get
		{
			return __pbn__ShowNewTag.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowNewTag = value;
		}
	}

	[ProtoMember(17, Name = "mall_anchor_list")]
	public uint[] MallAnchorLists { get; set; }

	[ProtoMember(18, Name = "is_ignore")]
	public bool IsIgnore
	{
		get
		{
			return __pbn__IsIgnore.GetValueOrDefault();
		}
		set
		{
			__pbn__IsIgnore = value;
		}
	}

	[ProtoMember(19, Name = "unlock_type")]
	public uint UnlockType
	{
		get
		{
			return __pbn__UnlockType.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockType = value;
		}
	}

	[ProtoMember(20, Name = "unlock_para_int")]
	public uint UnlockParaInt
	{
		get
		{
			return __pbn__UnlockParaInt.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockParaInt = value;
		}
	}

	[ProtoMember(21, Name = "unlock_para_str")]
	[DefaultValue("")]
	public string UnlockParaStr
	{
		get
		{
			return __pbn__UnlockParaStr ?? "";
		}
		set
		{
			__pbn__UnlockParaStr = value;
		}
	}

	[ProtoMember(22, Name = "prepare_level")]
	public uint PrepareLevel
	{
		get
		{
			return __pbn__PrepareLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__PrepareLevel = value;
		}
	}

	[ProtoMember(23, Name = "min_level")]
	public uint MinLevel
	{
		get
		{
			return __pbn__MinLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MinLevel = value;
		}
	}

	[ProtoMember(24, Name = "max_level")]
	public uint MaxLevel
	{
		get
		{
			return __pbn__MaxLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxLevel = value;
		}
	}

	[ProtoMember(25, Name = "discount")]
	public GoodsDiscount Discount { get; set; }

	[ProtoMember(26, Name = "is_popup")]
	public bool IsPopup
	{
		get
		{
			return __pbn__IsPopup.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPopup = value;
		}
	}

	[ProtoMember(27, Name = "is_hide_price")]
	public bool IsHidePrice
	{
		get
		{
			return __pbn__IsHidePrice.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHidePrice = value;
		}
	}

	[ProtoMember(28, Name = "max_buy_type")]
	public uint MaxBuyType
	{
		get
		{
			return __pbn__MaxBuyType.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxBuyType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGoodsId()
	{
		return __pbn__GoodsId.HasValue;
	}

	public void ResetGoodsId()
	{
		__pbn__GoodsId = null;
	}

	public bool ShouldSerializeBuyTimes()
	{
		return __pbn__BuyTimes.HasValue;
	}

	public void ResetBuyTimes()
	{
		__pbn__BuyTimes = null;
	}

	public bool ShouldSerializeCanBeRefresh()
	{
		return __pbn__CanBeRefresh.HasValue;
	}

	public void ResetCanBeRefresh()
	{
		__pbn__CanBeRefresh = null;
	}

	public bool ShouldSerializerefresh_time_type()
	{
		return __pbn__refresh_time_type.HasValue;
	}

	public void Resetrefresh_time_type()
	{
		__pbn__refresh_time_type = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeshow_type()
	{
		return __pbn__show_type.HasValue;
	}

	public void Resetshow_type()
	{
		__pbn__show_type = null;
	}

	public bool ShouldSerializeUnlockTips()
	{
		return __pbn__UnlockTips != null;
	}

	public void ResetUnlockTips()
	{
		__pbn__UnlockTips = null;
	}

	public bool ShouldSerializePrepareTime()
	{
		return __pbn__PrepareTime.HasValue;
	}

	public void ResetPrepareTime()
	{
		__pbn__PrepareTime = null;
	}

	public bool ShouldSerializeSortId()
	{
		return __pbn__SortId.HasValue;
	}

	public void ResetSortId()
	{
		__pbn__SortId = null;
	}

	public bool ShouldSerializeProductName()
	{
		return __pbn__ProductName != null;
	}

	public void ResetProductName()
	{
		__pbn__ProductName = null;
	}

	public bool ShouldSerializeProductId()
	{
		return __pbn__ProductId.HasValue;
	}

	public void ResetProductId()
	{
		__pbn__ProductId = null;
	}

	public bool ShouldSerializeProductPrice()
	{
		return __pbn__ProductPrice.HasValue;
	}

	public void ResetProductPrice()
	{
		__pbn__ProductPrice = null;
	}

	public bool ShouldSerializeShowSettingStr()
	{
		return __pbn__ShowSettingStr != null;
	}

	public void ResetShowSettingStr()
	{
		__pbn__ShowSettingStr = null;
	}

	public bool ShouldSerializeShowNewTag()
	{
		return __pbn__ShowNewTag.HasValue;
	}

	public void ResetShowNewTag()
	{
		__pbn__ShowNewTag = null;
	}

	public bool ShouldSerializeIsIgnore()
	{
		return __pbn__IsIgnore.HasValue;
	}

	public void ResetIsIgnore()
	{
		__pbn__IsIgnore = null;
	}

	public bool ShouldSerializeUnlockType()
	{
		return __pbn__UnlockType.HasValue;
	}

	public void ResetUnlockType()
	{
		__pbn__UnlockType = null;
	}

	public bool ShouldSerializeUnlockParaInt()
	{
		return __pbn__UnlockParaInt.HasValue;
	}

	public void ResetUnlockParaInt()
	{
		__pbn__UnlockParaInt = null;
	}

	public bool ShouldSerializeUnlockParaStr()
	{
		return __pbn__UnlockParaStr != null;
	}

	public void ResetUnlockParaStr()
	{
		__pbn__UnlockParaStr = null;
	}

	public bool ShouldSerializePrepareLevel()
	{
		return __pbn__PrepareLevel.HasValue;
	}

	public void ResetPrepareLevel()
	{
		__pbn__PrepareLevel = null;
	}

	public bool ShouldSerializeMinLevel()
	{
		return __pbn__MinLevel.HasValue;
	}

	public void ResetMinLevel()
	{
		__pbn__MinLevel = null;
	}

	public bool ShouldSerializeMaxLevel()
	{
		return __pbn__MaxLevel.HasValue;
	}

	public void ResetMaxLevel()
	{
		__pbn__MaxLevel = null;
	}

	public bool ShouldSerializeIsPopup()
	{
		return __pbn__IsPopup.HasValue;
	}

	public void ResetIsPopup()
	{
		__pbn__IsPopup = null;
	}

	public bool ShouldSerializeIsHidePrice()
	{
		return __pbn__IsHidePrice.HasValue;
	}

	public void ResetIsHidePrice()
	{
		__pbn__IsHidePrice = null;
	}

	public bool ShouldSerializeMaxBuyType()
	{
		return __pbn__MaxBuyType.HasValue;
	}

	public void ResetMaxBuyType()
	{
		__pbn__MaxBuyType = null;
	}
}
