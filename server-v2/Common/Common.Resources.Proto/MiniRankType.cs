using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MiniRankType
{
	[ProtoEnum(Name = "MINI_RANK_RPG_STAGE")]
	MiniRankRpgStage = 1,
	[ProtoEnum(Name = "MINI_RANK_GENERAL_ACTIVITY_STAGE")]
	MiniRankGeneralActivityStage,
	[ProtoEnum(Name = "MINI_RANK_CHAPTER_ACTIVITY_STAGE")]
	MiniRankChapterActivityStage,
	[ProtoEnum(Name = "MINI_RANK_RPG_SCHEDULE")]
	MiniRankRpgSchedule,
	[ProtoEnum(Name = "MINI_RANK_RANCH_ACTIVITY_STAGE")]
	MiniRankRanchActivityStage,
	[ProtoEnum(Name = "MINI_RANK_OPENWORLD_CHAPTER_CHALLENGE")]
	MiniRankOpenworldChapterChallenge,
	[ProtoEnum(Name = "MINI_RANK_GLOBAL_EXPLORE_ENTITY_CONTRIBUTION")]
	MiniRankGlobalExploreEntityContribution,
	[ProtoEnum(Name = "MINI_RANK_GLOBAL_EXPLORE_TOTAL_CONTRIBUTION")]
	MiniRankGlobalExploreTotalContribution,
	[ProtoEnum(Name = "MINI_RANK_GLOBAL_EXPLORE_STAGE")]
	MiniRankGlobalExploreStage
}
