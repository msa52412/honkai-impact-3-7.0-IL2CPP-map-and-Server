using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum KingdomWarKingdomStatus
{
	[ProtoEnum(Name = "KINGDOM_WAR_KINGDOM_NONE")]
	KingdomWarKingdomNone,
	[ProtoEnum(Name = "KINGDOM_WAR_KINGDOM_DEFENSE")]
	KingdomWarKingdomDefense,
	[ProtoEnum(Name = "KINGDOM_WAR_KINGDOM_ATTACK")]
	KingdomWarKingdomAttack
}
