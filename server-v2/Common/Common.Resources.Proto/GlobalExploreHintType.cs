using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreHintType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_HINT_NONE")]
	GlobalExploreHintNone,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_HINT_COIN")]
	GlobalExploreHintCoin,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_HINT_INVISIBLE_REWARD")]
	GlobalExploreHintInvisibleReward,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_HINT_COMPLETE_QUEST")]
	GlobalExploreHintCompleteQuest
}
