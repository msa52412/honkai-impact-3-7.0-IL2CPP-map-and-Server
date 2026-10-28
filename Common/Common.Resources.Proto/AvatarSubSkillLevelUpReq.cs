using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarSubSkillLevelUpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 50
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__SkillId;

	private uint? __pbn__SubSkillId;

	private bool? __pbn__IsLevelUpAll;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "skill_id")]
	public uint SkillId
	{
		get
		{
			return __pbn__SkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillId = value;
		}
	}

	[ProtoMember(3, Name = "sub_skill_id")]
	public uint SubSkillId
	{
		get
		{
			return __pbn__SubSkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__SubSkillId = value;
		}
	}

	[ProtoMember(4, Name = "is_level_up_all")]
	public bool IsLevelUpAll
	{
		get
		{
			return __pbn__IsLevelUpAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLevelUpAll = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}

	public bool ShouldSerializeSubSkillId()
	{
		return __pbn__SubSkillId.HasValue;
	}

	public void ResetSubSkillId()
	{
		__pbn__SubSkillId = null;
	}

	public bool ShouldSerializeIsLevelUpAll()
	{
		return __pbn__IsLevelUpAll.HasValue;
	}

	public void ResetIsLevelUpAll()
	{
		__pbn__IsLevelUpAll = null;
	}
}
