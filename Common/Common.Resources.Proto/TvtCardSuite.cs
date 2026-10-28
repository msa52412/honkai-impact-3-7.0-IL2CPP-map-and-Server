using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtCardSuite : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "card_id_list")]
	public uint[] CardIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
