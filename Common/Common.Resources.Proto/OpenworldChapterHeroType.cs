using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldChapterHeroType
{
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_CARD_HERO")]
	OpenworldChapterCardHero = 1,
	[ProtoEnum(Name = "OPENWORLD_CHAPTER_NORMAL_HERO")]
	OpenworldChapterNormalHero
}
