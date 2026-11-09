using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RankPowerType
{
	[ProtoEnum(Name = "AVATAR_SSS_NUM")]
	AvatarSssNum = 101,
	[ProtoEnum(Name = "AVATAR_SS_NUM")]
	AvatarSsNum = 102,
	[ProtoEnum(Name = "AVATAR_S_NUM")]
	AvatarSNum = 103,
	[ProtoEnum(Name = "AVATAR_A_NUM")]
	AvatarANum = 104,
	[ProtoEnum(Name = "WEAPON_5STAR_NUM")]
	Weapon5starNum = 201,
	[ProtoEnum(Name = "WEAPON_4STAR_NUM")]
	Weapon4starNum = 202,
	[ProtoEnum(Name = "WEAPON_3STAR_NUM")]
	Weapon3starNum = 203,
	[ProtoEnum(Name = "STIGMATA_5STAR_NUM")]
	Stigmata5starNum = 301,
	[ProtoEnum(Name = "STIGMATA_4STAR_NUM")]
	Stigmata4starNum = 302,
	[ProtoEnum(Name = "STIGMATA_3STAR_NUM")]
	Stigmata3starNum = 303
}
