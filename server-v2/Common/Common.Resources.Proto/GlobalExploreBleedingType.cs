using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreBleedingType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_BLEEDING_POINT")]
	GlobalExploreBleedingPoint = 1,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_BLEEDING_BOSS")]
	GlobalExploreBleedingBoss
}
