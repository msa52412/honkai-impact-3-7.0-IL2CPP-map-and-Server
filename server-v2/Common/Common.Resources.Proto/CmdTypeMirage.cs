using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeMirage
{
	[ProtoEnum(Name = "CMD_MIRAGE_GET_ACTIVITY_REQ")]
	CmdMirageGetActivityReq = 7000,
	[ProtoEnum(Name = "CMD_MIRAGE_GET_ACTIVITY_RSP")]
	CmdMirageGetActivityRsp,
	[ProtoEnum(Name = "CMD_MIRAGE_TALENT_LEVEL_UP_REQ")]
	CmdMirageTalentLevelUpReq,
	[ProtoEnum(Name = "CMD_MIRAGE_TALENT_LEVEL_UP_RSP")]
	CmdMirageTalentLevelUpRsp,
	[ProtoEnum(Name = "CMD_MIRAGE_REPORT_STAGE_BEGIN_REQ")]
	CmdMirageReportStageBeginReq,
	[ProtoEnum(Name = "CMD_MIRAGE_REPORT_STAGE_BEGIN_RSP")]
	CmdMirageReportStageBeginRsp,
	[ProtoEnum(Name = "CMD_MIRAGE_RESET_DAILY_DROP_LIMIT_REQ")]
	CmdMirageResetDailyDropLimitReq,
	[ProtoEnum(Name = "CMD_MIRAGE_RESET_DAILY_DROP_LIMIT_RSP")]
	CmdMirageResetDailyDropLimitRsp
}
