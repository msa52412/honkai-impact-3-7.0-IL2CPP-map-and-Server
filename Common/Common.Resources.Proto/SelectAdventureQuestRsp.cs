using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SelectAdventureQuestRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3903
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_FINISHED")]
		NotFinished,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "LACK_GRAIN")]
		LackGrain,
		[ProtoEnum(Name = "LACK_STAMINA")]
		LackStamina,
		[ProtoEnum(Name = "AVATAR_LOCK")]
		AvatarLock,
		[ProtoEnum(Name = "AVATAR_IN_QUEST")]
		AvatarInQuest,
		[ProtoEnum(Name = "AVATAR_NUM_ERROR")]
		AvatarNumError,
		[ProtoEnum(Name = "ELF_LOCK")]
		ElfLock,
		[ProtoEnum(Name = "ELF_IN_QUEST")]
		ElfInQuest,
		[ProtoEnum(Name = "SKILL_LACK")]
		SkillLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
