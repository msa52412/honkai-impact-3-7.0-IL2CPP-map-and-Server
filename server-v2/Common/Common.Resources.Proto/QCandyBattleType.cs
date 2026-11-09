using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyBattleType
{
	[ProtoEnum(Name = "QCANDY_BATTLE_TYPE_NONE")]
	QcandyBattleTypeNone,
	[ProtoEnum(Name = "QCANDY_BATTLE_TYPE_RACING")]
	QcandyBattleTypeRacing,
	[ProtoEnum(Name = "QCANDY_BATTLE_TYPE_SURVIVE")]
	QcandyBattleTypeSurvive,
	[ProtoEnum(Name = "QCANDY_BATTLE_TYPE_SCORE")]
	QcandyBattleTypeScore
}
