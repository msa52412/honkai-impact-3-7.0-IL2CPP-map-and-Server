using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatReportReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1150
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chat_report")]
	public ChatReport ChatReport { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
