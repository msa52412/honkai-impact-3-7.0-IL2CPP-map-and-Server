using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleFlagType
{
	[ProtoEnum(Name = "RPG_TALE_FLAG_INVALID")]
	RpgTaleFlagInvalid = 0,
	[ProtoEnum(Name = "RPG_TALE_FLAG_MATERIAL")]
	RpgTaleFlagMaterial = 1,
	[ProtoEnum(Name = "RPG_TALE_FLAG_MISSION")]
	RpgTaleFlagMission = 2,
	[ProtoEnum(Name = "RPG_TALE_FLAG_DIALOG")]
	RpgTaleFlagDialog = 3,
	[ProtoEnum(Name = "RPG_TALE_FLAG_ROLE")]
	RpgTaleFlagRole = 4,
	[ProtoEnum(Name = "RPG_TALE_FLAG_ROLE_STAR_NUM")]
	RpgTaleFlagRoleStarNum = 5,
	[ProtoEnum(Name = "RPG_TALE_FLAG_BEGIN_TIME")]
	RpgTaleFlagBeginTime = 6,
	[ProtoEnum(Name = "RPG_TALE_FLAG_END_TIME")]
	RpgTaleFlagEndTime = 7,
	[ProtoEnum(Name = "RPG_TALE_FLAG_PRE_SITE")]
	RpgTaleFlagPreSite = 8,
	[ProtoEnum(Name = "RPG_TALE_FLAG_MATERIAL_NUM")]
	RpgTaleFlagMaterialNum = 9,
	[ProtoEnum(Name = "RPG_TALE_FLAG_AVATAR")]
	RpgTaleFlagAvatar = 10,
	[ProtoEnum(Name = "RPG_TALE_FLAG_STAGE_CHALLENGE_NUM")]
	RpgTaleFlagStageChallengeNum = 11,
	[ProtoEnum(Name = "RPG_TALE_FLAG_PLAYER_LEVEL")]
	RpgTaleFlagPlayerLevel = 12,
	[ProtoEnum(Name = "RPG_TALE_FLAG_OVERALL")]
	RpgTaleFlagOverall = 13,
	[ProtoEnum(Name = "RPG_TALE_FLAG_TOTAL_STAGE_SCORE")]
	RpgTaleFlagTotalStageScore = 15,
	[ProtoEnum(Name = "RPG_TALE_FLAG_STAGE_SCORE")]
	RpgTaleFlagStageScore = 16,
	[ProtoEnum(Name = "RPG_TALE_FLAG_ABILITY_LEVEL")]
	RpgTaleFlagAbilityLevel = 17,
	[ProtoEnum(Name = "RPG_TALE_FLAG_ABILITY_TOTAL_LEVEL")]
	RpgTaleFlagAbilityTotalLevel = 18,
	[ProtoEnum(Name = "RPG_TALE_FLAG_SITE_ALLOW_ENTER")]
	RpgTaleFlagSiteAllowEnter = 19,
	[ProtoEnum(Name = "RPG_TALE_FLAG_SITE_STATUS")]
	RpgTaleFlagSiteStatus = 20,
	[ProtoEnum(Name = "RPG_TALE_FLAG_DROP_LIMIT")]
	RpgTaleFlagDropLimit = 21,
	[ProtoEnum(Name = "RPG_TALE_FLAG_IS_STAGE_PASSED")]
	RpgTaleFlagIsStagePassed = 22
}
