using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Shop : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ShopId;

	private bool? __pbn__IsOpen;

	private string __pbn__TextMapName;

	private string __pbn__ShopIconPath;

	private uint? __pbn__NextAutoRefreshTime;

	private uint? __pbn__ManualRefreshTimes;

	private uint? __pbn__RefreshItem;

	private uint? __pbn__NextRefreshCost;

	private uint? __pbn__MaxManualRefreshTimes;

	private uint? __pbn__ShopType;

	private uint? __pbn__ScheduleChangeTime;

	private string __pbn__ShopName;

	private uint? __pbn__UnlockLevel;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private bool? __pbn__IsHot;

	private uint? __pbn__MultiCurrency;

	private bool? __pbn__IsDoubleConfirm;

	private bool? __pbn__IsShow;

	private uint? __pbn__SortPriority;

	private uint? __pbn__FreeManualRefreshTimes;

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

	[ProtoMember(2, Name = "is_open")]
	public bool IsOpen
	{
		get
		{
			return __pbn__IsOpen.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOpen = value;
		}
	}

	[ProtoMember(3, Name = "text_map_name")]
	[DefaultValue("")]
	public string TextMapName
	{
		get
		{
			return __pbn__TextMapName ?? "";
		}
		set
		{
			__pbn__TextMapName = value;
		}
	}

	[ProtoMember(4, Name = "shop_icon_path")]
	[DefaultValue("")]
	public string ShopIconPath
	{
		get
		{
			return __pbn__ShopIconPath ?? "";
		}
		set
		{
			__pbn__ShopIconPath = value;
		}
	}

	[ProtoMember(5, Name = "goods_list")]
	public List<Goods> GoodsLists { get; } = new List<Goods>();


	[ProtoMember(6, Name = "next_auto_refresh_time")]
	public uint NextAutoRefreshTime
	{
		get
		{
			return __pbn__NextAutoRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextAutoRefreshTime = value;
		}
	}

	[ProtoMember(7, Name = "manual_refresh_times")]
	public uint ManualRefreshTimes
	{
		get
		{
			return __pbn__ManualRefreshTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ManualRefreshTimes = value;
		}
	}

	[ProtoMember(8, Name = "refresh_item")]
	public uint RefreshItem
	{
		get
		{
			return __pbn__RefreshItem.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshItem = value;
		}
	}

	[ProtoMember(9, Name = "next_refresh_cost")]
	public uint NextRefreshCost
	{
		get
		{
			return __pbn__NextRefreshCost.GetValueOrDefault();
		}
		set
		{
			__pbn__NextRefreshCost = value;
		}
	}

	[ProtoMember(10, Name = "max_manual_refresh_times")]
	public uint MaxManualRefreshTimes
	{
		get
		{
			return __pbn__MaxManualRefreshTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxManualRefreshTimes = value;
		}
	}

	[ProtoMember(11, Name = "shop_type")]
	public uint ShopType
	{
		get
		{
			return __pbn__ShopType.GetValueOrDefault();
		}
		set
		{
			__pbn__ShopType = value;
		}
	}

	[ProtoMember(12, Name = "currency_list")]
	public uint[] CurrencyLists { get; set; }

	[ProtoMember(13, Name = "schedule_change_time")]
	public uint ScheduleChangeTime
	{
		get
		{
			return __pbn__ScheduleChangeTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleChangeTime = value;
		}
	}

	[ProtoMember(14, Name = "shop_name")]
	[DefaultValue("")]
	public string ShopName
	{
		get
		{
			return __pbn__ShopName ?? "";
		}
		set
		{
			__pbn__ShopName = value;
		}
	}

	[ProtoMember(15, Name = "unlock_level")]
	public uint UnlockLevel
	{
		get
		{
			return __pbn__UnlockLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockLevel = value;
		}
	}

	[ProtoMember(16, Name = "all_goods_id_list")]
	public uint[] AllGoodsIdLists { get; set; }

	[ProtoMember(17, Name = "begin_time")]
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

	[ProtoMember(18, Name = "end_time")]
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

	[ProtoMember(19, Name = "is_hot")]
	public bool IsHot
	{
		get
		{
			return __pbn__IsHot.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHot = value;
		}
	}

	[ProtoMember(20, Name = "multi_currency")]
	public uint MultiCurrency
	{
		get
		{
			return __pbn__MultiCurrency.GetValueOrDefault();
		}
		set
		{
			__pbn__MultiCurrency = value;
		}
	}

	[ProtoMember(21, Name = "is_double_confirm")]
	public bool IsDoubleConfirm
	{
		get
		{
			return __pbn__IsDoubleConfirm.GetValueOrDefault();
		}
		set
		{
			__pbn__IsDoubleConfirm = value;
		}
	}

	[ProtoMember(22, Name = "is_show")]
	public bool IsShow
	{
		get
		{
			return __pbn__IsShow.GetValueOrDefault();
		}
		set
		{
			__pbn__IsShow = value;
		}
	}

	[ProtoMember(23, Name = "sort_priority")]
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

	[ProtoMember(24, Name = "toolbar_currency_list")]
	public uint[] ToolbarCurrencyLists { get; set; }

	[ProtoMember(25, Name = "new_currency_list")]
	public uint[] NewCurrencyLists { get; set; }

	[ProtoMember(26, Name = "free_manual_refresh_times")]
	public uint FreeManualRefreshTimes
	{
		get
		{
			return __pbn__FreeManualRefreshTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeManualRefreshTimes = value;
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

	public bool ShouldSerializeIsOpen()
	{
		return __pbn__IsOpen.HasValue;
	}

	public void ResetIsOpen()
	{
		__pbn__IsOpen = null;
	}

	public bool ShouldSerializeTextMapName()
	{
		return __pbn__TextMapName != null;
	}

	public void ResetTextMapName()
	{
		__pbn__TextMapName = null;
	}

	public bool ShouldSerializeShopIconPath()
	{
		return __pbn__ShopIconPath != null;
	}

	public void ResetShopIconPath()
	{
		__pbn__ShopIconPath = null;
	}

	public bool ShouldSerializeNextAutoRefreshTime()
	{
		return __pbn__NextAutoRefreshTime.HasValue;
	}

	public void ResetNextAutoRefreshTime()
	{
		__pbn__NextAutoRefreshTime = null;
	}

	public bool ShouldSerializeManualRefreshTimes()
	{
		return __pbn__ManualRefreshTimes.HasValue;
	}

	public void ResetManualRefreshTimes()
	{
		__pbn__ManualRefreshTimes = null;
	}

	public bool ShouldSerializeRefreshItem()
	{
		return __pbn__RefreshItem.HasValue;
	}

	public void ResetRefreshItem()
	{
		__pbn__RefreshItem = null;
	}

	public bool ShouldSerializeNextRefreshCost()
	{
		return __pbn__NextRefreshCost.HasValue;
	}

	public void ResetNextRefreshCost()
	{
		__pbn__NextRefreshCost = null;
	}

	public bool ShouldSerializeMaxManualRefreshTimes()
	{
		return __pbn__MaxManualRefreshTimes.HasValue;
	}

	public void ResetMaxManualRefreshTimes()
	{
		__pbn__MaxManualRefreshTimes = null;
	}

	public bool ShouldSerializeShopType()
	{
		return __pbn__ShopType.HasValue;
	}

	public void ResetShopType()
	{
		__pbn__ShopType = null;
	}

	public bool ShouldSerializeScheduleChangeTime()
	{
		return __pbn__ScheduleChangeTime.HasValue;
	}

	public void ResetScheduleChangeTime()
	{
		__pbn__ScheduleChangeTime = null;
	}

	public bool ShouldSerializeShopName()
	{
		return __pbn__ShopName != null;
	}

	public void ResetShopName()
	{
		__pbn__ShopName = null;
	}

	public bool ShouldSerializeUnlockLevel()
	{
		return __pbn__UnlockLevel.HasValue;
	}

	public void ResetUnlockLevel()
	{
		__pbn__UnlockLevel = null;
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

	public bool ShouldSerializeIsHot()
	{
		return __pbn__IsHot.HasValue;
	}

	public void ResetIsHot()
	{
		__pbn__IsHot = null;
	}

	public bool ShouldSerializeMultiCurrency()
	{
		return __pbn__MultiCurrency.HasValue;
	}

	public void ResetMultiCurrency()
	{
		__pbn__MultiCurrency = null;
	}

	public bool ShouldSerializeIsDoubleConfirm()
	{
		return __pbn__IsDoubleConfirm.HasValue;
	}

	public void ResetIsDoubleConfirm()
	{
		__pbn__IsDoubleConfirm = null;
	}

	public bool ShouldSerializeIsShow()
	{
		return __pbn__IsShow.HasValue;
	}

	public void ResetIsShow()
	{
		__pbn__IsShow = null;
	}

	public bool ShouldSerializeSortPriority()
	{
		return __pbn__SortPriority.HasValue;
	}

	public void ResetSortPriority()
	{
		__pbn__SortPriority = null;
	}

	public bool ShouldSerializeFreeManualRefreshTimes()
	{
		return __pbn__FreeManualRefreshTimes.HasValue;
	}

	public void ResetFreeManualRefreshTimes()
	{
		__pbn__FreeManualRefreshTimes = null;
	}
}
