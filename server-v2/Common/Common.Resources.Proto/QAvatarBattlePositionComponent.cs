using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattlePositionComponent : IExtensible
{
	private IExtension __pbn__extensionData;

	private float? __pbn__X;

	private float? __pbn__Y;

	private float? __pbn__RotationX;

	private float? __pbn__RotationY;

	[ProtoMember(1, Name = "x")]
	public float X
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
	public float Y
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

	[ProtoMember(3, Name = "rotation_x")]
	public float RotationX
	{
		get
		{
			return __pbn__RotationX.GetValueOrDefault();
		}
		set
		{
			__pbn__RotationX = value;
		}
	}

	[ProtoMember(4, Name = "rotation_y")]
	public float RotationY
	{
		get
		{
			return __pbn__RotationY.GetValueOrDefault();
		}
		set
		{
			__pbn__RotationY = value;
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

	public bool ShouldSerializeRotationX()
	{
		return __pbn__RotationX.HasValue;
	}

	public void ResetRotationX()
	{
		__pbn__RotationX = null;
	}

	public bool ShouldSerializeRotationY()
	{
		return __pbn__RotationY.HasValue;
	}

	public void ResetRotationY()
	{
		__pbn__RotationY = null;
	}
}
