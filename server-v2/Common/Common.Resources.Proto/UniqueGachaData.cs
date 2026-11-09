using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UniqueGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private uint? __pbn__LeftGachaTimes;

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

	[ProtoMember(7, Name = "left_gacha_times")]
	public uint LeftGachaTimes
	{
		get
		{
			return __pbn__LeftGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftGachaTimes = value;
		}
	}

	[ProtoMember(8, Name = "select_item_pool")]
	public UniqueGachaSelectItemPool SelectItemPool { get; set; }

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

	public bool ShouldSerializeLeftGachaTimes()
	{
		return __pbn__LeftGachaTimes.HasValue;
	}

	public void ResetLeftGachaTimes()
	{
		__pbn__LeftGachaTimes = null;
	}
}
