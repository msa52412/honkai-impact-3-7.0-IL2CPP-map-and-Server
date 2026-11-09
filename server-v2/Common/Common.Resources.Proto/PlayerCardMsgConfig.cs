using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlayerCardMsgConfig
{
	[ProtoEnum(Name = "CARD_MSG_ALL")]
	CardMsgAll,
	[ProtoEnum(Name = "CARD_MSG_FRIEND")]
	CardMsgFriend,
	[ProtoEnum(Name = "CARD_MSG_FORBIDDEN")]
	CardMsgForbidden
}
