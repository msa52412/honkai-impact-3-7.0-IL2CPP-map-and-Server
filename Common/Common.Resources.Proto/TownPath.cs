using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownPath : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Position;

	private uint? __pbn__NextPosition;

	[ProtoMember(1, Name = "position")]
	public uint Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(2, Name = "next_position")]
	public uint NextPosition
	{
		get
		{
			return __pbn__NextPosition.GetValueOrDefault();
		}
		set
		{
			__pbn__NextPosition = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeNextPosition()
	{
		return __pbn__NextPosition.HasValue;
	}

	public void ResetNextPosition()
	{
		__pbn__NextPosition = null;
	}
}
