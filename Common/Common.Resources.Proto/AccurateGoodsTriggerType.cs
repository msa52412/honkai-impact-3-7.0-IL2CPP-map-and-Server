using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AccurateGoodsTriggerType
{
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_NONE")]
	AccurateGoodsTriggerTypeNone,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_UNLOCK_AVATAR")]
	AccurateGoodsTriggerTypeUnlockAvatar,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_FIRST_GOT")]
	AccurateGoodsTriggerTypeFirstGot,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_REPEAT_GOT")]
	AccurateGoodsTriggerTypeRepeatGot,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_UNLOCK_ELF")]
	AccurateGoodsTriggerTypeUnlockElf,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_FINISH_MISSION")]
	AccurateGoodsTriggerTypeFinishMission,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_EXCHANGE_SCOIN")]
	AccurateGoodsTriggerTypeExchangeScoin,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_EXCHANGE_STAMINA")]
	AccurateGoodsTriggerTypeExchangeStamina,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_LOSE_STAGE")]
	AccurateGoodsTriggerTypeLoseStage,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_WIN_STAGE_FIRST")]
	AccurateGoodsTriggerTypeWinStageFirst,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_AVATAR_LEVEL")]
	AccurateGoodsTriggerTypeAvatarLevel,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_PLAYER_LEVEL")]
	AccurateGoodsTriggerTypePlayerLevel,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_GOBACK_DAYS")]
	AccurateGoodsTriggerTypeGobackDays,
	[ProtoEnum(Name = "ACCURATE_GOODS_TRIGGER_TYPE_AVATAR_STAR")]
	AccurateGoodsTriggerTypeAvatarStar
}
