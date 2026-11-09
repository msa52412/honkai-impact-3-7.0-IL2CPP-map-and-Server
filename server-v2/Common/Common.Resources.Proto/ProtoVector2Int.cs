using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ProtoVector2Int : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__X;

	private int? __pbn__Y;

	[ProtoMember(1, Name = "x")]
	public int X
	{
		get
		{
			return __pbn__X.GetValueOrDefault();
		}
		set
		{
			__pbn__X = value;
		}
	}

	[ProtoMember(2, Name = "y")]
	public int Y
	{
		get
		{
			return __pbn__Y.GetValueOrDefault();
		}
		set
		{
			__pbn__Y = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeX()
	{
		return __pbn__X.HasValue;
	}

	public void ResetX()
	{
		__pbn__X = null;
	}

	public bool ShouldSerializeY()
	{
		return __pbn__Y.HasValue;
	}

	public void ResetY()
	{
		__pbn__Y = null;
	}
}
