using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TvtBattleJudgeType
{
	[ProtoEnum(Name = "TVT_BATTLE_JUDGE_NONE")]
	TvtBattleJudgeNone,
	[ProtoEnum(Name = "TVT_BATTLE_JUDGE_WIN_WHEN_ZERO")]
	TvtBattleJudgeWinWhenZero,
	[ProtoEnum(Name = "TVT_BATTLE_JUDGE_LOSE_WHEN_ZERO")]
	TvtBattleJudgeLoseWhenZero
}
