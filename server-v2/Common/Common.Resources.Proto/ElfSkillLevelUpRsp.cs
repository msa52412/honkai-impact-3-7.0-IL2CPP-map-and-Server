using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfSkillLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2124
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ELF_LOCKED")]
		ElfLocked,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack,
		[ProtoEnum(Name = "MATERIAL_NOT_ENOUGH")]
		MaterialNotEnough,
		[ProtoEnum(Name = "LEVEL_FULL")]
		LevelFull,
		[ProtoEnum(Name = "ELF_STAR_NOT_ENOUGH")]
		ElfStarNotEnough,
		[ProtoEnum(Name = "PRE_SKILL_LEVEL_NOT_ENOUGH")]
		PreSkillLevelNotEnough,
		[ProtoEnum(Name = "ELF_LEVEL_NOT_ENOUGH")]
		ElfLevelNotEnough
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "elf_skill")]
	public ElfSkill ElfSkill { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
