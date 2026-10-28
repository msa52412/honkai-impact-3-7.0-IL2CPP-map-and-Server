using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FarmSlotDetail : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__SlotStatus;

	private uint? __pbn__ProduceBeginTime;

	private uint? __pbn__ProduceEndTime;

	private uint? __pbn__ProduceMaterialId;

	private uint? __pbn__HasProduceTimesDaily;

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

	[ProtoMember(2, Name = "slot_status")]
	public uint SlotStatus
	{
		get
		{
			return __pbn__SlotStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__SlotStatus = value;
		}
	}

	[ProtoMember(3, Name = "produce_begin_time")]
	public uint ProduceBeginTime
	{
		get
		{
			return __pbn__ProduceBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ProduceBeginTime = value;
		}
	}

	[ProtoMember(4, Name = "produce_end_time")]
	public uint ProduceEndTime
	{
		get
		{
			return __pbn__ProduceEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ProduceEndTime = value;
		}
	}

	[ProtoMember(5, Name = "produce_material_id")]
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

	[ProtoMember(6, Name = "has_produce_times_daily")]
	public uint HasProduceTimesDaily
	{
		get
		{
			return __pbn__HasProduceTimesDaily.GetValueOrDefault();
		}
		set
		{
			__pbn__HasProduceTimesDaily = value;
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

	public bool ShouldSerializeSlotStatus()
	{
		return __pbn__SlotStatus.HasValue;
	}

	public void ResetSlotStatus()
	{
		__pbn__SlotStatus = null;
	}

	public bool ShouldSerializeProduceBeginTime()
	{
		return __pbn__ProduceBeginTime.HasValue;
	}

	public void ResetProduceBeginTime()
	{
		__pbn__ProduceBeginTime = null;
	}

	public bool ShouldSerializeProduceEndTime()
	{
		return __pbn__ProduceEndTime.HasValue;
	}

	public void ResetProduceEndTime()
	{
		__pbn__ProduceEndTime = null;
	}

	public bool ShouldSerializeProduceMaterialId()
	{
		return __pbn__ProduceMaterialId.HasValue;
	}

	public void ResetProduceMaterialId()
	{
		__pbn__ProduceMaterialId = null;
	}

	public bool ShouldSerializeHasProduceTimesDaily()
	{
		return __pbn__HasProduceTimesDaily.HasValue;
	}

	public void ResetHasProduceTimesDaily()
	{
		__pbn__HasProduceTimesDaily = null;
	}
}
