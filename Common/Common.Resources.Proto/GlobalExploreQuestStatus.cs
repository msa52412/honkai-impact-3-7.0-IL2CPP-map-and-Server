using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreQuestStatus
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_QUEST_DOING")]
	GlobalExploreQuestDoing = 1,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_QUEST_CLOSE")]
	GlobalExploreQuestClose
}
