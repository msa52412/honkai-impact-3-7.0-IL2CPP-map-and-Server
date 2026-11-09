using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WeaponBranchType
{
	[ProtoEnum(Name = "WEAPON_DEFAULT")]
	WeaponDefault = 1,
	[ProtoEnum(Name = "WEAPON_BEFALL")]
	WeaponBefall
}
