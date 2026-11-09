using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterArkSkillUnlockReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7954
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	[ProtoMember(2, Name = "skill_id")]
	public uint SkillId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
