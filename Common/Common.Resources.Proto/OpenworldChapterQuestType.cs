using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterQuestType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_NORMAL_QUEST")]
	OpenworldChapterNormalQuest = 1,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_SPECIAL_QUEST")]
	OpenworldChapterSpecialQuest
}
