using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BoxGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private uint? __pbn__CostMaterialId;

	private uint? __pbn__CostMaterialNum;

	private bool? __pbn__IsEnableBaodi;

	private uint? __pbn__MaxGachaTimes;

	private uint? __pbn__GachaTimes;

	[ProtoMember(2)]
	public BoxGachaDisplayData box_gacha_data { get; set; }

	[ProtoMember(3, Name = "ticket_hcoin_cost")]
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

	[ProtoMember(4, Name = "ticket_material_id")]
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

	[ProtoMember(5, Name = "cost_material_id")]
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

	[ProtoMember(6, Name = "cost_material_num")]
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

	[ProtoMember(7, Name = "is_enable_baodi")]
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

	[ProtoMember(10, Name = "max_gacha_times")]
	public uint MaxGachaTimes
	{
		get
		{
			return __pbn__MaxGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxGachaTimes = value;
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

	public bool ShouldSerializeMaxGachaTimes()
	{
		return __pbn__MaxGachaTimes.HasValue;
	}

	public void ResetMaxGachaTimes()
	{
		__pbn__MaxGachaTimes = null;
	}

	public bool ShouldSerializeGachaTimes()
	{
		return __pbn__GachaTimes.HasValue;
	}

	public void ResetGachaTimes()
	{
		__pbn__GachaTimes = null;
	}
}
