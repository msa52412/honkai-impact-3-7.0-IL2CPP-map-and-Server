using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldFunctionUnlockType
{
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_UNLOCK_AUTO")]
	OpenworldFunctionUnlockAuto,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_UNLOCK_QUEST_LEVEL")]
	OpenworldFunctionUnlockQuestLevel,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_UNLOCK_STORY")]
	OpenworldFunctionUnlockStory,
	[ProtoEnum(Name = "OPENWORLD_FUNCTION_UNLOCK_TECH_LEVEL")]
	OpenworldFunctionUnlockTechLevel
}
