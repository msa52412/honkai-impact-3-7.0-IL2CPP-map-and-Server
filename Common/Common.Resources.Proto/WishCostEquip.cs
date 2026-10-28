using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WishCostEquip : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CostEquipId;

	private uint? __pbn__CostEquipNum;

	[ProtoMember(1, Name = "cost_equip_id")]
	public uint CostEquipId
	{
		get
		{
			return __pbn__CostEquipId.GetValueOrDefault();
		}
		set
		{
			__pbn__CostEquipId = value;
		}
	}

	[ProtoMember(2, Name = "cost_equip_num")]
	public uint CostEquipNum
	{
		get
		{
			return __pbn__CostEquipNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CostEquipNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCostEquipId()
	{
		return __pbn__CostEquipId.HasValue;
	}

	public void ResetCostEquipId()
	{
		__pbn__CostEquipId = null;
	}

	public bool ShouldSerializeCostEquipNum()
	{
		return __pbn__CostEquipNum.HasValue;
	}

	public void ResetCostEquipNum()
	{
		__pbn__CostEquipNum = null;
	}
}
