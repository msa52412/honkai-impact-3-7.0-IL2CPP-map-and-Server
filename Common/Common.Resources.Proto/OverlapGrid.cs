using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OverlapGrid : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__SummonId;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "summon_id")]
	public uint SummonId
	{
		get
		{
			return __pbn__SummonId.GetValueOrDefault();
		}
		set
		{
			__pbn__SummonId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeSummonId()
	{
		return __pbn__SummonId.HasValue;
	}

	public void ResetSummonId()
	{
		__pbn__SummonId = null;
	}
}
