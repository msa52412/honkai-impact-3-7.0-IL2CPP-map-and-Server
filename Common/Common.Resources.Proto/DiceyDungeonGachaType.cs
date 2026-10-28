using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DiceyDungeonGachaType
{
	[ProtoEnum(Name = "DICEY_DUNGEON_GACHA_WEAPON")]
	DiceyDungeonGachaWeapon = 1,
	[ProtoEnum(Name = "DICEY_DUNGEON_GACHA_ORNAMENT_INITIATIVE")]
	DiceyDungeonGachaOrnamentInitiative,
	[ProtoEnum(Name = "DICEY_DUNGEON_GACHA_ORNAMENT_PASSIVE")]
	DiceyDungeonGachaOrnamentPassive
}
