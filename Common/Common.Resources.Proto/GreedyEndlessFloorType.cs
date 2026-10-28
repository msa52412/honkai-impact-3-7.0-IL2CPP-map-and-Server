using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GreedyEndlessFloorType
{
	[ProtoEnum(Name = "GREEDY_ENDLESS_FLOOR_DEMOTE")]
	GreedyEndlessFloorDemote = 1,
	[ProtoEnum(Name = "GREEDY_ENDLESS_FLOOR_NORMAL")]
	GreedyEndlessFloorNormal,
	[ProtoEnum(Name = "GREEDY_ENDLESS_FLOOR_PROMOTE")]
	GreedyEndlessFloorPromote
}
