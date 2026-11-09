using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ActivityModuleType
{
	[ProtoEnum(Name = "ACTIVITY_MODULE_MISC")]
	ActivityModuleMisc = 1,
	[ProtoEnum(Name = "ACTIVITY_MODULE_RECLAIM")]
	ActivityModuleReclaim,
	[ProtoEnum(Name = "ACTIVITY_MODULE_FLOP")]
	ActivityModuleFlop,
	[ProtoEnum(Name = "ACTIVITY_MODULE_BINGO")]
	ActivityModuleBingo,
	[ProtoEnum(Name = "ACTIVITY_MODULE_BULLETIN_SCORE")]
	ActivityModuleBulletinScore,
	[ProtoEnum(Name = "ACTIVITY_MODULE_RELATION")]
	ActivityModuleRelation,
	[ProtoEnum(Name = "ACTIVITY_MODULE_FARM")]
	ActivityModuleFarm,
	[ProtoEnum(Name = "ACTIVITY_MODULE_TOWER_RAID")]
	ActivityModuleTowerRaid,
	[ProtoEnum(Name = "ACTIVITY_MODULE_CONTINUOUS_RECHARGE")]
	ActivityModuleContinuousRecharge,
	[ProtoEnum(Name = "ACTIVITY_MODULE_MISSION_THEME")]
	ActivityModuleMissionTheme,
	[ProtoEnum(Name = "ACTIVITY_MODULE_OFFLINE_RESOURECE")]
	ActivityModuleOfflineResourece,
	[ProtoEnum(Name = "ACTIVITY_MODULE_WEEKLY_ROUTINE")]
	ActivityModuleWeeklyRoutine,
	[ProtoEnum(Name = "ACTIVITY_MODULE_AVATAR_MISSION")]
	ActivityModuleAvatarMission,
	[ProtoEnum(Name = "ACTIVITY_MODULE_KINGDOW_WAR")]
	ActivityModuleKingdowWar,
	[ProtoEnum(Name = "ACTIVITY_MODULE_TVT")]
	ActivityModuleTvt,
	[ProtoEnum(Name = "ACTIVITY_MODULE_CHAPTER")]
	ActivityModuleChapter,
	[ProtoEnum(Name = "ACTIVITY_MODULE_BATTLE_PASS")]
	ActivityModuleBattlePass,
	[ProtoEnum(Name = "ACTIVITY_MODULE_MONOPOLY")]
	ActivityModuleMonopoly,
	[ProtoEnum(Name = "ACTIVITY_MODULE_RAFFLE")]
	ActivityModuleRaffle,
	[ProtoEnum(Name = "ACTIVITY_MODULE_OVERLAP")]
	ActivityModuleOverlap,
	[ProtoEnum(Name = "ACTIVITY_MODULE_GLOBAL_WAR")]
	ActivityModuleGlobalWar,
	[ProtoEnum(Name = "ACTIVITY_MODULE_GRATUITY")]
	ActivityModuleGratuity,
	[ProtoEnum(Name = "ACTIVITY_MODULE_SCRATCH_TICKET")]
	ActivityModuleScratchTicket,
	[ProtoEnum(Name = "ACTIVITY_MODULE_BBQ")]
	ActivityModuleBbq,
	[ProtoEnum(Name = "ACTIVITY_MODULE_NEWBIE")]
	ActivityModuleNewbie,
	[ProtoEnum(Name = "ACTIVITY_MODULE_MOSAIC")]
	ActivityModuleMosaic,
	[ProtoEnum(Name = "ACTIVITY_MODULE_NINJA")]
	ActivityModuleNinja,
	[ProtoEnum(Name = "ACTIVITY_MODULE_FAST_PASS")]
	ActivityModuleFastPass,
	[ProtoEnum(Name = "ACTIVITY_MODULE_QUESTION")]
	ActivityModuleQuestion,
	[ProtoEnum(Name = "ACTIVITY_MODULE_TRADING_CARD")]
	ActivityModuleTradingCard,
	[ProtoEnum(Name = "ACTIVITY_MODULE_ARMADA_STAGE_SCORE")]
	ActivityModuleArmadaStageScore,
	[ProtoEnum(Name = "ACTIVITY_MODULE_PICTURE")]
	ActivityModulePicture,
	[ProtoEnum(Name = "ACTIVITY_MODULE_JIGSAW")]
	ActivityModuleJigsaw,
	[ProtoEnum(Name = "ACTIVITY_MODULE_ARMADA_REUNION")]
	ActivityModuleArmadaReunion,
	[ProtoEnum(Name = "ACTIVITY_MODULE_GARDEN")]
	ActivityModuleGarden,
	[ProtoEnum(Name = "ACTIVITY_MODULE_CHATWORLD_DISH")]
	ActivityModuleChatworldDish,
	[ProtoEnum(Name = "ACTIVITY_MODULE_DROP_LIMIT")]
	ActivityModuleDropLimit,
	[ProtoEnum(Name = "ACTIVITY_MODULE_LOGIN_MISSION")]
	ActivityModuleLoginMission,
	[ProtoEnum(Name = "ACTIVITY_MODULE_MASSIVE_WAR")]
	ActivityModuleMassiveWar,
	[ProtoEnum(Name = "ACTIVITY_MODULE_SANCTUARY")]
	ActivityModuleSanctuary,
	[ProtoEnum(Name = "ACTIVITY_MODULE_REWARD_STATISTIC")]
	ActivityModuleRewardStatistic,
	[ProtoEnum(Name = "ACTIVITY_MODULE_SLOT_MACHINE")]
	ActivityModuleSlotMachine,
	[ProtoEnum(Name = "ACTIVITY_MODULE_EXAMINATION")]
	ActivityModuleExamination,
	[ProtoEnum(Name = "ACTIVITY_MODULE_SUPPORT")]
	ActivityModuleSupport,
	[ProtoEnum(Name = "ACTIVITY_MODULE_BLOCK_ZONE")]
	ActivityModuleBlockZone,
	[ProtoEnum(Name = "ACTIVITY_MODULE_LOGIN_WISH")]
	ActivityModuleLoginWish,
	[ProtoEnum(Name = "ACTIVITY_MODULE_BURDEN_ALLEVIATION")]
	ActivityModuleBurdenAlleviation,
	[ProtoEnum(Name = "ACTIVITY_MODULE_ROGUE_TOWER")]
	ActivityModuleRogueTower,
	[ProtoEnum(Name = "ACTIVITY_MODULE_RPG_DUNGEON")]
	ActivityModuleRpgDungeon,
	[ProtoEnum(Name = "ACTIVITY_MODULE_REUNION_COOK")]
	ActivityModuleReunionCook,
	[ProtoEnum(Name = "ACTIVITY_MODULE_DREAM")]
	ActivityModuleDream,
	[ProtoEnum(Name = "ACTIVITY_MODULE_SLG")]
	ActivityModuleSlg,
	[ProtoEnum(Name = "ACTIVITY_MODULE_THEME_WANTED")]
	ActivityModuleThemeWanted,
	[ProtoEnum(Name = "ACTIVITY_MODULE_GLOBAL_POLLING")]
	ActivityModuleGlobalPolling,
	[ProtoEnum(Name = "ACTIVITY_MODULE_RANCH")]
	ActivityModuleRanch,
	[ProtoEnum(Name = "ACTIVITY_MODULE_RESTAURANT")]
	ActivityModuleRestaurant,
	[ProtoEnum(Name = "ACTIVITY_MODULE_TOWN")]
	ActivityModuleTown,
	[ProtoEnum(Name = "ACTIVITY_MODULE_WEB")]
	ActivityModuleWeb,
	[ProtoEnum(Name = "ACTIVITY_MODULE_MONSTER_CARD")]
	ActivityModuleMonsterCard,
	[ProtoEnum(Name = "ACTIVITY_MODULE_DICEY_DUNGEON")]
	ActivityModuleDiceyDungeon,
	[ProtoEnum(Name = "ACTIVITY_MODULE_PVZ")]
	ActivityModulePvz,
	[ProtoEnum(Name = "ACTIVITY_MODULE_BUFF_ASSIST")]
	ActivityModuleBuffAssist,
	[ProtoEnum(Name = "ACTIVITY_MODULE_INVITE")]
	ActivityModuleInvite,
	[ProtoEnum(Name = "ACTIVITY_MODULE_QCANDY_BATTLE")]
	ActivityModuleQcandyBattle,
	[ProtoEnum(Name = "ACTIVITY_MODULE_WAVE_RUSH")]
	ActivityModuleWaveRush,
	[ProtoEnum(Name = "ACTIVITY_MODULE_AI_CYBER")]
	ActivityModuleAiCyber,
	[ProtoEnum(Name = "ACTIVITY_MODULE_MINI_MONOPOLY")]
	ActivityModuleMiniMonopoly,
	[ProtoEnum(Name = "ACTIVITY_MODULE_GLOBAL_EXPLORE")]
	ActivityModuleGlobalExplore,
	[ProtoEnum(Name = "ACTIVITY_MODULE_PLATFORM_SHOOTER")]
	ActivityModulePlatformShooter,
	[ProtoEnum(Name = "ACTIVITY_MODULE_AVATAR_CULTIVATE")]
	ActivityModuleAvatarCultivate,
	[ProtoEnum(Name = "ACTIVITY_MODULE_MIRAGE")]
	ActivityModuleMirage,
	[ProtoEnum(Name = "ACTIVITY_MODULE_SUSANA_TRIAL")]
	ActivityModuleSusanaTrial,
	[ProtoEnum(Name = "ACTIVITY_MODULE_MATCH_THREE")]
	ActivityModuleMatchThree,
	[ProtoEnum(Name = "ACTIVITY_MODULE_CHAPTER_BW_WORLD")]
	ActivityModuleChapterBwWorld,
	[ProtoEnum(Name = "ACTIVITY_MODULE_SHIGURE_KIRA")]
	ActivityModuleShigureKira
}
