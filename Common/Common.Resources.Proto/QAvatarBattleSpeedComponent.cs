using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleSpeedComponent : IExtensible
{
	private IExtension __pbn__extensionData;

	private float? __pbn__SpeedX;

	private float? __pbn__SpeedY;

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
}
