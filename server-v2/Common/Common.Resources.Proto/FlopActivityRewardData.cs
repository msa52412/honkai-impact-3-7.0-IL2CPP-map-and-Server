using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FlopActivityRewardData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Index;

	private uint? __pbn__State;

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

	[ProtoMember(2, Name = "state")]
	public uint State
	{
		get
		{
			return __pbn__State.GetValueOrDefault();
		}
		set
		{
			__pbn__State = value;
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

	public bool ShouldSerializeState()
	{
		return __pbn__State.HasValue;
	}

	public void ResetState()
	{
		__pbn__State = null;
	}
}
