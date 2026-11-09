using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CustomGachaData : IExtensible
{
	[ProtoContract]
	public enum ProtectDisplayType
	{
		[ProtoEnum(Name = "NO_DISPLAY")]
		NoDisplay,
		[ProtoEnum(Name = "AVATAR_DISPLAY")]
		AvatarDisplay,
		[ProtoEnum(Name = "EQUIP_DISPLAY")]
		EquipDisplay
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private uint? __pbn__CostMaterialId;

	private uint? __pbn__CostMaterialNum;

	private bool? __pbn__IsEnableBaodi;

	private GachaType? __pbn__GachaType;

	private uint? __pbn__TotalGachaTimes;

	private uint? __pbn__GachaTimes;

	private uint? __pbn__DisplayMaxTimes;

	private uint? __pbn__NoProtectGachaTimes;

	private ProtectDisplayType? __pbn__protect_display_type;

	private uint? __pbn__DisplayKeyAvatar;

	private uint? __pbn__DisplayVideoAvatar;

	private uint? __pbn__ShiningType;

	private uint? __pbn__GachaPanelType;

	private string __pbn__DisplayExtraPara;

	private uint? __pbn__ExId;

	private uint? __pbn__ExEnergy;

	private uint? __pbn__ExGachaNum;

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

	[ProtoMember(6, Name = "is_enable_baodi")]
	public bool IsEnableBaodi
	{
		get
		{
			return __pbn__IsEnableBaodi.GetValueOrDefault();
		}
		set
		{
			__pbn__IsEnableBaodi = value;
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

	[ProtoMember(10, Name = "total_gacha_times")]
	public uint TotalGachaTimes
	{
		get
		{
			return __pbn__TotalGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalGachaTimes = value;
		}
	}

	[ProtoMember(11, Name = "gacha_times")]
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

	[ProtoMember(12, Name = "item_list")]
	public List<CustomGachaItemData> ItemLists { get; } = new List<CustomGachaItemData>();


	[ProtoMember(13, Name = "display_max_times")]
	public uint DisplayMaxTimes
	{
		get
		{
			return __pbn__DisplayMaxTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayMaxTimes = value;
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

	[ProtoMember(15)]
	[DefaultValue(ProtectDisplayType.NoDisplay)]
	public ProtectDisplayType protect_display_type
	{
		get
		{
			return __pbn__protect_display_type.GetValueOrDefault();
		}
		set
		{
			__pbn__protect_display_type = value;
		}
	}

	[ProtoMember(16, Name = "display_key_avatar")]
	public uint DisplayKeyAvatar
	{
		get
		{
			return __pbn__DisplayKeyAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayKeyAvatar = value;
		}
	}

	[ProtoMember(17, Name = "display_video_avatar")]
	public uint DisplayVideoAvatar
	{
		get
		{
			return __pbn__DisplayVideoAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayVideoAvatar = value;
		}
	}

	[ProtoMember(18, Name = "droped_up_item_list")]
	public uint[] DropedUpItemLists { get; set; }

	[ProtoMember(19, Name = "shining_type")]
	public uint ShiningType
	{
		get
		{
			return __pbn__ShiningType.GetValueOrDefault();
		}
		set
		{
			__pbn__ShiningType = value;
		}
	}

	[ProtoMember(20, Name = "gacha_panel_type")]
	public uint GachaPanelType
	{
		get
		{
			return __pbn__GachaPanelType.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaPanelType = value;
		}
	}

	[ProtoMember(21, Name = "display_key_item_list")]
	public uint[] DisplayKeyItemLists { get; set; }

	[ProtoMember(22, Name = "display_extra_para")]
	[DefaultValue("")]
	public string DisplayExtraPara
	{
		get
		{
			return __pbn__DisplayExtraPara ?? "";
		}
		set
		{
			__pbn__DisplayExtraPara = value;
		}
	}

	[ProtoMember(23, Name = "ex_id")]
	public uint ExId
	{
		get
		{
			return __pbn__ExId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExId = value;
		}
	}

	[ProtoMember(24, Name = "ex_energy")]
	public uint ExEnergy
	{
		get
		{
			return __pbn__ExEnergy.GetValueOrDefault();
		}
		set
		{
			__pbn__ExEnergy = value;
		}
	}

	[ProtoMember(25, Name = "ex_gacha_num")]
	public uint ExGachaNum
	{
		get
		{
			return __pbn__ExGachaNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ExGachaNum = value;
		}
	}

	[ProtoMember(26, Name = "gacha_ex_reward_info")]
	public GachaExRewardInfo GachaExRewardInfo { get; set; }

	[ProtoMember(27, Name = "protect_display_info")]
	public GachaProtectDisplayInfo ProtectDisplayInfo { get; set; }

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

	public bool ShouldSerializeIsEnableBaodi()
	{
		return __pbn__IsEnableBaodi.HasValue;
	}

	public void ResetIsEnableBaodi()
	{
		__pbn__IsEnableBaodi = null;
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}

	public bool ShouldSerializeTotalGachaTimes()
	{
		return __pbn__TotalGachaTimes.HasValue;
	}

	public void ResetTotalGachaTimes()
	{
		__pbn__TotalGachaTimes = null;
	}

	public bool ShouldSerializeGachaTimes()
	{
		return __pbn__GachaTimes.HasValue;
	}

	public void ResetGachaTimes()
	{
		__pbn__GachaTimes = null;
	}

	public bool ShouldSerializeDisplayMaxTimes()
	{
		return __pbn__DisplayMaxTimes.HasValue;
	}

	public void ResetDisplayMaxTimes()
	{
		__pbn__DisplayMaxTimes = null;
	}

	public bool ShouldSerializeNoProtectGachaTimes()
	{
		return __pbn__NoProtectGachaTimes.HasValue;
	}

	public void ResetNoProtectGachaTimes()
	{
		__pbn__NoProtectGachaTimes = null;
	}

	public bool ShouldSerializeprotect_display_type()
	{
		return __pbn__protect_display_type.HasValue;
	}

	public void Resetprotect_display_type()
	{
		__pbn__protect_display_type = null;
	}

	public bool ShouldSerializeDisplayKeyAvatar()
	{
		return __pbn__DisplayKeyAvatar.HasValue;
	}

	public void ResetDisplayKeyAvatar()
	{
		__pbn__DisplayKeyAvatar = null;
	}

	public bool ShouldSerializeDisplayVideoAvatar()
	{
		return __pbn__DisplayVideoAvatar.HasValue;
	}

	public void ResetDisplayVideoAvatar()
	{
		__pbn__DisplayVideoAvatar = null;
	}

	public bool ShouldSerializeShiningType()
	{
		return __pbn__ShiningType.HasValue;
	}

	public void ResetShiningType()
	{
		__pbn__ShiningType = null;
	}

	public bool ShouldSerializeGachaPanelType()
	{
		return __pbn__GachaPanelType.HasValue;
	}

	public void ResetGachaPanelType()
	{
		__pbn__GachaPanelType = null;
	}

	public bool ShouldSerializeDisplayExtraPara()
	{
		return __pbn__DisplayExtraPara != null;
	}

	public void ResetDisplayExtraPara()
	{
		__pbn__DisplayExtraPara = null;
	}

	public bool ShouldSerializeExId()
	{
		return __pbn__ExId.HasValue;
	}

	public void ResetExId()
	{
		__pbn__ExId = null;
	}

	public bool ShouldSerializeExEnergy()
	{
		return __pbn__ExEnergy.HasValue;
	}

	public void ResetExEnergy()
	{
		__pbn__ExEnergy = null;
	}

	public bool ShouldSerializeExGachaNum()
	{
		return __pbn__ExGachaNum.HasValue;
	}

	public void ResetExGachaNum()
	{
		__pbn__ExGachaNum = null;
	}
}
