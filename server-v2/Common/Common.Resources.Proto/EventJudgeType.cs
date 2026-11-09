using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EventJudgeType
{
	[ProtoEnum(Name = "EVENT_JUDGE_TYPE_NONT")]
	EventJudgeTypeNont,
	[ProtoEnum(Name = "EVENT_JUDGE_TYPE_COLLECT")]
	EventJudgeTypeCollect,
	[ProtoEnum(Name = "EVENT_JUDGE_TYPE_MONSTER")]
	EventJudgeTypeMonster,
	[ProtoEnum(Name = "EVENT_JUDGE_TYPE_EMERGENCY")]
	EventJudgeTypeEmergency
}
