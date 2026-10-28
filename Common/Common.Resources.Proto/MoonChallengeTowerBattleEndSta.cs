using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MoonChallengeTowerBattleEndStatus
{
	[ProtoEnum(Name = "MOON_CHALLENGE_TOWER_BATTLE_END_INVALID")]
	MoonChallengeTowerBattleEndInvalid,
	[ProtoEnum(Name = "MOON_CHALLENGE_TOWER_BATTLE_END_SUCC")]
	MoonChallengeTowerBattleEndSucc,
	[ProtoEnum(Name = "MOON_CHALLENGE_TOWER_BATTLE_END_FAIL")]
	MoonChallengeTowerBattleEndFail
}
