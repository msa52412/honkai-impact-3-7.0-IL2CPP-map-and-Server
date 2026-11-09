using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeUltraendless
{
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_GET_TOP_RANK_REQ")]
	CmdUltraEndlessGetTopRankReq = 5200,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_GET_TOP_RANK_RSP")]
	CmdUltraEndlessGetTopRankRsp = 5201,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_GET_MAIN_DATA_REQ")]
	CmdUltraEndlessGetMainDataReq = 5202,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_GET_MAIN_DATA_RSP")]
	CmdUltraEndlessGetMainDataRsp = 5203,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_LAST_SETTLE_REWARD_NOTIFY")]
	CmdUltraEndlessLastSettleRewardNotify = 5205,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_REPORT_SITE_FLOOR_REQ")]
	CmdUltraEndlessReportSiteFloorReq = 5206,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_REPORT_SITE_FLOOR_RSP")]
	CmdUltraEndlessReportSiteFloorRsp = 5207,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_BRIEF_DATA_NOTIFY")]
	CmdUltraEndlessBriefDataNotify = 5210,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_ENTER_SITE_REQ")]
	CmdUltraEndlessEnterSiteReq = 5211,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_ENTER_SITE_RSP")]
	CmdUltraEndlessEnterSiteRsp = 5212,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_GROUP_LEVEL_REWARD_REQ")]
	CmdUltraEndlessGroupLevelRewardReq = 5213,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_GROUP_LEVEL_REWARD_RSP")]
	CmdUltraEndlessGroupLevelRewardRsp = 5214,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_TOP_RANK_REWARD_REQ")]
	CmdUltraEndlessTopRankRewardReq = 5215,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_TOP_RANK_REWARD_RSP")]
	CmdUltraEndlessTopRankRewardRsp = 5216,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_CLIENT_REPORT_NOTIFY")]
	CmdUltraEndlessClientReportNotify = 5219,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_FIRST_JOIN_REWARD_NOTIFY")]
	CmdUltraEndlessFirstJoinRewardNotify = 5220,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_COMMON_NOTIFY")]
	CmdUltraEndlessCommonNotify = 5298,
	[ProtoEnum(Name = "CMD_ULTRA_ENDLESS_DUMMY")]
	CmdUltraEndlessDummy = 5299
}
