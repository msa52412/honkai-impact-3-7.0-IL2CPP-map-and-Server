using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UniqueGachaSelectItemPool : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "select_item_list")]
	public uint[] SelectItemLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
