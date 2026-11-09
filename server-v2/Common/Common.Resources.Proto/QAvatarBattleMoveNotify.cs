using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleMoveNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5603
	}

	private IExtension __pbn__extensionData;

	private float? __pbn__SpeedX;

	private float? __pbn__SpeedY;

	private float? __pbn__RotationX;

	private float? __pbn__RotationY;

	[ProtoMember(1, Name = "speed_x")]
	public float SpeedX
	{
		get
		{
			return __pbn__SpeedX.GetValueOrDefault();
		}
		set
		{
			__pbn__SpeedX = value;
		}
	}

	[ProtoMember(2, Name = "speed_y")]
	public float SpeedY
	{
		get
		{
			return __pbn__SpeedY.GetValueOrDefault();
		}
		set
		{
			__pbn__SpeedY = value;
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

	public bool ShouldSerializeSpeedX()
	{
		return __pbn__SpeedX.HasValue;
	}

	public void ResetSpeedX()
	{
		__pbn__SpeedX = null;
	}

	public bool ShouldSerializeSpeedY()
	{
		return __pbn__SpeedY.HasValue;
	}

	public void ResetSpeedY()
	{
		__pbn__SpeedY = null;
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
