using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ActivityType
{
	[ProtoEnum(Name = "ACTIVITY_NONE")]
	ActivityNone = 0,
	[ProtoEnum(Name = "ACTIVITY_WEEK_DAY")]
	ActivityWeekDay = 1,
	[ProtoEnum(Name = "ACTIVITY_BEFALL")]
	ActivityBefall = 2,
	[ProtoEnum(Name = "ACTIVITY_NUCLEAR")]
	ActivityNuclear = 3,
	[ProtoEnum(Name = "ACTIVITY_MULTIPLAYER")]
	ActivityMultiplayer = 4,
	[ProtoEnum(Name = "ACTIVITY_MP_RAID")]
	ActivityMpRaid = 5,
	[ProtoEnum(Name = "ACTIVITY_SIMULATE_RANK")]
	ActivitySimulateRank = 7,
	[ProtoEnum(Name = "ACTIVITY_BOSS_RUSH")]
	ActivityBossRush = 8,
	[ProtoEnum(Name = "ACTIVITY_MINUTE")]
	ActivityMinute = 9,
	[ProtoEnum(Name = "ACTIVITY_SIMULATE_NO_RANK")]
	ActivitySimulateNoRank = 10,
	[ProtoEnum(Name = "ACTIVITY_RECLAIM")]
	ActivityReclaim = 12,
	[ProtoEnum(Name = "ACTIVITY_MP_OPENWORLD_BOSS")]
	ActivityMpOpenworldBoss = 13,
	[ProtoEnum(Name = "ACTIVITY_MATRIX")]
	ActivityMatrix = 14
}
