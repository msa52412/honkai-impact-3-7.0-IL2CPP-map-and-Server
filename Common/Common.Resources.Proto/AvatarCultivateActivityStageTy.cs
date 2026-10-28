using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarCultivateActivityStageType
{
	[ProtoEnum(Name = "AVATAR_CULTIVATE_ACTIVITY_STAGE_TYPE_NONE")]
	AvatarCultivateActivityStageTypeNone,
	[ProtoEnum(Name = "AVATAR_CULTIVATE_ACTIVITY_STAGE_TYPE_SWEEP")]
	AvatarCultivateActivityStageTypeSweep,
	[ProtoEnum(Name = "AVATAR_CULTIVATE_ACTIVITY_STAGE_TYPE_CHALLENGE")]
	AvatarCultivateActivityStageTypeChallenge,
	[ProtoEnum(Name = "AVATAR_CULTIVATE_ACTIVITY_STAGE_TYPE_NORMAL")]
	AvatarCultivateActivityStageTypeNormal
}
