using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipmentItemList : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "item_list")]
	public List<EquipmentItem> ItemLists { get; } = new List<EquipmentItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
