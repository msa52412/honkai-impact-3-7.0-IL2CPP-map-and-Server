using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PrayGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private uint? __pbn__CostMaterialId;

	private uint? __pbn__CostMaterialNum;

	private uint? __pbn__MaxGachaTimes;

	private uint? __pbn__GachaTimes;

	private bool? __pbn__IsFinished;

	private uint? __pbn__GachaId;

	private uint? __pbn__PrayValuePerGacha;

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

	[ProtoMember(8, Name = "max_gacha_times")]
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

	[ProtoMember(9, Name = "gacha_times")]
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

	[ProtoMember(10, Name = "is_finished")]
	public bool IsFinished
	{
		get
		{
			return __pbn__IsFinished.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinished = value;
		}
	}

	[ProtoMember(11, Name = "protect_item_list")]
	public List<GachaItem> ProtectItemLists { get; } = new List<GachaItem>();


	[ProtoMember(12, Name = "pool_item_list")]
	public List<PrayGachaItemData> PoolItemLists { get; } = new List<PrayGachaItemData>();


	[ProtoMember(13, Name = "gacha_id")]
	public uint GachaId
	{
		get
		{
			return __pbn__GachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaId = value;
		}
	}

	[ProtoMember(14, Name = "pray_value_per_gacha")]
	public uint PrayValuePerGacha
	{
		get
		{
			return __pbn__PrayValuePerGacha.GetValueOrDefault();
		}
		set
		{
			__pbn__PrayValuePerGacha = value;
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

	public bool ShouldSerializeIsFinished()
	{
		return __pbn__IsFinished.HasValue;
	}

	public void ResetIsFinished()
	{
		__pbn__IsFinished = null;
	}

	public bool ShouldSerializeGachaId()
	{
		return __pbn__GachaId.HasValue;
	}

	public void ResetGachaId()
	{
		__pbn__GachaId = null;
	}

	public bool ShouldSerializePrayValuePerGacha()
	{
		return __pbn__PrayValuePerGacha.HasValue;
	}

	public void ResetPrayValuePerGacha()
	{
		__pbn__PrayValuePerGacha = null;
	}
}
