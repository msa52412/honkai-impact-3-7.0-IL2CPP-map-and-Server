using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChapterGroupSiteType
{
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_TYPE_INVALID")]
	ChapterGroupSiteTypeInvalid,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_TYPE_CHAPTER")]
	ChapterGroupSiteTypeChapter,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_TYPE_VIDEO")]
	ChapterGroupSiteTypeVideo,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_TYPE_OPENWORLD")]
	ChapterGroupSiteTypeOpenworld,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_TYPE_OW_STAGE")]
	ChapterGroupSiteTypeOwStage
}
