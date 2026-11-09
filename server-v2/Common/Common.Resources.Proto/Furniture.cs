using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Furniture : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__PosX;

	private uint? __pbn__PosY;

	private uint? __pbn__Direction;

	private uint? __pbn__Location;

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

	[ProtoMember(2, Name = "pos_x")]
	public uint PosX
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

	[ProtoMember(3, Name = "pos_y")]
	public uint PosY
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

	[ProtoMember(4, Name = "direction")]
	public uint Direction
	{
		get
		{
			return __pbn__Direction.GetValueOrDefault();
		}
		set
		{
			__pbn__Direction = value;
		}
	}

	[ProtoMember(5, Name = "location")]
	public uint Location
	{
		get
		{
			return __pbn__Location.GetValueOrDefault();
		}
		set
		{
			__pbn__Location = value;
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

	public bool ShouldSerializeDirection()
	{
		return __pbn__Direction.HasValue;
	}

	public void ResetDirection()
	{
		__pbn__Direction = null;
	}

	public bool ShouldSerializeLocation()
	{
		return __pbn__Location.HasValue;
	}

	public void ResetLocation()
	{
		__pbn__Location = null;
	}
}
