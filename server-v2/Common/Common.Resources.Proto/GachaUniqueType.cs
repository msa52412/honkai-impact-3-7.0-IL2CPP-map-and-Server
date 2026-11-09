using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GachaUniqueType
{
	[ProtoEnum(Name = "GACHA_UNIQUE_NONE")]
	GachaUniqueNone,
	[ProtoEnum(Name = "GACHA_UNIQUE_WEAPON")]
	GachaUniqueWeapon,
	[ProtoEnum(Name = "GACHA_UNIQUE_STIGMATA")]
	GachaUniqueStigmata,
	[ProtoEnum(Name = "GACHA_UNIQUE_EQUIP")]
	GachaUniqueEquip
}
