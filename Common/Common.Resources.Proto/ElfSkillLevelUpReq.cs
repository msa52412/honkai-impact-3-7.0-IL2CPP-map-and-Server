using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfSkillLevelUpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2123
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfId;

	private uint? __pbn__ElfSkillId;

	[ProtoMember(1, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
		}
	}

	[ProtoMember(2, Name = "elf_skill_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}

	public bool ShouldSerializeElfSkillId()
	{
		return __pbn__ElfSkillId.HasValue;
	}

	public void ResetElfSkillId()
	{
		__pbn__ElfSkillId = null;
	}
}
