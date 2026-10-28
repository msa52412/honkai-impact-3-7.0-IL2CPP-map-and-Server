using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldStanceMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stance_list")]
	public List<ChatworldStance> StanceLists { get; } = new List<ChatworldStance>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
