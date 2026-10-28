using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldPosition : IExtensible
{
	private IExtension __pbn__extensionData;

	private float? __pbn__X;

	private float? __pbn__Y;

	private float? __pbn__Z;

	private float? __pbn__Rotation;

	private float? __pbn__Speed;

	private ulong? __pbn__TimeMsU64;

	private float? __pbn__AimRotation;

	private bool? __pbn__IsTeleport;

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

	[ProtoMember(3, Name = "z")]
	public float Z
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

	[ProtoMember(4, Name = "rotation")]
	public float Rotation
	{
		get
		{
			return __pbn__Rotation.GetValueOrDefault();
		}
		set
		{
			__pbn__Rotation = value;
		}
	}

	[ProtoMember(5, Name = "speed")]
	public float Speed
	{
		get
		{
			return __pbn__Speed.GetValueOrDefault();
		}
		set
		{
			__pbn__Speed = value;
		}
	}

	[ProtoMember(6, Name = "time_ms_u64")]
	public ulong TimeMsU64
	{
		get
		{
			return __pbn__TimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__TimeMsU64 = value;
		}
	}

	[ProtoMember(7, Name = "aim_rotation")]
	public float AimRotation
	{
		get
		{
			return __pbn__AimRotation.GetValueOrDefault();
		}
		set
		{
			__pbn__AimRotation = value;
		}
	}

	[ProtoMember(8, Name = "is_teleport")]
	public bool IsTeleport
	{
		get
		{
			return __pbn__IsTeleport.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTeleport = value;
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

	public bool ShouldSerializeRotation()
	{
		return __pbn__Rotation.HasValue;
	}

	public void ResetRotation()
	{
		__pbn__Rotation = null;
	}

	public bool ShouldSerializeSpeed()
	{
		return __pbn__Speed.HasValue;
	}

	public void ResetSpeed()
	{
		__pbn__Speed = null;
	}

	public bool ShouldSerializeTimeMsU64()
	{
		return __pbn__TimeMsU64.HasValue;
	}

	public void ResetTimeMsU64()
	{
		__pbn__TimeMsU64 = null;
	}

	public bool ShouldSerializeAimRotation()
	{
		return __pbn__AimRotation.HasValue;
	}

	public void ResetAimRotation()
	{
		__pbn__AimRotation = null;
	}

	public bool ShouldSerializeIsTeleport()
	{
		return __pbn__IsTeleport.HasValue;
	}

	public void ResetIsTeleport()
	{
		__pbn__IsTeleport = null;
	}
}
