using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipmentSellReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 33
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "sell_item_list")]
	public EquipmentItemList SellItemList { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
