using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ExtraStoryChapterType
{
	[ProtoEnum(Name = "EXTRA_STORY_CHAPTER_INVALID")]
	ExtraStoryChapterInvalid,
	[ProtoEnum(Name = "EXTRA_STORY_CHAPTER_NORMAL")]
	ExtraStoryChapterNormal,
	[ProtoEnum(Name = "EXTRA_STORY_CHAPTER_LINE")]
	ExtraStoryChapterLine
}
