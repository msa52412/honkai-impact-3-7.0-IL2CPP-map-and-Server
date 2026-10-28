using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownActivityChallengeRecordType
{
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_RECORD_IN_GAME")]
	TownActivityChallengeRecordInGame = 1,
	[ProtoEnum(Name = "TOWN_ACTIVITY_CHALLENGE_RECORD_SETTLE_GAME")]
	TownActivityChallengeRecordSettleGame
}
