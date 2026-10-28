using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatSwitchChannelType;

	[ProtoMember(1, Name = "chat_switch_channel_type")]
	public uint ChatSwitchChannelType
	{
		get
		{
			return __pbn__ChatSwitchChannelType.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatSwitchChannelType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatSwitchChannelType()
	{
		return __pbn__ChatSwitchChannelType.HasValue;
	}

	public void ResetChatSwitchChannelType()
	{
		__pbn__ChatSwitchChannelType = null;
	}
}
