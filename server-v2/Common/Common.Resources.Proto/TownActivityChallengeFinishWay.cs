using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownActivityChallengeFinishWay
{
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_BUILDING_HP")]
	TownActivityChallengeBuildingHp = 1,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_AVATAR_DIE_TIMES")]
	TownActivityChallengeAvatarDieTimes,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_BUILDING_TIMES")]
	TownActivityChallengeBuildingTimes,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_STRENTH_AVATAR_NUM")]
	TownActivityChallengeStrenthAvatarNum,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_COIN_NUM")]
	TownActivityChallengeCoinNum
}
