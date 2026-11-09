using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CabinTech : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__PosX;

	private int? __pbn__PosY;

	[ProtoMember(1, Name = "pos_x")]
	public int PosX
	{
		get
		{
			return __pbn__PosX.GetValueOrDefault();
		}
		set
		{
			__pbn__PosX = value;
		}
	}

	[ProtoMember(2, Name = "pos_y")]
	public int PosY
	{
		get
		{
			return __pbn__PosY.GetValueOrDefault();
		}
		set
		{
			__pbn__PosY = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePosX()
	{
		return __pbn__PosX.HasValue;
	}

	public void ResetPosX()
	{
		__pbn__PosX = null;
	}

	public bool ShouldSerializePosY()
	{
		return __pbn__PosY.HasValue;
	}

	public void ResetPosY()
	{
		__pbn__PosY = null;
	}
}
