using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleOverallRefreshType
{
	[ProtoEnum(Name = "RPG_TALE_OVERALL_REFRESH_NONE")]
	RpgTaleOverallRefreshNone,
	[ProtoEnum(Name = "RPG_TALE_OVERALL_REFRESH_DAILY")]
	RpgTaleOverallRefreshDaily,
	[ProtoEnum(Name = "RPG_TALE_OVERALL_REFRESH_WEEKLY")]
	RpgTaleOverallRefreshWeekly
}
