using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleEndSkillReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5607
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillType;

	private float? __pbn__RotationX;

	private float? __pbn__RotationY;

	private float? __pbn__DstPositionX;

	private float? __pbn__DstPositionY;

	[ProtoMember(1, Name = "skill_type")]
	public uint SkillType
	{
		get
		{
			return __pbn__SkillType.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillType = value;
		}
	}

	[ProtoMember(2, Name = "rotation_x")]
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

	[ProtoMember(3, Name = "rotation_y")]
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

	[ProtoMember(4, Name = "dst_position_x")]
	public float DstPositionX
	{
		get
		{
			return __pbn__DstPositionX.GetValueOrDefault();
		}
		set
		{
			__pbn__DstPositionX = value;
		}
	}

	[ProtoMember(5, Name = "dst_position_y")]
	public float DstPositionY
	{
		get
		{
			return __pbn__DstPositionY.GetValueOrDefault();
		}
		set
		{
			__pbn__DstPositionY = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillType()
	{
		return __pbn__SkillType.HasValue;
	}

	public void ResetSkillType()
	{
		__pbn__SkillType = null;
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

	public bool ShouldSerializeDstPositionX()
	{
		return __pbn__DstPositionX.HasValue;
	}

	public void ResetDstPositionX()
	{
		__pbn__DstPositionX = null;
	}

	public bool ShouldSerializeDstPositionY()
	{
		return __pbn__DstPositionY.HasValue;
	}

	public void ResetDstPositionY()
	{
		__pbn__DstPositionY = null;
	}
}
