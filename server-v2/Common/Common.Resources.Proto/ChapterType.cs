using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChapterType
{
	[ProtoEnum(Name = "CHAPTER_TYPE_MAIN_STORY")]
	ChapterTypeMainStory = 1,
	[ProtoEnum(Name = "CHAPTER_TYPE_EVENT")]
	ChapterTypeEvent,
	[ProtoEnum(Name = "CHAPTER_TYPE_SPECIAL_STORY")]
	ChapterTypeSpecialStory,
	[ProtoEnum(Name = "CHAPTER_MP")]
	ChapterMp,
	[ProtoEnum(Name = "CHAPTER_TYPE_CHAPTER_ACTIVITY")]
	ChapterTypeChapterActivity,
	[ProtoEnum(Name = "CHAPTER_TYPE_HYBRID_SITE_ACTIVITY")]
	ChapterTypeHybridSiteActivity,
	[ProtoEnum(Name = "CHAPTER_TYPE_OPENWORLD")]
	ChapterTypeOpenworld
}
