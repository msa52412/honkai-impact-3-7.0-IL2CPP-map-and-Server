using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterStoryType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_STAGE")]
	OpenworldChapterStage = 1,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_STORY")]
	OpenworldChapterStory
}
