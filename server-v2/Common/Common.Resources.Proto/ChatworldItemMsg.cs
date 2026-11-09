using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldItemMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "update_item_list")]
	public List<ChatworldItem> UpdateItemLists { get; } = new List<ChatworldItem>();


	[ProtoMember(2, Name = "delete_item_list")]
	public uint[] DeleteItemLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
