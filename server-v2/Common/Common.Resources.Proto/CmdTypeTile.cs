using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeTile
{
	[ProtoEnum(Name = "CMD_GET_TILE_MAP_REQ")]
	CmdGetTileMapReq = 4900,
	[ProtoEnum(Name = "CMD_GET_TILE_MAP_RSP")]
	CmdGetTileMapRsp,
	[ProtoEnum(Name = "CMD_TILE_UPDATE_MAP_REQ")]
	CmdTileUpdateMapReq,
	[ProtoEnum(Name = "CMD_TILE_UPDATE_MAP_RSP")]
	CmdTileUpdateMapRsp,
	[ProtoEnum(Name = "CMD_TILE_GET_TOWER_REQ")]
	CmdTileGetTowerReq,
	[ProtoEnum(Name = "CMD_TILE_GET_TOWER_RSP")]
	CmdTileGetTowerRsp,
	[ProtoEnum(Name = "CMD_TILE_ENTER_REQ")]
	CmdTileEnterReq,
	[ProtoEnum(Name = "CMD_TILE_ENTER_RSP")]
	CmdTileEnterRsp,
	[ProtoEnum(Name = "CMD_TILE_SAVE_PROGRESS_REQ")]
	CmdTileSaveProgressReq,
	[ProtoEnum(Name = "CMD_TILE_SAVE_PROGRESS_RSP")]
	CmdTileSaveProgressRsp,
	[ProtoEnum(Name = "CMD_TILE_SETTLE_EVENT_REQ")]
	CmdTileSettleEventReq,
	[ProtoEnum(Name = "CMD_TILE_SETTLE_EVENT_RSP")]
	CmdTileSettleEventRsp,
	[ProtoEnum(Name = "CMD_TILE_RESET_FLOOR_REQ")]
	CmdTileResetFloorReq,
	[ProtoEnum(Name = "CMD_TILE_RESET_FLOOR_RSP")]
	CmdTileResetFloorRsp,
	[ProtoEnum(Name = "CMD_TILE_ENTITY_OP_NOTIFY")]
	CmdTileEntityOpNotify,
	[ProtoEnum(Name = "CMD_TILE_SELECT_PATH_REQ")]
	CmdTileSelectPathReq,
	[ProtoEnum(Name = "CMD_TILE_SELECT_PATH_RSP")]
	CmdTileSelectPathRsp,
	[ProtoEnum(Name = "CMD_TILE_RESET_REGION_REQ")]
	CmdTileResetRegionReq,
	[ProtoEnum(Name = "CMD_TILE_RESET_REGION_RSP")]
	CmdTileResetRegionRsp,
	[ProtoEnum(Name = "CMD_TILE_REPORT_SAVED_POSITION_REQ")]
	CmdTileReportSavedPositionReq,
	[ProtoEnum(Name = "CMD_TILE_REPORT_SAVED_POSITION_RSP")]
	CmdTileReportSavedPositionRsp,
	[ProtoEnum(Name = "CMD_TILE_LOAD_SAVED_POSITION_REQ")]
	CmdTileLoadSavedPositionReq,
	[ProtoEnum(Name = "CMD_TILE_LOAD_SAVED_POSITION_RSP")]
	CmdTileLoadSavedPositionRsp,
	[ProtoEnum(Name = "CMD_TILE_MAP_UPDATE_NOTIFY")]
	CmdTileMapUpdateNotify
}
