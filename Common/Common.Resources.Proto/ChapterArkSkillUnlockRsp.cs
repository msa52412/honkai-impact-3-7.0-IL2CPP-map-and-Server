using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterArkSkillUnlockRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7955
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SKILL_ERROR")]
		SkillError,
		[ProtoEnum(Name = "NOT_MEET_CONDITION")]
		NotMeetCondition,
		[ProtoEnum(Name = "RELATED_AVATAR_LOCKED")]
		RelatedAvatarLocked
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	[ProtoMember(3, Name = "skill_id")]
	public uint SkillId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
