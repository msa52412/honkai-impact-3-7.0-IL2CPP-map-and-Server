using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SlgScoreRewardType
{
	[ProtoEnum(Name = "SLG_SCORE_REWARD_TYPE_NONE")]
	SlgScoreRewardTypeNone,
	[ProtoEnum(Name = "SLG_SCORE_REWARD_TYPE_PERSONAL_TOTAL")]
	SlgScoreRewardTypePersonalTotal,
	[ProtoEnum(Name = "SLG_SCORE_REWARD_TYPE_COUNTRY")]
	SlgScoreRewardTypeCountry
}
