using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActivityGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private uint? __pbn__CostMaterialId;

	private uint? __pbn__CostMaterialNum;

	private uint? __pbn__BaodiTimes;

	private GachaType? __pbn__GachaType;

	private uint? __pbn__GachaTimes;

	private bool? __pbn__IsProtectRecycle;

	private uint? __pbn__DisplayProtectTimes;

	private uint? __pbn__NoProtectGachaTimes;

	[ProtoMember(2, Name = "ticket_hcoin_cost")]
	public uint TicketHcoinCost
	{
		get
		{
			return __pbn__TicketHcoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__TicketHcoinCost = value;
		}
	}

	[ProtoMember(3, Name = "ticket_material_id")]
	public uint TicketMaterialId
	{
		get
		{
			return __pbn__TicketMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__TicketMaterialId = value;
		}
	}

	[ProtoMember(4, Name = "cost_material_id")]
	public uint CostMaterialId
	{
		get
		{
			return __pbn__CostMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__CostMaterialId = value;
		}
	}

	[ProtoMember(5, Name = "cost_material_num")]
	public uint CostMaterialNum
	{
		get
		{
			return __pbn__CostMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CostMaterialNum = value;
		}
	}

	[ProtoMember(6, Name = "baodi_times")]
	public uint BaodiTimes
	{
		get
		{
			return __pbn__BaodiTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BaodiTimes = value;
		}
	}

	[ProtoMember(9, Name = "gacha_type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType GachaType
	{
		get
		{
			return __pbn__GachaType ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__GachaType = value;
		}
	}

	[ProtoMember(10, Name = "gacha_times")]
	public uint GachaTimes
	{
		get
		{
			return __pbn__GachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaTimes = value;
		}
	}

	[ProtoMember(11, Name = "display_protect_entry_list")]
	public List<ProtectEntry> DisplayProtectEntryLists { get; } = new List<ProtectEntry>();


	[ProtoMember(12, Name = "is_protect_recycle")]
	public bool IsProtectRecycle
	{
		get
		{
			return __pbn__IsProtectRecycle.GetValueOrDefault();
		}
		set
		{
			__pbn__IsProtectRecycle = value;
		}
	}

	[ProtoMember(13, Name = "display_protect_times")]
	public uint DisplayProtectTimes
	{
		get
		{
			return __pbn__DisplayProtectTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayProtectTimes = value;
		}
	}

	[ProtoMember(14, Name = "no_protect_gacha_times")]
	public uint NoProtectGachaTimes
	{
		get
		{
			return __pbn__NoProtectGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__NoProtectGachaTimes = value;
		}
	}

	[ProtoMember(15, Name = "gacha_ex_reward_info")]
	public GachaExRewardInfo GachaExRewardInfo { get; set; }

	[ProtoMember(16, Name = "display_setting_dict")]
	public SettingDict DisplaySettingDict { get; set; }

	[ProtoMember(17, Name = "wish_well_data")]
	public WishWellData WishWellData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTicketHcoinCost()
	{
		return __pbn__TicketHcoinCost.HasValue;
	}

	public void ResetTicketHcoinCost()
	{
		__pbn__TicketHcoinCost = null;
	}

	public bool ShouldSerializeTicketMaterialId()
	{
		return __pbn__TicketMaterialId.HasValue;
	}

	public void ResetTicketMaterialId()
	{
		__pbn__TicketMaterialId = null;
	}

	public bool ShouldSerializeCostMaterialId()
	{
		return __pbn__CostMaterialId.HasValue;
	}

	public void ResetCostMaterialId()
	{
		__pbn__CostMaterialId = null;
	}

	public bool ShouldSerializeCostMaterialNum()
	{
		return __pbn__CostMaterialNum.HasValue;
	}

	public void ResetCostMaterialNum()
	{
		__pbn__CostMaterialNum = null;
	}

	public bool ShouldSerializeBaodiTimes()
	{
		return __pbn__BaodiTimes.HasValue;
	}

	public void ResetBaodiTimes()
	{
		__pbn__BaodiTimes = null;
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}

	public bool ShouldSerializeGachaTimes()
	{
		return __pbn__GachaTimes.HasValue;
	}

	public void ResetGachaTimes()
	{
		__pbn__GachaTimes = null;
	}

	public bool ShouldSerializeIsProtectRecycle()
	{
		return __pbn__IsProtectRecycle.HasValue;
	}

	public void ResetIsProtectRecycle()
	{
		__pbn__IsProtectRecycle = null;
	}

	public bool ShouldSerializeDisplayProtectTimes()
	{
		return __pbn__DisplayProtectTimes.HasValue;
	}

	public void ResetDisplayProtectTimes()
	{
		__pbn__DisplayProtectTimes = null;
	}

	public bool ShouldSerializeNoProtectGachaTimes()
	{
		return __pbn__NoProtectGachaTimes.HasValue;
	}

	public void ResetNoProtectGachaTimes()
	{
		__pbn__NoProtectGachaTimes = null;
	}
}
