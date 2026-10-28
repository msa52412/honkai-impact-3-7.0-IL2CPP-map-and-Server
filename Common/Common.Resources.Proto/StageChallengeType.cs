using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageChallengeType
{
	[ProtoEnum(Name = "CHALLENGE_LIMIT_AVATAR")]
	ChallengeLimitAvatar = 1,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_AVATAR_KILLED")]
	ChallengeLimitAvatarKilled,
	[ProtoEnum(Name = "CHALLENGE_MAX_COMBO")]
	ChallengeMaxCombo,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_BE_HIT")]
	ChallengeLimitBeHit,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_LEVEL_TIME")]
	ChallengeLimitLevelTime,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_WITCH_TIME_TRIGGERED")]
	ChallengeLimitWitchTimeTriggered,
	[ProtoEnum(Name = "CHALLENGE_MAX_DAMAGE_IN_WITCH_TIME")]
	ChallengeMaxDamageInWitchTime,
	[ProtoEnum(Name = "CHALLENGE_MAX_ULTIMATE_SKILL_TRIGGERED")]
	ChallengeMaxUltimateSkillTriggered,
	[ProtoEnum(Name = "CHALLENGE_MAX_DAMAGE_IN_CERTAIN_TIME")]
	ChallengeMaxDamageInCertainTime,
	[ProtoEnum(Name = "CHALLENGE_MAX_ULTIMATE_SKILL_TRIGGERED_BY_DISTINCT_AVATAR")]
	ChallengeMaxUltimateSkillTriggeredByDistinctAvatar,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_BE_HIT_DOWN")]
	ChallengeLimitBeHitDown,
	[ProtoEnum(Name = "CHALLENGE_MAX_AVATAR_CLASS_IN_TEAM")]
	ChallengeMaxAvatarClassInTeam,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_KILLED")]
	ChallengeMaxMonsterKilled,
	[ProtoEnum(Name = "CHALLENGE_MAX_AVATAR_NATURE_IN_TEAM")]
	ChallengeMaxAvatarNatureInTeam,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_TOTAL_DAMAGE")]
	ChallengeLimitTotalDamage,
	[ProtoEnum(Name = "CHALLENGE_HELPER_AVATAR_ALIVE")]
	ChallengeHelperAvatarAlive,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_TRAP_TRIGGERED")]
	ChallengeLimitTrapTriggered,
	[ProtoEnum(Name = "CHALLENGE_MAX_BOX_OPENED")]
	ChallengeMaxBoxOpened,
	[ProtoEnum(Name = "CHALLENGE_MAX_DEBUFF_ADD")]
	ChallengeMaxDebuffAdd,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_BE_ADDED_DEBUFF")]
	ChallengeLimitBeAddedDebuff,
	[ProtoEnum(Name = "CHALLENGE_MAX_ALL_DAMAGE_IND_WITCH_TIME")]
	ChallengeMaxAllDamageIndWitchTime,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_KILLED_BY_SWITCH_IN_ATTACK")]
	ChallengeMaxMonsterKilledBySwitchInAttack,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_KILLED_IN_WITCH_TIME")]
	ChallengeMaxMonsterKilledInWitchTime,
	[ProtoEnum(Name = "CHALLENGE_MONSTER_LAST_KILLED_BY_SWITCH_IN_ATTACK")]
	ChallengeMonsterLastKilledBySwitchInAttack,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_KILLED_IN_CERTAIN_TIME")]
	ChallengeMaxMonsterKilledInCertainTime,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_HIT_AIR")]
	ChallengeMaxMonsterHitAir,
	[ProtoEnum(Name = "CHALLENGE_FINISH_STAGE_NORMAL")]
	ChallengeFinishStageNormal,
	[ProtoEnum(Name = "CHALLENGE_FINISH_STAGE_FAST")]
	ChallengeFinishStageFast,
	[ProtoEnum(Name = "CHALLENGE_FINISH_STAGE_VERY_FAST")]
	ChallengeFinishStageVeryFast,
	[ProtoEnum(Name = "CHALLENGE_MONSTER_KILLED_BY_BRANCH")]
	ChallengeMonsterKilledByBranch,
	[ProtoEnum(Name = "CHALLENGE_QTE_TRIGGERED")]
	ChallengeQteTriggered,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_KILLED_BY_ASSASSIN")]
	ChallengeMaxMonsterKilledByAssassin,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_CONTINOUS_KILLED_BY_ASSASSIN")]
	ChallengeMaxMonsterContinousKilledByAssassin,
	[ProtoEnum(Name = "CHALLENGE_MAX_ELITE_MONSTER_KLLED")]
	ChallengeMaxEliteMonsterKlled,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_DISCOVERED")]
	ChallengeLimitDiscovered,
	[ProtoEnum(Name = "CHALLENGE_MAX_DEFEND_OBJECT_DAMAGE")]
	ChallengeMaxDefendObjectDamage,
	[ProtoEnum(Name = "CHALLENGE_MAX_TEAM_SKILL_TRIGGERED")]
	ChallengeMaxTeamSkillTriggered,
	[ProtoEnum(Name = "CHALLENGE_ALL_STORY_MISSION_COMPLETE")]
	ChallengeAllStoryMissionComplete,
	[ProtoEnum(Name = "CHALLENGE_MAX_SCORE")]
	ChallengeMaxScore,
	[ProtoEnum(Name = "CHALLENGE_MAX_TEAM_SKILL_TRIGGERED_FOR_ALL_TEAM_MEMBER")]
	ChallengeMaxTeamSkillTriggeredForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_KILLED_IN_WITCH_TIME_FOR_ALL_TEAM_MEMBER")]
	ChallengeMaxMonsterKilledInWitchTimeForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_MAX_MONSTER_KILLED_FOR_ALL_TEAM_MEMBER")]
	ChallengeMaxMonsterKilledForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_MONSTER_KILLED_BY_BRANCH_FOR_ALL_TEAM_MEMBER")]
	ChallengeMonsterKilledByBranchForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_MAX_QTE_TRIGGERED_FOR_ALL_TEAM_MEMBER")]
	ChallengeMaxQteTriggeredForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_EVADE_SUCCESS_TRIGGERED_FOR_ALL_TEAM_MEMBER")]
	ChallengeLimitEvadeSuccessTriggeredForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_MAX_ULTIMATE_SKILL_TRIGGERED_FOR_ALL_TEAM_MEMBER")]
	ChallengeMaxUltimateSkillTriggeredForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_AVATAR_KILLED_FOR_ALL_TEAM_MEMBER")]
	ChallengeLimitAvatarKilledForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_MONSTER_KILLED_BY_QTE_FOR_ALL_TEAM_MEMBER")]
	ChallengeMonsterKilledByQteForAllTeamMember,
	[ProtoEnum(Name = "CHALLENGE_CLIENT_CUSTOM")]
	ChallengeClientCustom,
	[ProtoEnum(Name = "CHALLENGE_CUSTOM_BUTTON")]
	ChallengeCustomButton,
	[ProtoEnum(Name = "CHALLENGE_LIMIT_LEVEL_TIME2")]
	ChallengeLimitLevelTime2,
	[ProtoEnum(Name = "CHALLENGE_MONSTERCARD_LIMIT_AVATARSIDE_MONSTER_KILLED")]
	ChallengeMonstercardLimitAvatarsideMonsterKilled
}
