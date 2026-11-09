using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyGoodsCost : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HcoinCost;

	private uint? __pbn__ScoinCost;

	private uint? __pbn__McoinCost;

	[ProtoMember(1, Name = "hcoin_cost")]
	public uint HcoinCost
	{
		get
		{
			return __pbn__HcoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__HcoinCost = value;
		}
	}

	[ProtoMember(2, Name = "scoin_cost")]
	public uint ScoinCost
	{
		get
		{
			return __pbn__ScoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinCost = value;
		}
	}

	[ProtoMember(3, Name = "mcoin_cost")]
	public uint McoinCost
	{
		get
		{
			return __pbn__McoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__McoinCost = value;
		}
	}

	[ProtoMember(4, Name = "cost_item_list")]
	public List<Material> CostItemLists { get; } = new List<Material>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHcoinCost()
	{
		return __pbn__HcoinCost.HasValue;
	}

	public void ResetHcoinCost()
	{
		__pbn__HcoinCost = null;
	}

	public bool ShouldSerializeScoinCost()
	{
		return __pbn__ScoinCost.HasValue;
	}

	public void ResetScoinCost()
	{
		__pbn__ScoinCost = null;
	}

	public bool ShouldSerializeMcoinCost()
	{
		return __pbn__McoinCost.HasValue;
	}

	public void ResetMcoinCost()
	{
		__pbn__McoinCost = null;
	}
}
