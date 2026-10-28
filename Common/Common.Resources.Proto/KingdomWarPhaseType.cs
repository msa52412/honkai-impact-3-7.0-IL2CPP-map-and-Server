using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum KingdomWarPhaseType
{
	[ProtoEnum(Name = "KINGDOM_WAR_PHASE_NONE")]
	KingdomWarPhaseNone,
	[ProtoEnum(Name = "KINGDOM_WAR_PHASE_OCCUPY_POINT")]
	KingdomWarPhaseOccupyPoint,
	[ProtoEnum(Name = "KINGDOM_WAR_PHASE_ATTACK_AND_DEFENSE")]
	KingdomWarPhaseAttackAndDefense,
	[ProtoEnum(Name = "KINGDOM_WAR_PHASE_BATTLE_LINE")]
	KingdomWarPhaseBattleLine
}
