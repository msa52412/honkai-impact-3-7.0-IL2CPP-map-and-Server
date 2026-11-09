using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AdventureEquipGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private bool? __pbn__IsEnableBaodi;

	private uint? __pbn__GachaTimes;

	private uint? __pbn__NewProtectEquipProb;

	private uint? __pbn__NextAssureNewEquipNeedTimes;

	[ProtoMember(1, Name = "ticket_hcoin_cost")]
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

	[ProtoMember(2, Name = "ticket_material_id")]
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

	[ProtoMember(3, Name = "is_enable_baodi")]
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

	[ProtoMember(4, Name = "gacha_times")]
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

	[ProtoMember(5, Name = "new_protect_equip_prob")]
	public uint NewProtectEquipProb
	{
		get
		{
			return __pbn__NewProtectEquipProb.GetValueOrDefault();
		}
		set
		{
			__pbn__NewProtectEquipProb = value;
		}
	}

	[ProtoMember(6, Name = "next_assure_new_equip_need_times")]
	public uint NextAssureNewEquipNeedTimes
	{
		get
		{
			return __pbn__NextAssureNewEquipNeedTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__NextAssureNewEquipNeedTimes = value;
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

	public bool ShouldSerializeIsEnableBaodi()
	{
		return __pbn__IsEnableBaodi.HasValue;
	}

	public void ResetIsEnableBaodi()
	{
		__pbn__IsEnableBaodi = null;
	}

	public bool ShouldSerializeGachaTimes()
	{
		return __pbn__GachaTimes.HasValue;
	}

	public void ResetGachaTimes()
	{
		__pbn__GachaTimes = null;
	}

	public bool ShouldSerializeNewProtectEquipProb()
	{
		return __pbn__NewProtectEquipProb.HasValue;
	}

	public void ResetNewProtectEquipProb()
	{
		__pbn__NewProtectEquipProb = null;
	}

	public bool ShouldSerializeNextAssureNewEquipNeedTimes()
	{
		return __pbn__NextAssureNewEquipNeedTimes.HasValue;
	}

	public void ResetNextAssureNewEquipNeedTimes()
	{
		__pbn__NextAssureNewEquipNeedTimes = null;
	}
}
