using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldPosResetNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2424
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "reset_pos")]
	public ChatworldPosition ResetPos { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
