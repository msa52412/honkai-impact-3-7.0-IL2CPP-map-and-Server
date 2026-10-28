using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AchievementType
{
	[ProtoEnum(Name = "ACHIEVE_TYPE_NONE")]
	AchieveTypeNone,
	[ProtoEnum(Name = "ACHIEVE_TYPE_CAPTAIN")]
	AchieveTypeCaptain,
	[ProtoEnum(Name = "ACHIEVE_TYPE_DEVELOP")]
	AchieveTypeDevelop,
	[ProtoEnum(Name = "ACHIEVE_TYPE_SOCIAL")]
	AchieveTypeSocial,
	[ProtoEnum(Name = "ACHIEVE_TYPE_STAGE")]
	AchieveTypeStage,
	[ProtoEnum(Name = "ACHIEVE_TYPE_ISLAND")]
	AchieveTypeIsland,
	[ProtoEnum(Name = "ACHIEVE_TYPE_CHALLENGE")]
	AchieveTypeChallenge,
	[ProtoEnum(Name = "ACHIEVE_TYPE_DORM")]
	AchieveTypeDorm,
	[ProtoEnum(Name = "ACHIEVE_TYPE_EXTRA_STORY")]
	AchieveTypeExtraStory
}
