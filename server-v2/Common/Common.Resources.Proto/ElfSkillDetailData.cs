using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfSkillDetailData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfSkillId;

	private uint? __pbn__SkillLevel;

	[ProtoMember(1, Name = "elf_skill_id")]
	public uint ElfSkillId
	{
		get
		{
			return __pbn__ElfSkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfSkillId = value;
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

	public bool ShouldSerializeElfSkillId()
	{
		return __pbn__ElfSkillId.HasValue;
	}

	public void ResetElfSkillId()
	{
		__pbn__ElfSkillId = null;
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
