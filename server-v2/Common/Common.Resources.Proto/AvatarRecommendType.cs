using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarRecommendType
{
	[ProtoEnum(Name = "AVATAR_RECOMMEND_NONE")]
	AvatarRecommendNone,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_STAGE")]
	AvatarRecommendStage,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_EXBOSS")]
	AvatarRecommendExboss,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_WEEKLY_REPORT_ENDLESS")]
	AvatarRecommendWeeklyReportEndless,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_WEEKLY_REPORT_EX_BOSS")]
	AvatarRecommendWeeklyReportExBoss,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_WEEKLY_REPORT_ULTRA_ENDLESS")]
	AvatarRecommendWeeklyReportUltraEndless,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_ULTRA_ENDLESS")]
	AvatarRecommendUltraEndless,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_GREEDY")]
	AvatarRecommendGreedy,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_WEEKLY_REPORT_GREEDY")]
	AvatarRecommendWeeklyReportGreedy
}
