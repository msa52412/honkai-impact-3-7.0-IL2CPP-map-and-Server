using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AdventureQuestStatus
{
	[ProtoEnum(Name = "ADVENTURE_QUEST_STATUS_NONE")]
	AdventureQuestStatusNone,
	[ProtoEnum(Name = "ADVENTURE_QUEST_STATUS_DOING")]
	AdventureQuestStatusDoing,
	[ProtoEnum(Name = "ADVENTURE_QUEST_STATUS_FINISHED")]
	AdventureQuestStatusFinished
}
