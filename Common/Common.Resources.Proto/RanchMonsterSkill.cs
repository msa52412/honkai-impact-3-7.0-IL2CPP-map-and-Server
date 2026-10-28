using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchMonsterSkill : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillId;

	private bool? __pbn__IsLocked;

	[ProtoMember(1, Name = "skill_id")]
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

	[ProtoMember(2, Name = "is_locked")]
	public bool IsLocked
	{
		get
		{
			return __pbn__IsLocked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLocked = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}

	public bool ShouldSerializeIsLocked()
	{
		return __pbn__IsLocked.HasValue;
	}

	public void ResetIsLocked()
	{
		__pbn__IsLocked = null;
	}
}
