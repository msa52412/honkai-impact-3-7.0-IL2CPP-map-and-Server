using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum StageRecordType
{
	[ProtoEnum(Name = "STAGE_RECORD_SCORE")]
	StageRecordScore = 1,
	[ProtoEnum(Name = "STAGE_RECORD_TIME")]
	StageRecordTime,
	[ProtoEnum(Name = "STAGE_RECORD_TOTAL_SCORE")]
	StageRecordTotalScore,
	[ProtoEnum(Name = "STAGE_RECORD_BATTLE_SCORE")]
	StageRecordBattleScore,
	[ProtoEnum(Name = "STAGE_RECORD_BATTLE_SCORE_NO_COIN")]
	StageRecordBattleScoreNoCoin
}
