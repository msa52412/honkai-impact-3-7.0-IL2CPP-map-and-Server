using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NewbieActivityServerMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "newbie_login_post_data_list")]
	public List<NewbieLoginPostEntry> NewbieLoginPostDataLists { get; } = new List<NewbieLoginPostEntry>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
