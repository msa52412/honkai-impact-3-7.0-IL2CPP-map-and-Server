using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlayerInterestType
{
	[ProtoEnum(Name = "INTEREST_NONE")]
	InterestNone,
	[ProtoEnum(Name = "INTEREST_ANIMATION")]
	InterestAnimation,
	[ProtoEnum(Name = "INTEREST_COMIC")]
	InterestComic,
	[ProtoEnum(Name = "INTEREST_GAME")]
	InterestGame,
	[ProtoEnum(Name = "INTEREST_MUSIC")]
	InterestMusic,
	[ProtoEnum(Name = "INTEREST_DANCING")]
	InterestDancing,
	[ProtoEnum(Name = "INTEREST_KITIKU")]
	InterestKitiku,
	[ProtoEnum(Name = "INTEREST_MMD")]
	InterestMmd,
	[ProtoEnum(Name = "INTEREST_TOKUSATSU")]
	InterestTokusatsu
}
