using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BurdenAlleviationType
{
	[ProtoEnum(Name = "BURDEN_ALLEVIATION_NONE")]
	BurdenAlleviationNone,
	[ProtoEnum(Name = "BURDEN_ALLEVIATION_EX_BOSS")]
	BurdenAlleviationExBoss,
	[ProtoEnum(Name = "BURDEN_ALLEVIATION_WEEKDAY")]
	BurdenAlleviationWeekday,
	[ProtoEnum(Name = "BURDEN_ALLEVIATION_RPG_TALE")]
	BurdenAlleviationRpgTale,
	[ProtoEnum(Name = "BURDEN_ALLEVIATION_RAID")]
	BurdenAlleviationRaid,
	[ProtoEnum(Name = "BURDEN_ALLEVIATION_DAILY_MP")]
	BurdenAlleviationDailyMp,
	[ProtoEnum(Name = "BURDEN_ALLEVIATION_GRANT_REWARD")]
	BurdenAlleviationGrantReward,
	[ProtoEnum(Name = "BURDEN_ALLEVIATION_MISSION")]
	BurdenAlleviationMission
}
