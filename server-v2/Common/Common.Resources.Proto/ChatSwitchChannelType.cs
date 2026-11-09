using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatSwitchChannelType
{
	[ProtoEnum(Name = "CHAT_SWITCH_CHANNEL_TYPE_NONE")]
	ChatSwitchChannelTypeNone,
	[ProtoEnum(Name = "CHAT_SWITCH_CHANNEL_TYPE_WORLD")]
	ChatSwitchChannelTypeWorld,
	[ProtoEnum(Name = "CHAT_SWITCH_CHANNEL_TYPE_PRIVATE")]
	ChatSwitchChannelTypePrivate,
	[ProtoEnum(Name = "CHAT_SWITCH_CHANNEL_TYPE_ARMADA")]
	ChatSwitchChannelTypeArmada,
	[ProtoEnum(Name = "CHAT_SWITCH_CHANNEL_TYPE_TEAM")]
	ChatSwitchChannelTypeTeam,
	[ProtoEnum(Name = "CHAT_SWITCH_CHANNEL_TYPE_GROUP")]
	ChatSwitchChannelTypeGroup,
	[ProtoEnum(Name = "CHAT_SWITCH_CHANNEL_TYPE_COMMON_CHATWORLD")]
	ChatSwitchChannelTypeCommonChatworld,
	[ProtoEnum(Name = "CHAT_SWITCH_CHANNEL_TYPE_OTHERS")]
	ChatSwitchChannelTypeOthers
}
