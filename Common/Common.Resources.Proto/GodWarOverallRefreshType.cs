using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarOverallRefreshType
{
	[ProtoEnum(Name = "GOD_WAR_OVERALL_REFRESH_NONE")]
	GodWarOverallRefreshNone,
	[ProtoEnum(Name = "GOD_WAR_OVERALL_REFRESH_DAILY")]
	GodWarOverallRefreshDaily,
	[ProtoEnum(Name = "GOD_WAR_OVERALL_REFRESH_WEEKLY")]
	GodWarOverallRefreshWeekly
}
