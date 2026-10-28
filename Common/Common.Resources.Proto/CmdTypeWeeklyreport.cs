using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeWeeklyreport
{
	[ProtoEnum(Name = "CMD_GET_WEEKLY_REPORT_REQ")]
	CmdGetWeeklyReportReq = 5100,
	[ProtoEnum(Name = "CMD_GET_WEEKLY_REPORT_RSP")]
	CmdGetWeeklyReportRsp,
	[ProtoEnum(Name = "CMD_GET_WEEKLY_REPORT_ENDLESS_RECOMMEND_PLAYER_REQ")]
	CmdGetWeeklyReportEndlessRecommendPlayerReq,
	[ProtoEnum(Name = "CMD_GET_WEEKLY_REPORT_ENDLESS_RECOMMEND_PLAYER_RSP")]
	CmdGetWeeklyReportEndlessRecommendPlayerRsp,
	[ProtoEnum(Name = "CMD_GET_WEEKLY_REPORT_EX_BOSS_RECOMMEND_PLAYER_REQ")]
	CmdGetWeeklyReportExBossRecommendPlayerReq,
	[ProtoEnum(Name = "CMD_GET_WEEKLY_REPORT_EX_BOSS_RECOMMEND_PLAYER_RSP")]
	CmdGetWeeklyReportExBossRecommendPlayerRsp,
	[ProtoEnum(Name = "CMD_GET_WEEKLY_REPORT_ULTRA_ENDLESS_RECOMMEND_PLAYER_REQ")]
	CmdGetWeeklyReportUltraEndlessRecommendPlayerReq,
	[ProtoEnum(Name = "CMD_GET_WEEKLY_REPORT_ULTRA_ENDLESS_RECOMMEND_PLAYER_RSP")]
	CmdGetWeeklyReportUltraEndlessRecommendPlayerRsp
}
