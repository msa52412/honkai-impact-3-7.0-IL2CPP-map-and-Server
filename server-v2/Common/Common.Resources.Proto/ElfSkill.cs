using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfSkill : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillId;

	private uint? __pbn__SkillLevel;

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

	[ProtoMember(2, Name = "skill_level")]
	public uint SkillLevel
	{
		get
		{
			return __pbn__SkillLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillLevel = value;
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

	public bool ShouldSerializeSkillLevel()
	{
		return __pbn__SkillLevel.HasValue;
	}

	public void ResetSkillLevel()
	{
		__pbn__SkillLevel = null;
	}
}
