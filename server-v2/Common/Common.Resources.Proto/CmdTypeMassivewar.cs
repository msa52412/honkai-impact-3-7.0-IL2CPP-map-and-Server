using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeMassivewar
{
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_GET_ACTIVITY_REQ")]
	CmdMassiveWarGetActivityReq = 4800,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_GET_ACTIVITY_RSP")]
	CmdMassiveWarGetActivityRsp = 4801,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_GET_BATTLE_INFO_REQ")]
	CmdMassiveWarGetBattleInfoReq = 4802,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_GET_BATTLE_INFO_RSP")]
	CmdMassiveWarGetBattleInfoRsp = 4803,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_GET_RANK_REQ")]
	CmdMassiveWarGetRankReq = 4804,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_GET_RANK_RSP")]
	CmdMassiveWarGetRankRsp = 4805,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_BATTLE_BEGIN_NOTIFY")]
	CmdMassiveWarBattleBeginNotify = 4806,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_REPORT_BATTLE_MESSAGE_NOTIFY")]
	CmdMassiveWarReportBattleMessageNotify = 4807,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_EXIT_BATTLE_REQ")]
	CmdMassiveWarExitBattleReq = 4808,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_EXIT_BATTLE_RSP")]
	CmdMassiveWarExitBattleRsp = 4809,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_BATTLE_END_NOTIFY")]
	CmdMassiveWarBattleEndNotify = 4810,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_LINEUP_INFO_NOTIFY")]
	CmdMassiveWarLineupInfoNotify = 4811,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_TAKE_SCORE_REWARD_REQ")]
	CmdMassiveWarTakeScoreRewardReq = 4812,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_TAKE_SCORE_REWARD_RSP")]
	CmdMassiveWarTakeScoreRewardRsp = 4813,
	[ProtoEnum(Name = "CMD_MASSIVE_WAR_DUMMY")]
	CmdMassiveWarDummy = 4899
}
