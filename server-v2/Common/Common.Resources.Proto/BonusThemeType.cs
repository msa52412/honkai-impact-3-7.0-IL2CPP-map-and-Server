using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BonusThemeType
{
	[ProtoEnum(Name = "BONUS_THEME_TYPE_NONE")]
	BonusThemeTypeNone,
	[ProtoEnum(Name = "BONUS_THEME_TYPE_BONUS_SCORE_RATIO")]
	BonusThemeTypeBonusScoreRatio,
	[ProtoEnum(Name = "BONUS_THEME_TYPE_BONUS_SCORE_FIXED")]
	BonusThemeTypeBonusScoreFixed,
	[ProtoEnum(Name = "BONUS_THEME_TYPE_BONUS_DROP_PROB")]
	BonusThemeTypeBonusDropProb,
	[ProtoEnum(Name = "BONUS_THEME_TYPE_BONUS_DROP_FIXED")]
	BonusThemeTypeBonusDropFixed
}
