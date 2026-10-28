using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaSystemChatMsgType
{
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_GACHA")]
	ArmadaChatMsgGacha = 1,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_AVATAR_STAR_UP")]
	ArmadaChatMsgAvatarStarUp,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_AVATAR_UNLOCK")]
	ArmadaChatMsgAvatarUnlock,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_JOIN_ARMADA")]
	ArmadaChatMsgJoinArmada,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_MISSION_FINISH")]
	ArmadaChatMsgMissionFinish,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_LEVEL_UP")]
	ArmadaChatMsgLevelUp,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_CABIN_BUILD_FINISH")]
	ArmadaChatMsgCabinBuildFinish,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_CABIN_LEVEL_UP")]
	ArmadaChatMsgCabinLevelUp,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_POSITION")]
	ArmadaChatMsgPosition,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_KICK")]
	ArmadaChatMsgKick,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_KEEPUP")]
	ArmadaChatMsgKeepup,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_CANCEL_DISSOLVE")]
	ArmadaChatMsgCancelDissolve,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_CHANGE_NAME")]
	ArmadaChatMsgChangeName,
	[ProtoEnum(Name = "ARMADA_CHAT_MSG_TEXT_MAP")]
	ArmadaChatMsgTextMap
}
