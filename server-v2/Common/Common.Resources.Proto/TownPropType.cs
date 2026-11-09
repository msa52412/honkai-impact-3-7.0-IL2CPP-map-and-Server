using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownPropType
{
	[ProtoEnum(Name = "TOWN_NONE")]
	TownNone,
	[ProtoEnum(Name = "TOWN_AVATAR")]
	TownAvatar,
	[ProtoEnum(Name = "TOWN_MONSTER")]
	TownMonster
}
