using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SlgMatchFactorType
{
	[ProtoEnum(Name = "SLG_MATCH_FACTOR_TYPE_NONE")]
	SlgMatchFactorTypeNone,
	[ProtoEnum(Name = "SLG_MATCH_FACTOR_TYPE_CRYSTAL_COST")]
	SlgMatchFactorTypeCrystalCost,
	[ProtoEnum(Name = "SLG_MATCH_FACTOR_TYPE_PLAYER_LEVEL")]
	SlgMatchFactorTypePlayerLevel,
	[ProtoEnum(Name = "SLG_MATCH_FACTOR_TYPE_TOP_AVATAR_COMBAT_SUM")]
	SlgMatchFactorTypeTopAvatarCombatSum,
	[ProtoEnum(Name = "SLG_MATCH_FACTOR_TYPE_CUR_BATTLE_SCORE")]
	SlgMatchFactorTypeCurBattleScore
}
