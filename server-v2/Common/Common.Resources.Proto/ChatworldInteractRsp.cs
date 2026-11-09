using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldInteractRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2414
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "NOT_IN_CHATWORLD")]
		NotInChatworld = 3,
		[ProtoEnum(Name = "HELD_BY_OTHER")]
		HeldByOther = 4,
		[ProtoEnum(Name = "NOT_HOLD")]
		NotHold = 5
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "interact_msg")]
	public ChatworldMsgInteract InteractMsg { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
