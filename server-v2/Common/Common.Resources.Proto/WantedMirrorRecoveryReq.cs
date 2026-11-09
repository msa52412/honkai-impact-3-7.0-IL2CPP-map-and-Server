using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WantedMirrorRecoveryReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1517
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "consume_item_list")]
	public EquipmentItemList ConsumeItemList { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
