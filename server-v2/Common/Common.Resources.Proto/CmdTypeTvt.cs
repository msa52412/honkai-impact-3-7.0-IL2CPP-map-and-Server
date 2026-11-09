using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeTvt
{
	[ProtoEnum(Name = "CMD_GET_TVT_ACTIVITY_REQ")]
	CmdGetTvtActivityReq = 3300,
	[ProtoEnum(Name = "CMD_GET_TVT_ACTIVITY_RSP")]
	CmdGetTvtActivityRsp = 3301,
	[ProtoEnum(Name = "CMD_GET_TVT_BATTLE_HISTORY_REQ")]
	CmdGetTvtBattleHistoryReq = 3302,
	[ProtoEnum(Name = "CMD_GET_TVT_BATTLE_HISTORY_RSP")]
	CmdGetTvtBattleHistoryRsp = 3303,
	[ProtoEnum(Name = "CMD_SYNC_TVT_MEMBER_IN_ROOM_NOTIFY")]
	CmdSyncTvtMemberInRoomNotify = 3304,
	[ProtoEnum(Name = "CMD_TVT_TAKE_MISSION_GROUP_REWARD_REQ")]
	CmdTvtTakeMissionGroupRewardReq = 3305,
	[ProtoEnum(Name = "CMD_TVT_TAKE_MISSION_GROUP_REWARD_RSP")]
	CmdTvtTakeMissionGroupRewardRsp = 3306,
	[ProtoEnum(Name = "CMD_TVT_GET_STAGE_SCHEDULE_REQ")]
	CmdTvtGetStageScheduleReq = 3307,
	[ProtoEnum(Name = "CMD_TVT_GET_STAGE_SCHEDULE_RSP")]
	CmdTvtGetStageScheduleRsp = 3308,
	[ProtoEnum(Name = "CMD_TVT_CARD_GET_DATA_REQ")]
	CmdTvtCardGetDataReq = 3309,
	[ProtoEnum(Name = "CMD_TVT_CARD_GET_DATA_RSP")]
	CmdTvtCardGetDataRsp = 3310,
	[ProtoEnum(Name = "CMD_TVT_CARD_LEVELUP_REQ")]
	CmdTvtCardLevelupReq = 3311,
	[ProtoEnum(Name = "CMD_TVT_CARD_LEVELUP_RSP")]
	CmdTvtCardLevelupRsp = 3312,
	[ProtoEnum(Name = "CMD_TVT_CARD_UPDATE_SUITE_REQ")]
	CmdTvtCardUpdateSuiteReq = 3313,
	[ProtoEnum(Name = "CMD_TVT_CARD_UPDATE_SUITE_RSP")]
	CmdTvtCardUpdateSuiteRsp = 3314,
	[ProtoEnum(Name = "CMD_TVT_CARD_TAKE_SEASON_RETURN_REQ")]
	CmdTvtCardTakeSeasonReturnReq = 3315,
	[ProtoEnum(Name = "CMD_TVT_CARD_TAKE_SEASON_RETURN_RSP")]
	CmdTvtCardTakeSeasonReturnRsp = 3316,
	[ProtoEnum(Name = "CMD_TVT_CARD_TRANSFORM_MATERIAL_REQ")]
	CmdTvtCardTransformMaterialReq = 3317,
	[ProtoEnum(Name = "CMD_TVT_CARD_TRANSFORM_MATERIAL_RSP")]
	CmdTvtCardTransformMaterialRsp = 3318,
	[ProtoEnum(Name = "CMD_SYNC_TVT_BATTLE_INFO_NOTIFY")]
	CmdSyncTvtBattleInfoNotify = 3350,
	[ProtoEnum(Name = "CMD_TVT_BATTLE_MEMBER_PREPARE_NOTIFY")]
	CmdTvtBattleMemberPrepareNotify = 3351,
	[ProtoEnum(Name = "CMD_TVT_BATTLE_BEGIN_NOTIFY")]
	CmdTvtBattleBeginNotify = 3352,
	[ProtoEnum(Name = "CMD_TVT_BATTLE_END_NOTIFY")]
	CmdTvtBattleEndNotify = 3353,
	[ProtoEnum(Name = "CMD_TVT_BATTLE_SETTLE_NOTIFY")]
	CmdTvtBattleSettleNotify = 3354,
	[ProtoEnum(Name = "CMD_REPORT_TVT_BATTLE_STAGE_MESSAGE_NOTIFY")]
	CmdReportTvtBattleStageMessageNotify = 3355,
	[ProtoEnum(Name = "CMD_SYNC_TVT_BATTLE_STAGE_MESSAGE_NOTIFY")]
	CmdSyncTvtBattleStageMessageNotify = 3356,
	[ProtoEnum(Name = "CMD_EXIT_TVT_BATTLE_NOTIFY")]
	CmdExitTvtBattleNotify = 3357,
	[ProtoEnum(Name = "CMD_TVT_BATTLE_CLIENT_REPORT_NOTIFY")]
	CmdTvtBattleClientReportNotify = 3358,
	[ProtoEnum(Name = "CMD_TVT_ENTER_SHOP_REQ")]
	CmdTvtEnterShopReq = 3359,
	[ProtoEnum(Name = "CMD_TVT_ENTER_SHOP_RSP")]
	CmdTvtEnterShopRsp = 3360,
	[ProtoEnum(Name = "CMD_TVT_BATTLE_CONFIRM_NOTIFY")]
	CmdTvtBattleConfirmNotify = 3361,
	[ProtoEnum(Name = "CMD_TVT_UPDATE_LINEUP_INFO_NOTIFY")]
	CmdTvtUpdateLineupInfoNotify = 3362,
	[ProtoEnum(Name = "CMD_TVT_BATTLE_ROBOT_SETTLE_NOTIFY")]
	CmdTvtBattleRobotSettleNotify = 3363,
	[ProtoEnum(Name = "CMD_TVT_DUMMY")]
	CmdTvtDummy = 3399
}
