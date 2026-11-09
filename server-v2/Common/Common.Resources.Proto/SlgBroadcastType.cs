using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SlgBroadcastType
{
	[ProtoEnum(Name = "SLG_BROADCAST_TYPE_OCCUPY")]
	SlgBroadcastTypeOccupy = 1,
	[ProtoEnum(Name = "SLG_BROADCAST_TYPE_SCORE_STREAK")]
	SlgBroadcastTypeScoreStreak,
	[ProtoEnum(Name = "SLG_BROADCAST_TYPE_OCCUPY_STREAK")]
	SlgBroadcastTypeOccupyStreak
}
