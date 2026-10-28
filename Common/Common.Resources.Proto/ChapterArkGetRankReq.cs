using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterArkGetRankReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7960
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	[ProtoMember(2, Name = "stage_id")]
	public uint StageId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
