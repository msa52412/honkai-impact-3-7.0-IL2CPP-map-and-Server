using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatchThreeGemGachaPoolStatus
{
	[ProtoEnum(Name = "MATCH_THREE_GEM_GACHA_POOL_LOCKED")]
	MatchThreeGemGachaPoolLocked = 1,
	[ProtoEnum(Name = "MATCH_THREE_GEM_GACHA_POOL_DOING")]
	MatchThreeGemGachaPoolDoing,
	[ProtoEnum(Name = "MATCH_THREE_GEM_GACHA_POOL_FINISHED")]
	MatchThreeGemGachaPoolFinished
}
