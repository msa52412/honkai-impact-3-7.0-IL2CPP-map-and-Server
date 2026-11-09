using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SlgBattleRankType
{
	[ProtoEnum(Name = "SLG_BATTLE_RANK_NORMAL_POINT")]
	SlgBattleRankNormalPoint = 1,
	[ProtoEnum(Name = "SLG_BATTLE_RANK_BIG_BOSS")]
	SlgBattleRankBigBoss,
	[ProtoEnum(Name = "SLG_BATTLE_RANK_COUNTRY")]
	SlgBattleRankCountry
}
