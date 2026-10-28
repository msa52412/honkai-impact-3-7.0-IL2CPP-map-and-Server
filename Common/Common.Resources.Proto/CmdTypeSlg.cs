using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeSlg
{
	[ProtoEnum(Name = "CMD_SLG_GET_DATA_REQ")]
	CmdSlgGetDataReq = 6050,
	[ProtoEnum(Name = "CMD_SLG_GET_DATA_RSP")]
	CmdSlgGetDataRsp = 6051,
	[ProtoEnum(Name = "CMD_SLG_POINT_SWEEP_REQ")]
	CmdSlgPointSweepReq = 6052,
	[ProtoEnum(Name = "CMD_SLG_POINT_SWEEP_RSP")]
	CmdSlgPointSweepRsp = 6053,
	[ProtoEnum(Name = "CMD_SLG_BATTLE_MATCH_RSP")]
	CmdSlgBattleMatchRsp = 6054,
	[ProtoEnum(Name = "CMD_SLG_ENROLL_REQ")]
	CmdSlgEnrollReq = 6055,
	[ProtoEnum(Name = "CMD_SLG_ENROLL_RSP")]
	CmdSlgEnrollRsp = 6056,
	[ProtoEnum(Name = "CMD_SLG_GET_ACTIVITY_STAMINA_REQ")]
	CmdSlgGetActivityStaminaReq = 6057,
	[ProtoEnum(Name = "CMD_SLG_GET_ACTIVITY_STAMINA_RSP")]
	CmdSlgGetActivityStaminaRsp = 6058,
	[ProtoEnum(Name = "CMD_SLG_TAKE_SCORE_REWARD_REQ")]
	CmdSlgTakeScoreRewardReq = 6059,
	[ProtoEnum(Name = "CMD_SLG_TAKE_SCORE_REWARD_RSP")]
	CmdSlgTakeScoreRewardRsp = 6060,
	[ProtoEnum(Name = "CMD_SLG_BATTLE_ADD_SCORE_FAIL_NOTIFY")]
	CmdSlgBattleAddScoreFailNotify = 6061,
	[ProtoEnum(Name = "CMD_SLG_GET_BATTLE_RANK_REQ")]
	CmdSlgGetBattleRankReq = 6062,
	[ProtoEnum(Name = "CMD_SLG_GET_BATTLE_RANK_RSP")]
	CmdSlgGetBattleRankRsp = 6063,
	[ProtoEnum(Name = "CMD_SLG_GET_POINT_RANK_REQ")]
	CmdSlgGetPointRankReq = 6064,
	[ProtoEnum(Name = "CMD_SLG_GET_POINT_RANK_RSP")]
	CmdSlgGetPointRankRsp = 6065,
	[ProtoEnum(Name = "CMD_SLG_BATTLE_SETTLE_NOTIFY")]
	CmdSlgBattleSettleNotify = 6066,
	[ProtoEnum(Name = "CMD_SLG_BROADCAST_NOTIFY")]
	CmdSlgBroadcastNotify = 6068,
	[ProtoEnum(Name = "CMD_SLG_MAIN_PAGE_REQ")]
	CmdSlgMainPageReq = 6069,
	[ProtoEnum(Name = "CMD_SLG_BATTLE_IN_SETTLE_NOTIFY")]
	CmdSlgBattleInSettleNotify = 6070,
	[ProtoEnum(Name = "CMD_SLG_GET_BRIEF_RANK_REQ")]
	CmdSlgGetBriefRankReq = 6071,
	[ProtoEnum(Name = "CMD_SLG_GET_BRIEF_RANK_RSP")]
	CmdSlgGetBriefRankRsp = 6072,
	[ProtoEnum(Name = "CMD_SLG_DUMMY")]
	CmdSlgDummy = 6149
}
