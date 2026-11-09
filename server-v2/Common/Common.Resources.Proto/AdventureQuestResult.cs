using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AdventureQuestResult
{
	[ProtoEnum(Name = "ADVENTURE_QUEST_RESULT_NONE")]
	AdventureQuestResultNone,
	[ProtoEnum(Name = "ADVENTURE_QUEST_RESULT_FAIL")]
	AdventureQuestResultFail,
	[ProtoEnum(Name = "ADVENTURE_QUEST_RESULT_SUCC")]
	AdventureQuestResultSucc,
	[ProtoEnum(Name = "ADVENTURE_QUEST_RESULT_BIG_SUCC")]
	AdventureQuestResultBigSucc
}
