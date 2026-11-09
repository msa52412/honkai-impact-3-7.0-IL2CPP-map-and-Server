using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldInteractReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2413
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "interact_msg")]
	public ChatworldMsgInteract InteractMsg { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
