using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NinjaSlotStrengthenReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4402
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__MaterialId;

	private uint? __pbn__CostNum;

	[ProtoMember(1, Name = "slot_id")]
	public uint SlotId
	{
		get
		{
			return __pbn__SlotId.GetValueOrDefault();
		}
		set
		{
			__pbn__SlotId = value;
		}
	}

	[ProtoMember(2, Name = "material_id")]
	public uint MaterialId
	{
		get
		{
			return __pbn__MaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialId = value;
		}
	}

	[ProtoMember(3, Name = "cost_num")]
	public uint CostNum
	{
		get
		{
			return __pbn__CostNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CostNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSlotId()
	{
		return __pbn__SlotId.HasValue;
	}

	public void ResetSlotId()
	{
		__pbn__SlotId = null;
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializeCostNum()
	{
		return __pbn__CostNum.HasValue;
	}

	public void ResetCostNum()
	{
		__pbn__CostNum = null;
	}
}
