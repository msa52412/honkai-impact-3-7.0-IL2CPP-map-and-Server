using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatMsgContent : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "items")]
	public List<ChatMsgItem> Items { get; } = new List<ChatMsgItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
