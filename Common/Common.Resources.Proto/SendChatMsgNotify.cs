using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SendChatMsgNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2234
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chat_msg")]
	public ChatMsg ChatMsg { get; set; }

	[ProtoMember(2, Name = "target_uid_list")]
	public uint[] TargetUidLists { get; set; }

	[ProtoMember(3, Name = "extra_chat_param")]
	public ExtraChatParam ExtraChatParam { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
