using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExplorePoolContentType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_POOL_CONTENT_EVENT")]
	GlobalExplorePoolContentEvent = 1,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_POOL_CONTENT_QUEST")]
	GlobalExplorePoolContentQuest,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_POOL_CONTENT_ENTITY")]
	GlobalExplorePoolContentEntity
}
