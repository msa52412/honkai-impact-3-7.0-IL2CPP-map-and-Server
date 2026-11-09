using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExtraChatParam : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "room_chat_param")]
	public RoomChatParam RoomChatParam { get; set; }

	[ProtoMember(2, Name = "private_chat_param")]
	public PrivateChatParam PrivateChatParam { get; set; }

	[ProtoMember(3, Name = "group_chat_param")]
	public GroupChatParam GroupChatParam { get; set; }

	[ProtoMember(4, Name = "battle_chat_param")]
	public BattleChatParam BattleChatParam { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
