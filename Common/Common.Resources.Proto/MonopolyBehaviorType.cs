using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonopolyBehaviorType
{
	[ProtoEnum(Name = "MONOPOLY_BEHAVIOR_WALK")]
	MonopolyBehaviorWalk = 1,
	[ProtoEnum(Name = "MONOPOLY_BEHAVIOR_DEMOLISH_BUILDING")]
	MonopolyBehaviorDemolishBuilding = 2,
	[ProtoEnum(Name = "MONOPOLY_BEHAVIOR_FREEZE_BUILDING")]
	MonopolyBehaviorFreezeBuilding = 3,
	[ProtoEnum(Name = "MONOPOLY_BEHAVIOR_RANDOM_FREEZE")]
	MonopolyBehaviorRandomFreeze = 4,
	[ProtoEnum(Name = "MONOPOLY_BEHAVIOR_SUMMON")]
	MonopolyBehaviorSummon = 5,
	[ProtoEnum(Name = "MONOPOLY_BEHAVIOR_REPEL")]
	MonopolyBehaviorRepel = 7,
	[ProtoEnum(Name = "MONOPOLY_BEHAVIOR_DELELTE_SELF")]
	MonopolyBehaviorDelelteSelf = 8,
	[ProtoEnum(Name = "MONOPOLY_BEHAVIOR_TRIGGER_RPG_EVENT")]
	MonopolyBehaviorTriggerRpgEvent = 9
}
