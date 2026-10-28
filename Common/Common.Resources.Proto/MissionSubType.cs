using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MissionSubType
{
	[ProtoEnum(Name = "MISSION_SUB_BRANCH")]
	MissionSubBranch = 1,
	[ProtoEnum(Name = "MISSION_SUB_MP_RAID")]
	MissionSubMpRaid = 102,
	[ProtoEnum(Name = "MISSION_SUB_ARMADA")]
	MissionSubArmada = 301,
	[ProtoEnum(Name = "MISSION_SUB_SHOP_ADD_ON")]
	MissionSubShopAddOn = 501,
	[ProtoEnum(Name = "MISSION_SUB_RAFFLE")]
	MissionSubRaffle = 502,
	[ProtoEnum(Name = "MISSION_SUB_WEB_ACTIVITY")]
	MissionSubWebActivity = 503,
	[ProtoEnum(Name = "MISSION_SUB_COMPETITION_SEASON")]
	MissionSubCompetitionSeason = 504,
	[ProtoEnum(Name = "MISSION_SUB_DREAM")]
	MissionSubDream = 505,
	[ProtoEnum(Name = "MISSION_SUB_REFRESH_CYCLE_ON_FINISH")]
	MissionSubRefreshCycleOnFinish = 506,
	[ProtoEnum(Name = "MISSION_SUB_GOD_WAR")]
	MissionSubGodWar = 507,
	[ProtoEnum(Name = "MISSION_SUB_SC_DLC")]
	MissionSubScDlc = 508,
	[ProtoEnum(Name = "MISSION_SUB_CHAPTER_OW")]
	MissionSubChapterOw = 509,
	[ProtoEnum(Name = "MISSION_SUB_OW_STORY")]
	MissionSubOwStory = 510,
	[ProtoEnum(Name = "MISSION_SUB_RPG_TAKE_REWARD_DROP")]
	MissionSubRpgTakeRewardDrop = 1301,
	[ProtoEnum(Name = "MISSION_SUB_GOBACK")]
	MissionSubGoback = 1501,
	[ProtoEnum(Name = "MISSION_SUB_GOBACK_ACTIVITY")]
	MissionSubGobackActivity = 1502,
	[ProtoEnum(Name = "MISSION_SUB_GOBACK_LIMIT_TIME")]
	MissionSubGobackLimitTime = 1503,
	[ProtoEnum(Name = "MISSION_SUB_NINJA")]
	MissionSubNinja = 2001,
	[ProtoEnum(Name = "MISSION_SUB_ARMADA_REUNION")]
	MissionSubArmadaReunion = 2002,
	[ProtoEnum(Name = "MISSION_SUB_LOGIN_MISSION")]
	MissionSubLoginMission = 2003,
	[ProtoEnum(Name = "MISSION_SUB_INVITEE")]
	MissionSubInvitee = 2004
}
