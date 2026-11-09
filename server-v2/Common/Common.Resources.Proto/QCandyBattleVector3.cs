using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleVector3 : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__X;

	private int? __pbn__Y;

	private int? __pbn__Z;

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

	[ProtoMember(3, Name = "z")]
	public int Z
	{
		get
		{
			return __pbn__Z.GetValueOrDefault();
		}
		set
		{
			__pbn__Z = value;
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

	public bool ShouldSerializeZ()
	{
		return __pbn__Z.HasValue;
	}

	public void ResetZ()
	{
		__pbn__Z = null;
	}
}
