using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldMsgMove : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "target_position")]
	public ChatworldPosition TargetPosition { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
