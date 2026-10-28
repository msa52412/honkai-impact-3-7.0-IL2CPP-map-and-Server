using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RaidActiveType
{
	[ProtoEnum(Name = "RAID_ACTIVE_NONE")]
	RaidActiveNone,
	[ProtoEnum(Name = "RAID_ACTIVE_DAILY_RAID")]
	RaidActiveDailyRaid,
	[ProtoEnum(Name = "RAID_ACTIVE_WEEKLY_RAID")]
	RaidActiveWeeklyRaid
}
