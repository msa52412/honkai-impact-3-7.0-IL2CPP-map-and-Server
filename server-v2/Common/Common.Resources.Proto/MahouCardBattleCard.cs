using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardBattleCard : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "slot")]
	public uint Slot { get; set; }

	[ProtoMember(2, Name = "card_id")]
	public uint CardId { get; set; }

	[ProtoMember(3, Name = "unique_id")]
	public uint UniqueId { get; set; }

	[ProtoMember(4, Name = "tag_list", IsPacked = true)]
	public uint[] TagLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
