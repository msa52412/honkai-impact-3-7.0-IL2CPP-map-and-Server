using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RecvChatMsgNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2235
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chat_msg_list")]
	public List<ChatMsg> ChatMsgLists { get; } = new List<ChatMsg>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
