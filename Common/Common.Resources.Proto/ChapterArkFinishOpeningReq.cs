using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterArkFinishOpeningReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7962
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
