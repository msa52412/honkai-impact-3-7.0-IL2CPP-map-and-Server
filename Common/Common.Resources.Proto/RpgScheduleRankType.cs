using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgScheduleRankType
{
	[ProtoEnum(Name = "RPG_SCHEDULE_RANK_COLLECTION")]
	RpgScheduleRankCollection = 1,
	[ProtoEnum(Name = "RPG_SCHEDULE_RANK_GENERAL")]
	RpgScheduleRankGeneral
}
