using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldEntityMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "update_entity_list")]
	public List<ChatworldEntity> UpdateEntityLists { get; } = new List<ChatworldEntity>();


	[ProtoMember(2, Name = "delete_entity_list")]
	public uint[] DeleteEntityLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
