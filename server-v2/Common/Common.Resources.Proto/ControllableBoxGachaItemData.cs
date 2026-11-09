using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ControllableBoxGachaItemData : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "item_list")]
	public List<ControllableBoxGachaItem> ItemLists { get; } = new List<ControllableBoxGachaItem>();


	[ProtoMember(2, Name = "has_got_item_list")]
	public uint[] HasGotItemLists { get; set; }

	[ProtoMember(3, Name = "selecetable_group_list")]
	public List<ControllableBoxGachaSelectableGroup> SelecetableGroupLists { get; } = new List<ControllableBoxGachaSelectableGroup>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
