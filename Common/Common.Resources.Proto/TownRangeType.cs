using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownRangeType
{
	[ProtoEnum(Name = "TOWN_RANGE_NONE")]
	TownRangeNone,
	[ProtoEnum(Name = "TOWN_RANGE_CROSS")]
	TownRangeCross,
	[ProtoEnum(Name = "TOWN_RANGE_ROUND")]
	TownRangeRound
}
