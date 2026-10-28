using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownActivityChallengeCmpType
{
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_CMP_GREATER")]
	TownActivityChallengeCmpGreater = 1,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_CMP_LESS")]
	TownActivityChallengeCmpLess,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_CMP_EQUAL")]
	TownActivityChallengeCmpEqual,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_CMP_GREATER_EQUAL")]
	TownActivityChallengeCmpGreaterEqual,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_CMP_LESS_EQUAL")]
	TownActivityChallengeCmpLessEqual
}
