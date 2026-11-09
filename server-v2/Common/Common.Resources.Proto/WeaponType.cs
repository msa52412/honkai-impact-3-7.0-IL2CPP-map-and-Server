using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WeaponType
{
	[ProtoEnum(Name = "WEAPON_DOUBLE_GUN")]
	WeaponDoubleGun = 1,
	[ProtoEnum(Name = "WEAPON_SWORD")]
	WeaponSword,
	[ProtoEnum(Name = "WEAPON_HAND_GUN")]
	WeaponHandGun,
	[ProtoEnum(Name = "WEAPON_HEAVY_SWORD")]
	WeaponHeavySword,
	[ProtoEnum(Name = "WEAPON_TAIL")]
	WeaponTail,
	[ProtoEnum(Name = "WEAPON_FIST")]
	WeaponFist,
	[ProtoEnum(Name = "WEAPON_SCYTHE")]
	WeaponScythe,
	[ProtoEnum(Name = "WEAPON_LANCE")]
	WeaponLance,
	[ProtoEnum(Name = "WEAPON_BOW")]
	WeaponBow,
	BladeRing,
	[ProtoEnum(Name = "WEAPON_JAVELIN")]
	WeaponJavelin
}
