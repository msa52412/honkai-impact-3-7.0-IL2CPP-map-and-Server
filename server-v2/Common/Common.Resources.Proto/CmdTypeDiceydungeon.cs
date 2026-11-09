using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeDiceydungeon
{
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_GET_DATA_REQ")]
	CmdDiceyDungeonActivityGetDataReq = 6500,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_GET_DATA_RSP")]
	CmdDiceyDungeonActivityGetDataRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_ROLE_LEVEL_UP_REQ")]
	CmdDiceyDungeonActivityRoleLevelUpReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_ROLE_LEVEL_UP_RSP")]
	CmdDiceyDungeonActivityRoleLevelUpRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_DUNGEON_BEGIN_REQ")]
	CmdDiceyDungeonActivityDungeonBeginReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_DUNGEON_BEGIN_RSP")]
	CmdDiceyDungeonActivityDungeonBeginRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_DUNGEON_END_REQ")]
	CmdDiceyDungeonActivityDungeonEndReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_DUNGEON_END_RSP")]
	CmdDiceyDungeonActivityDungeonEndRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_ROOM_BEGIN_REQ")]
	CmdDiceyDungeonActivityRoomBeginReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_ROOM_BEGIN_RSP")]
	CmdDiceyDungeonActivityRoomBeginRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_ROOM_END_REQ")]
	CmdDiceyDungeonActivityRoomEndReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_ROOM_END_RSP")]
	CmdDiceyDungeonActivityRoomEndRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_GET_WEAPON_GACHA_DISPLAY_REQ")]
	CmdDiceyDungeonActivityGetWeaponGachaDisplayReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_GET_WEAPON_GACHA_DISPLAY_RSP")]
	CmdDiceyDungeonActivityGetWeaponGachaDisplayRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_WEAPON_GACHA_REQ")]
	CmdDiceyDungeonActivityWeaponGachaReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_WEAPON_GACHA_RSP")]
	CmdDiceyDungeonActivityWeaponGachaRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_SELECT_ORNAMENT_REQ")]
	CmdDiceyDungeonActivitySelectOrnamentReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_SELECT_ORNAMENT_RSP")]
	CmdDiceyDungeonActivitySelectOrnamentRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_REFRESH_ORNAMENT_REQ")]
	CmdDiceyDungeonActivityRefreshOrnamentReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_REFRESH_ORNAMENT_RSP")]
	CmdDiceyDungeonActivityRefreshOrnamentRsp,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_GET_DAILY_SCORE_RANK_REQ")]
	CmdDiceyDungeonActivityGetDailyScoreRankReq,
	[ProtoEnum(Name = "CMD_DICEY_DUNGEON_ACTIVITY_GET_DAILY_SCORE_RANK_RSP")]
	CmdDiceyDungeonActivityGetDailyScoreRankRsp
}
