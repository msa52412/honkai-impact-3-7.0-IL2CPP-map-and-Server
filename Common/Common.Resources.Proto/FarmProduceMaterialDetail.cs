using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FarmProduceMaterialDetail : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__HasProduceTimesTotal;

	[ProtoMember(1, Name = "material_id")]
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

	[ProtoMember(2, Name = "has_produce_times_total")]
	public uint HasProduceTimesTotal
	{
		get
		{
			return __pbn__HasProduceTimesTotal.GetValueOrDefault();
		}
		set
		{
			__pbn__HasProduceTimesTotal = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializeHasProduceTimesTotal()
	{
		return __pbn__HasProduceTimesTotal.HasValue;
	}

	public void ResetHasProduceTimesTotal()
	{
		__pbn__HasProduceTimesTotal = null;
	}
}
