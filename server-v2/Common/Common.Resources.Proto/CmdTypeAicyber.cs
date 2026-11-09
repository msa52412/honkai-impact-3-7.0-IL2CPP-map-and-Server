using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeAicyber
{
	[ProtoEnum(Name = "CMD_AI_CYBER_GET_ACTIVITY_REQ")]
	CmdAiCyberGetActivityReq = 6650,
	[ProtoEnum(Name = "CMD_AI_CYBER_GET_ACTIVITY_RSP")]
	CmdAiCyberGetActivityRsp,
	[ProtoEnum(Name = "CMD_AI_CYBER_TAKE_PUZZLE_REWARD_REQ")]
	CmdAiCyberTakePuzzleRewardReq,
	[ProtoEnum(Name = "CMD_AI_CYBER_TAKE_PUZZLE_REWARD_RSP")]
	CmdAiCyberTakePuzzleRewardRsp,
	[ProtoEnum(Name = "CMD_AI_CYBER_ADD_REPAIR_PROGRESS_REQ")]
	CmdAiCyberAddRepairProgressReq,
	[ProtoEnum(Name = "CMD_AI_CYBER_ADD_REPAIR_PROGRESS_RSP")]
	CmdAiCyberAddRepairProgressRsp,
	[ProtoEnum(Name = "CMD_AI_CYBER_SET_BLESS_REQ")]
	CmdAiCyberSetBlessReq,
	[ProtoEnum(Name = "CMD_AI_CYBER_SET_BLESS_RSP")]
	CmdAiCyberSetBlessRsp,
	[ProtoEnum(Name = "CMD_AI_CYBER_REFRESH_TICKET_REQ")]
	CmdAiCyberRefreshTicketReq,
	[ProtoEnum(Name = "CMD_AI_CYBER_REFRESH_TICKET_RSP")]
	CmdAiCyberRefreshTicketRsp,
	[ProtoEnum(Name = "CMD_AI_CYBER_UPDATE_CLIENT_SETTING_REQ")]
	CmdAiCyberUpdateClientSettingReq,
	[ProtoEnum(Name = "CMD_AI_CYBER_UPDATE_CLIENT_SETTING_RSP")]
	CmdAiCyberUpdateClientSettingRsp
}
