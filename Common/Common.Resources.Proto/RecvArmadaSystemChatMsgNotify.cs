using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RecvArmadaSystemChatMsgNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2662
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chat_msg")]
	public ArmadaSystemChatMsg ChatMsg { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
