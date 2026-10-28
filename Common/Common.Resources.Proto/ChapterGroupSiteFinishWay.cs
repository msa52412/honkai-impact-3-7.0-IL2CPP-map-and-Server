using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChapterGroupSiteFinishWay
{
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_FINISH_WAY_INVALID")]
	ChapterGroupSiteFinishWayInvalid,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_FINISH_WAY_PASS_STAGE_AND")]
	ChapterGroupSiteFinishWayPassStageAnd,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_FINISH_WAY_PASS_STAGE_OR")]
	ChapterGroupSiteFinishWayPassStageOr,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_FINISH_WAY_FINISH_MISSION_AND")]
	ChapterGroupSiteFinishWayFinishMissionAnd,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_FINISH_WAY_FINISH_MISSION_OR")]
	ChapterGroupSiteFinishWayFinishMissionOr,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_FINISH_WAY_TIME_ARRIVED")]
	ChapterGroupSiteFinishWayTimeArrived,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_FINISH_WAY_FINISH_STORY_AND")]
	ChapterGroupSiteFinishWayFinishStoryAnd,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_FINISH_WAY_FINISH_STORY_OR")]
	ChapterGroupSiteFinishWayFinishStoryOr
}
