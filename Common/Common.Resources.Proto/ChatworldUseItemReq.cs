using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldUseItemReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2407
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "use_item_msg")]
	public ChatworldMsgUseItem UseItemMsg { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
