using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldQuestFinishWay
{
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_NONE")]
	OpenworldQuestFinishWayNone,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_DLC_COIN")]
	OpenworldQuestFinishWayDlcCoin,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_FINISH_EVENT")]
	OpenworldQuestFinishWayFinishEvent,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_ASSASSIN_KILL")]
	OpenworldQuestFinishWayAssassinKill,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_QTE_KILL")]
	OpenworldQuestFinishWayQteKill,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_KILL_MONSTER_TYPE")]
	OpenworldQuestFinishWayKillMonsterType,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_AIR_KILL")]
	OpenworldQuestFinishWayAirKill,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_KILL_NATURE_MONSTER")]
	OpenworldQuestFinishWayKillNatureMonster,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_AIRSTRIKE_KILL")]
	OpenworldQuestFinishWayAirstrikeKill,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAY_THROWBLOW")]
	OpenworldQuestFinishWayThrowblow,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAT_CHAPTER_OW_FINAL_COIN")]
	OpenworldQuestFinishWatChapterOwFinalCoin,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAT_CHAPTER_OW_FINAL_ROBOT_ULTRA_KILL")]
	OpenworldQuestFinishWatChapterOwFinalRobotUltraKill,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH_WAT_CHAPTER_OW_FINAL_ROBOT_USE_CATCH")]
	OpenworldQuestFinishWatChapterOwFinalRobotUseCatch
}
