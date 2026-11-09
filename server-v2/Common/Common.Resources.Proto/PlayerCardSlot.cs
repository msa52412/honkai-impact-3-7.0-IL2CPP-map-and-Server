using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerCardSlot : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Index;

	private uint? __pbn__TargetId;

	[ProtoMember(1, Name = "index")]
	public uint Index
	{
		get
		{
			return __pbn__Index.GetValueOrDefault();
		}
		set
		{
			__pbn__Index = value;
		}
	}

	[ProtoMember(2, Name = "target_id")]
	public uint TargetId
	{
		get
		{
			return __pbn__TargetId.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIndex()
	{
		return __pbn__Index.HasValue;
	}

	public void ResetIndex()
	{
		__pbn__Index = null;
	}

	public bool ShouldSerializeTargetId()
	{
		return __pbn__TargetId.HasValue;
	}

	public void ResetTargetId()
	{
		__pbn__TargetId = null;
	}
}
