using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeActivity3
{
	[ProtoEnum(Name = "CMD_SUSANNA_TRIAL_GET_ACTIVITY_REQ")]
	CmdSusannaTrialGetActivityReq = 7050,
	[ProtoEnum(Name = "CMD_SUSANNA_TRIAL_GET_ACTIVITY_RSP")]
	CmdSusannaTrialGetActivityRsp = 7051,
	[ProtoEnum(Name = "CMD_SUSANNA_TRIAL_TAKE_MENTOR_PROGRESS_REWARD_REQ")]
	CmdSusannaTrialTakeMentorProgressRewardReq = 7052,
	[ProtoEnum(Name = "CMD_SUSANNA_TRIAL_TAKE_MENTOR_PROGRESS_REWARD_RSP")]
	CmdSusannaTrialTakeMentorProgressRewardRsp = 7053,
	[ProtoEnum(Name = "CMD_SUSANNA_TRIAL_TAKE_PLOT_REWARD_REQ")]
	CmdSusannaTrialTakePlotRewardReq = 7054,
	[ProtoEnum(Name = "CMD_SUSANNA_TRIAL_TAKE_PLOT_REWARD_RSP")]
	CmdSusannaTrialTakePlotRewardRsp = 7055,
	[ProtoEnum(Name = "CMD_SHIGURE_KIRA_GET_ACTIVITY_REQ")]
	CmdShigureKiraGetActivityReq = 7060,
	[ProtoEnum(Name = "CMD_SHIGURE_KIRA_GET_ACTIVITY_RSP")]
	CmdShigureKiraGetActivityRsp = 7061,
	[ProtoEnum(Name = "CMD_SHIGURE_KIRA_ADD_CONCERT_PROGRESS_REQ")]
	CmdShigureKiraAddConcertProgressReq = 7062,
	[ProtoEnum(Name = "CMD_SHIGURE_KIRA_ADD_CONCERT_PROGRESS_RSP")]
	CmdShigureKiraAddConcertProgressRsp = 7063
}
