using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatMsgInternal : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "key_value_list")]
	public List<ChatMsgKeyValue> KeyValueLists { get; } = new List<ChatMsgKeyValue>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
