using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralActivityScheduledMapSiteType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCHEDULED_MAP_SITE_DAILY_RAID")]
	GeneralActivityScheduledMapSiteDailyRaid = 1,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCHEDULED_MAP_SITE_MINI_SKY_FIRE")]
	GeneralActivityScheduledMapSiteMiniSkyFire = 2,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCHEDULED_MAP_SITE_TWO_PLUS_TWO_PVE")]
	GeneralActivityScheduledMapSiteTwoPlusTwoPve = 3,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCHEDULED_MAP_SITE_BOSS_RUSH")]
	GeneralActivityScheduledMapSiteBossRush = 4,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCHEDULED_MAP_SITE_CHAPTER_OW")]
	GeneralActivityScheduledMapSiteChapterOw = 6
}
