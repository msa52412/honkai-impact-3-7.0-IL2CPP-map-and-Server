using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldQuestStatus
{
	[ProtoEnum(Name = "OPENWORLD_QUEST_NONE")]
	OpenworldQuestNone,
	[ProtoEnum(Name = "OPENWORLD_QUEST_DOING")]
	OpenworldQuestDoing,
	[ProtoEnum(Name = "OPENWORLD_QUEST_FINISH")]
	OpenworldQuestFinish,
	[ProtoEnum(Name = "OPENWORLD_QUEST_CLOSE")]
	OpenworldQuestClose
}
