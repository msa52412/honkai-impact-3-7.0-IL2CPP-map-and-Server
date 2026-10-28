using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldUnlockConditionType
{
	[ProtoEnum(Name = "OPENWORLD_UNLOCK_PRE_EVENT_LIST")]
	OpenworldUnlockPreEventList = 1,
	[ProtoEnum(Name = "OPENWORLD_UNLOCK_DATETIME")]
	OpenworldUnlockDatetime,
	[ProtoEnum(Name = "OPENWORLD_UNLOCK_STAGE_LIST")]
	OpenworldUnlockStageList,
	[ProtoEnum(Name = "OPENWORLD_UNLOCK_MISSION_LIST")]
	OpenworldUnlockMissionList,
	[ProtoEnum(Name = "OPENWORLD_UNLOCK_PRE_STORY_LIST")]
	OpenworldUnlockPreStoryList
}
