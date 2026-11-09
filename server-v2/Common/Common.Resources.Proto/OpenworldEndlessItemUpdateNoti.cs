using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessItemUpdateNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3421
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "item_list")]
	public List<OpenworldEndlessItem> ItemLists { get; } = new List<OpenworldEndlessItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
