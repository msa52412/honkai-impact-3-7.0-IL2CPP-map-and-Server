using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StartFarmProduceReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1389
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__ProduceMaterialId;

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

	[ProtoMember(2, Name = "produce_material_id")]
	public uint ProduceMaterialId
	{
		get
		{
			return __pbn__ProduceMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProduceMaterialId = value;
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

	public bool ShouldSerializeProduceMaterialId()
	{
		return __pbn__ProduceMaterialId.HasValue;
	}

	public void ResetProduceMaterialId()
	{
		__pbn__ProduceMaterialId = null;
	}
}
