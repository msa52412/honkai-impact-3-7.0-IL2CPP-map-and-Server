using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DiceyDungeonRoomType
{
	[ProtoEnum(Name = "DICEY_DUNGEON_ROOM_DICEY_BATTLE")]
	DiceyDungeonRoomDiceyBattle = 1,
	[ProtoEnum(Name = "DICEY_DUNGEON_ROOM_AVATAR_BATTLE")]
	DiceyDungeonRoomAvatarBattle,
	[ProtoEnum(Name = "DICEY_DUNGEON_ROOM_ORNAMENT_REWARD")]
	DiceyDungeonRoomOrnamentReward,
	[ProtoEnum(Name = "DICEY_DUNGEON_ROOM_HP_RECOVER")]
	DiceyDungeonRoomHpRecover,
	[ProtoEnum(Name = "DICEY_DUNGEON_ROOM_PLOT")]
	DiceyDungeonRoomPlot,
	[ProtoEnum(Name = "DICEY_DUNGEON_ROOM_BOSS")]
	DiceyDungeonRoomBoss
}
