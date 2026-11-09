using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeChapterbwworld
{
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_GET_DATA_REQ")]
	CmdChapterBwWorldGetDataReq = 7650,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_GET_DATA_RSP")]
	CmdChapterBwWorldGetDataRsp = 7651,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_UPDATE_EQUIP_RUNE_REQ")]
	CmdChapterBwWorldUpdateEquipRuneReq = 7652,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_UPDATE_EQUIP_RUNE_RSP")]
	CmdChapterBwWorldUpdateEquipRuneRsp = 7653,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_RUNE_LEVEL_UP_REQ")]
	CmdChapterBwWorldRuneLevelUpReq = 7654,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_RUNE_LEVEL_UP_RSP")]
	CmdChapterBwWorldRuneLevelUpRsp = 7655,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_RUNE_SYNTHESIS_REQ")]
	CmdChapterBwWorldRuneSynthesisReq = 7656,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_RUNE_SYNTHESIS_RSP")]
	CmdChapterBwWorldRuneSynthesisRsp = 7657,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_GET_RANK_REQ")]
	CmdChapterBwWorldGetRankReq = 7658,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_GET_RANK_RSP")]
	CmdChapterBwWorldGetRankRsp = 7659,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_TAKE_REWARD_LINE_REWARD_REQ")]
	CmdChapterBwWorldTakeRewardLineRewardReq = 7660,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_TAKE_REWARD_LINE_REWARD_RSP")]
	CmdChapterBwWorldTakeRewardLineRewardRsp = 7661,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_SYNC_RUNE_NOTIFY")]
	CmdChapterBwWorldSyncRuneNotify = 7662,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_TOWER_STAGE_REPORT_FLOOR_REQ")]
	CmdChapterBwWorldTowerStageReportFloorReq = 7663,
	[ProtoEnum(Name = "CMD_CHAPTER_BW_WORLD_TOWER_STAGE_REPORT_FLOOR_RSP")]
	CmdChapterBwWorldTowerStageReportFloorRsp = 7664,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_GET_DATA_REQ")]
	CmdBwWorldCampActivityGetDataReq = 7671,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_GET_DATA_RSP")]
	CmdBwWorldCampActivityGetDataRsp = 7672,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_SELECT_CAMP_REQ")]
	CmdBwWorldCampActivitySelectCampReq = 7673,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_SELECT_CAMP_RSP")]
	CmdBwWorldCampActivitySelectCampRsp = 7674,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_PUBLISH_RUNE_REQ")]
	CmdBwWorldCampActivityPublishRuneReq = 7675,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_PUBLISH_RUNE_RSP")]
	CmdBwWorldCampActivityPublishRuneRsp = 7676,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_REFRESH_WAIT_SELECT_RUNE_REQ")]
	CmdBwWorldCampActivityRefreshWaitSelectRuneReq = 7677,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_REFRESH_WAIT_SELECT_RUNE_RSP")]
	CmdBwWorldCampActivityRefreshWaitSelectRuneRsp = 7678,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_TAKE_ASSIST_CONTRIBUTION_REQ")]
	CmdBwWorldCampActivityTakeAssistContributionReq = 7679,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_TAKE_ASSIST_CONTRIBUTION_RSP")]
	CmdBwWorldCampActivityTakeAssistContributionRsp = 7680,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_ADD_ASSIST_CONTRIBUTION_NOTIFY")]
	CmdBwWorldCampActivityAddAssistContributionNotify = 7681,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_GET_RANK_REQ")]
	CmdBwWorldCampActivityGetRankReq = 7682,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_GET_RANK_RSP")]
	CmdBwWorldCampActivityGetRankRsp = 7683,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_SELECT_STAGE_RUNE_REQ")]
	CmdBwWorldCampActivitySelectStageRuneReq = 7684,
	[ProtoEnum(Name = "CMD_BW_WORLD_CAMP_ACTIVITY_SELECT_STAGE_RUNE_RSP")]
	CmdBwWorldCampActivitySelectStageRuneRsp = 7685
}
