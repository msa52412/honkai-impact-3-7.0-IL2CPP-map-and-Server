using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterUnlockConditionType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_UNLOCK_NONE")]
	OpenworldChapterUnlockNone,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_UNLOCK_PRE_EVENT")]
	OpenworldChapterUnlockPreEvent,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_UNLOCK_DATETIME")]
	OpenworldChapterUnlockDatetime,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_UNLOCK_STAGE")]
	OpenworldChapterUnlockStage,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_UNLOCK_MISSION")]
	OpenworldChapterUnlockMission,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_UNLOCK_PRE_STORY")]
	OpenworldChapterUnlockPreStory,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_UNLOCK_PRE_TOWER")]
	OpenworldChapterUnlockPreTower
}
