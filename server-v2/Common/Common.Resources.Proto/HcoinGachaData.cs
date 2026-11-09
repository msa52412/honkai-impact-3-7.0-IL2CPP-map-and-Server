using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class HcoinGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private uint? __pbn__ShiningType;

	private uint? __pbn__GachaPanelType;

	private uint? __pbn__ProtectTypeId;

	private uint? __pbn__ProtectLeftTimes;

	private uint? __pbn__DisplayMaxTimes;

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

	[ProtoMember(7, Name = "wish_well_data")]
	public WishWellData WishWellData { get; set; }

	[ProtoMember(8, Name = "shining_type")]
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

	[ProtoMember(9, Name = "gacha_panel_type")]
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

	[ProtoMember(10, Name = "protect_type_id")]
	public uint ProtectTypeId
	{
		get
		{
			return __pbn__ProtectTypeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProtectTypeId = value;
		}
	}

	[ProtoMember(11, Name = "protect_left_times")]
	public uint ProtectLeftTimes
	{
		get
		{
			return __pbn__ProtectLeftTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ProtectLeftTimes = value;
		}
	}

	[ProtoMember(12, Name = "protect_display_info")]
	public GachaProtectDisplayInfo ProtectDisplayInfo { get; set; }

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

	public bool ShouldSerializeProtectTypeId()
	{
		return __pbn__ProtectTypeId.HasValue;
	}

	public void ResetProtectTypeId()
	{
		__pbn__ProtectTypeId = null;
	}

	public bool ShouldSerializeProtectLeftTimes()
	{
		return __pbn__ProtectLeftTimes.HasValue;
	}

	public void ResetProtectLeftTimes()
	{
		__pbn__ProtectLeftTimes = null;
	}

	public bool ShouldSerializeDisplayMaxTimes()
	{
		return __pbn__DisplayMaxTimes.HasValue;
	}

	public void ResetDisplayMaxTimes()
	{
		__pbn__DisplayMaxTimes = null;
	}
}
