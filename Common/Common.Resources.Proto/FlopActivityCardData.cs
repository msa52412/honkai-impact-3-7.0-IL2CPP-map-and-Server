using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FlopActivityCardData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__State;

	private uint? __pbn__CardNumber;

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

	[ProtoMember(3, Name = "card_number")]
	public uint CardNumber
	{
		get
		{
			return __pbn__CardNumber.GetValueOrDefault();
		}
		set
		{
			__pbn__CardNumber = value;
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

	public bool ShouldSerializeState()
	{
		return __pbn__State.HasValue;
	}

	public void ResetState()
	{
		__pbn__State = null;
	}

	public bool ShouldSerializeCardNumber()
	{
		return __pbn__CardNumber.HasValue;
	}

	public void ResetCardNumber()
	{
		__pbn__CardNumber = null;
	}
}
