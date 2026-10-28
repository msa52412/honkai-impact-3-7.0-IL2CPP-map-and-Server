using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OptionalGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChoosedSubGachaId;

	private uint? __pbn__ResetLeftTimes;

	private string __pbn__TitleImage;

	private uint? __pbn__DisplayMaxTimes;

	private uint? __pbn__NoProtectGachaTimes;

	private bool? __pbn__IsProtectDisplay;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private uint? __pbn__ExId;

	private uint? __pbn__ExEnergy;

	private uint? __pbn__ExGachaNum;

	[ProtoMember(1, Name = "choosed_sub_gacha_id")]
	public uint ChoosedSubGachaId
	{
		get
		{
			return __pbn__ChoosedSubGachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChoosedSubGachaId = value;
		}
	}

	[ProtoMember(2, Name = "candidate_gacha_list")]
	public List<OptionalGachaItemData> CandidateGachaLists { get; } = new List<OptionalGachaItemData>();


	[ProtoMember(3, Name = "reset_left_times")]
	public uint ResetLeftTimes
	{
		get
		{
			return __pbn__ResetLeftTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetLeftTimes = value;
		}
	}

	[ProtoMember(4, Name = "title_image")]
	[DefaultValue("")]
	public string TitleImage
	{
		get
		{
			return __pbn__TitleImage ?? "";
		}
		set
		{
			__pbn__TitleImage = value;
		}
	}

	[ProtoMember(5, Name = "display_max_times")]
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

	[ProtoMember(6, Name = "no_protect_gacha_times")]
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

	[ProtoMember(8, Name = "protect_item_list")]
	public List<ProtectEntry> ProtectItemLists { get; } = new List<ProtectEntry>();


	[ProtoMember(9, Name = "is_protect_display")]
	public bool IsProtectDisplay
	{
		get
		{
			return __pbn__IsProtectDisplay.GetValueOrDefault();
		}
		set
		{
			__pbn__IsProtectDisplay = value;
		}
	}

	[ProtoMember(10, Name = "ticket_hcoin_cost")]
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

	[ProtoMember(11, Name = "ticket_material_id")]
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

	[ProtoMember(12, Name = "wish_well_data")]
	public WishWellData WishWellData { get; set; }

	[ProtoMember(13, Name = "ex_id")]
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

	[ProtoMember(14, Name = "ex_energy")]
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

	[ProtoMember(15, Name = "ex_gacha_num")]
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

	[ProtoMember(16, Name = "protect_display_info")]
	public GachaProtectDisplayInfo ProtectDisplayInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChoosedSubGachaId()
	{
		return __pbn__ChoosedSubGachaId.HasValue;
	}

	public void ResetChoosedSubGachaId()
	{
		__pbn__ChoosedSubGachaId = null;
	}

	public bool ShouldSerializeResetLeftTimes()
	{
		return __pbn__ResetLeftTimes.HasValue;
	}

	public void ResetResetLeftTimes()
	{
		__pbn__ResetLeftTimes = null;
	}

	public bool ShouldSerializeTitleImage()
	{
		return __pbn__TitleImage != null;
	}

	public void ResetTitleImage()
	{
		__pbn__TitleImage = null;
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

	public bool ShouldSerializeIsProtectDisplay()
	{
		return __pbn__IsProtectDisplay.HasValue;
	}

	public void ResetIsProtectDisplay()
	{
		__pbn__IsProtectDisplay = null;
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
