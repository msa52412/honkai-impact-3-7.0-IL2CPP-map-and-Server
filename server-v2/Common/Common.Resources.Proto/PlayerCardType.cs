using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlayerCardType
{
	[ProtoEnum(Name = "CARD_ALL")]
	CardAll,
	[ProtoEnum(Name = "CARD_AVATAR")]
	CardAvatar,
	[ProtoEnum(Name = "CARD_MEDAL")]
	CardMedal,
	[ProtoEnum(Name = "CARD_MESSAGE")]
	CardMessage,
	[ProtoEnum(Name = "CARD_MSG_CONFIG")]
	CardMsgConfig,
	[ProtoEnum(Name = "CARD_ELF")]
	CardElf
}
