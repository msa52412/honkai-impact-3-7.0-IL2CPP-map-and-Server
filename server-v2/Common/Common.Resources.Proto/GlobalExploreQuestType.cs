using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreQuestType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_QUEST_EXPLORE")]
	GlobalExploreQuestExplore = 1,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_QUEST_MOVE_PATH")]
	GlobalExploreQuestMovePath = 2,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_QUEST_PASS_STAGE")]
	GlobalExploreQuestPassStage = 3,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_QUEST_ADD_MATERIAL")]
	GlobalExploreQuestAddMaterial = 4,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_QUEST_FINISH_QUEST")]
	GlobalExploreQuestFinishQuest = 100
}
