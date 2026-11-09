using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChapterGroupSiteStatus
{
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_STATUS_INVALID")]
	ChapterGroupSiteStatusInvalid,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_STATUS_LOCKED")]
	ChapterGroupSiteStatusLocked,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_STATUS_UNLOCKED")]
	ChapterGroupSiteStatusUnlocked,
	[ProtoEnum(Name = "CHAPTER_GROUP_SITE_STATUS_FINISHED")]
	ChapterGroupSiteStatusFinished
}
