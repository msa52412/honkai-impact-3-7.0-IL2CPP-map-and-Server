using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum KingdomWarCombatType
{
	[ProtoEnum(Name = "KINGDOM_WAR_COMBAT_TYPE_NONE")]
	KingdomWarCombatTypeNone,
	[ProtoEnum(Name = "KINGDOM_WAR_COMBAT_TYPE_VIP_POINT")]
	KingdomWarCombatTypeVipPoint,
	[ProtoEnum(Name = "KINGDOM_WAR_COMBAT_TYPE_LEVEL")]
	KingdomWarCombatTypeLevel,
	[ProtoEnum(Name = "KINGDOM_WAR_COMBAT_TYPE_AVATAR_STAR")]
	KingdomWarCombatTypeAvatarStar
}
