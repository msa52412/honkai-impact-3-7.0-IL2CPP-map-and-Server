using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StartFarmProduceRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1390
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SLOT_LOCKED")]
		SlotLocked,
		[ProtoEnum(Name = "SLOT_IN_PRODUCE")]
		SlotInProduce,
		[ProtoEnum(Name = "EQUIPMENT_MATERIAL_LIMIT")]
		EquipmentMaterialLimit,
		[ProtoEnum(Name = "PRODUCE_MATERIAL_LIMIT")]
		ProduceMaterialLimit,
		[ProtoEnum(Name = "PRODUCE_TIMES_LIMIT")]
		ProduceTimesLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__ProduceMaterialId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "slot_id")]
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

	[ProtoMember(3, Name = "produce_material_id")]
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

	[ProtoMember(4, Name = "slot_detail")]
	public FarmSlotDetail SlotDetail { get; set; }

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
