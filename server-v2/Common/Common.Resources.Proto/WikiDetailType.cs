using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WikiDetailType
{
	[ProtoEnum(Name = "WIKI_UNKNOWN")]
	WikiUnknown,
	[ProtoEnum(Name = "WIKI_ALL")]
	WikiAll,
	[ProtoEnum(Name = "WIKI_STIGMATA")]
	WikiStigmata,
	[ProtoEnum(Name = "WIKI_WEAPON")]
	WikiWeapon,
	[ProtoEnum(Name = "WIKI_DORM")]
	WikiDorm,
	[ProtoEnum(Name = "WIKI_CG")]
	WikiCg,
	[ProtoEnum(Name = "WIKI_MEDAL")]
	WikiMedal,
	[ProtoEnum(Name = "WIKI_ACHIEVEMENT")]
	WikiAchievement,
	[ProtoEnum(Name = "WIKI_SKIN")]
	WikiSkin,
	[ProtoEnum(Name = "WIKI_EMOJI")]
	WikiEmoji,
	[ProtoEnum(Name = "WIKI_AVATAR")]
	WikiAvatar
}
