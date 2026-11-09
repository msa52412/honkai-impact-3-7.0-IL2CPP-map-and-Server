using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleStartSkillReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5604
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillType;

	private float? __pbn__RotationX;

	private float? __pbn__RotationY;

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
}
