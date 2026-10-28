using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExplorePoolType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_POOL_WITH_REPLACEMENT")]
	GlobalExplorePoolWithReplacement,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_POOL_WITHOUT_REPLACEMENT")]
	GlobalExplorePoolWithoutReplacement,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_POOL_CYCLE")]
	GlobalExplorePoolCycle
}
