using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralActivityScoreRewardType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCORE_REWARD_AUTO")]
	GeneralActivityScoreRewardAuto = 1,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_SCORE_REWARD_MANUAL")]
	GeneralActivityScoreRewardManual
}
