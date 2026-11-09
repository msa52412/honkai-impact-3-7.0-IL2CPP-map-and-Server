using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RecvCommonChatworldMsgNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2405
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chat_msg_list")]
	public List<ChatworldMsg> ChatMsgLists { get; } = new List<ChatworldMsg>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
