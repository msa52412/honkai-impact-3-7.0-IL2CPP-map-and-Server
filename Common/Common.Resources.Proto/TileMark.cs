using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileMark : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MarkId;

	private uint? __pbn__MarkValue;

	[ProtoMember(1, Name = "mark_id")]
	public uint MarkId
	{
		get
		{
			return __pbn__MarkId.GetValueOrDefault();
		}
		set
		{
			__pbn__MarkId = value;
		}
	}

	[ProtoMember(2, Name = "mark_value")]
	public uint MarkValue
	{
		get
		{
			return __pbn__MarkValue.GetValueOrDefault();
		}
		set
		{
			__pbn__MarkValue = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMarkId()
	{
		return __pbn__MarkId.HasValue;
	}

	public void ResetMarkId()
	{
		__pbn__MarkId = null;
	}

	public bool ShouldSerializeMarkValue()
	{
		return __pbn__MarkValue.HasValue;
	}

	public void ResetMarkValue()
	{
		__pbn__MarkValue = null;
	}
}
