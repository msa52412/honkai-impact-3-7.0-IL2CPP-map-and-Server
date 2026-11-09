using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RankScheduleType
{
	[ProtoEnum(Name = "RANK_SCHEDULE_DORM")]
	RankScheduleDorm = 1
}
