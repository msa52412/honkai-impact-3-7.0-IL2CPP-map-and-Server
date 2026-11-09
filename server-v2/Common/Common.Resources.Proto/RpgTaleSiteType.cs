using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleSiteType
{
	[ProtoEnum(Name = "RPG_TALE_SITE_TYPE_STORY")]
	RpgTaleSiteTypeStory = 1,
	[ProtoEnum(Name = "RPG_TALE_SITE_TYPE_CLUE")]
	RpgTaleSiteTypeClue,
	[ProtoEnum(Name = "RPG_TALE_SITE_TYPE_CYCLE")]
	RpgTaleSiteTypeCycle,
	[ProtoEnum(Name = "RPG_TALE_SITE_TYPE_RANK")]
	RpgTaleSiteTypeRank,
	[ProtoEnum(Name = "RPG_TALE_SITE_TYPE_ENDLESS")]
	RpgTaleSiteTypeEndless
}
