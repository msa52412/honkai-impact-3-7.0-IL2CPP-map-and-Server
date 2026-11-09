using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CommonChatMsgType
{
	[ProtoEnum(Name = "COMMON_CHAT_MSG_NONE")]
	CommonChatMsgNone,
	[ProtoEnum(Name = "COMMON_CHAT_MSG_TEAM")]
	CommonChatMsgTeam,
	[ProtoEnum(Name = "COMMON_CHAT_MSG_CAMPAIGN")]
	CommonChatMsgCampaign,
	[ProtoEnum(Name = "COMMON_CHAT_MSG_ROOM")]
	CommonChatMsgRoom,
	[ProtoEnum(Name = "COMMON_CHAT_MSG_PRIVATE")]
	CommonChatMsgPrivate,
	[ProtoEnum(Name = "COMMON_CHAT_MSG_CAHT_GROUP")]
	CommonChatMsgCahtGroup,
	[ProtoEnum(Name = "COMMON_CHAT_MSG_WORLD_CHAT")]
	CommonChatMsgWorldChat,
	[ProtoEnum(Name = "COMMON_CHAT_MSG_ARMADA_CHAT")]
	CommonChatMsgArmadaChat
}
