using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralActivityStageAvatarBonusType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_STAGE_AVATAR_BONUS_NONE")]
	GeneralActivityStageAvatarBonusNone,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_STAGE_AVATAR_BONUS_ONCE")]
	GeneralActivityStageAvatarBonusOnce,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_STAGE_AVATAR_BONUS_ALL")]
	GeneralActivityStageAvatarBonusAll
}
