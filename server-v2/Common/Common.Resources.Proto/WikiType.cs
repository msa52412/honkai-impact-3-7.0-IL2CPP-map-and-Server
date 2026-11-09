using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WikiType
{
	[ProtoEnum(Name = "WIKI_TYPE_OVERVIEW")]
	WikiTypeOverview,
	[ProtoEnum(Name = "WIKI_TYPE_WEAPON")]
	WikiTypeWeapon,
	[ProtoEnum(Name = "WIKI_TYPE_STIGMATA")]
	WikiTypeStigmata,
	[ProtoEnum(Name = "WIKI_TYPE_FURNITURE")]
	WikiTypeFurniture,
	[ProtoEnum(Name = "WIKI_TYPE_CG")]
	WikiTypeCg,
	[ProtoEnum(Name = "WIKI_TYPE_MEDAL")]
	WikiTypeMedal,
	[ProtoEnum(Name = "WIKI_TYPE_ACHIEVEMENT")]
	WikiTypeAchievement,
	[ProtoEnum(Name = "WIKI_TYPE_DRESS")]
	WikiTypeDress,
	[ProtoEnum(Name = "WIKI_TYPE_EMOJI")]
	WikiTypeEmoji,
	[ProtoEnum(Name = "WIKI_TYPE_AVATAR")]
	WikiTypeAvatar,
	[ProtoEnum(Name = "WIKI_TYPE_ACTIVITY")]
	WikiTypeActivity,
	[ProtoEnum(Name = "WIKI_TYPE_MONSTER")]
	WikiTypeMonster
}
