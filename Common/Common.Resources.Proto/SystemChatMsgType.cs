using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SystemChatMsgType
{
	[ProtoEnum(Name = "CHAT_MSG_NONE")]
	ChatMsgNone,
	[ProtoEnum(Name = "CHAT_MSG_GACHA")]
	ChatMsgGacha,
	[ProtoEnum(Name = "CHAT_MSG_LOBBY")]
	ChatMsgLobby,
	[ProtoEnum(Name = "CHAT_MSG_FORGE")]
	ChatMsgForge,
	[ProtoEnum(Name = "CHAT_MSG_SCRATCH")]
	ChatMsgScratch
}
