using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeTown
{
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_GET_DATA_REQ")]
	CmdTownActivityGetDataReq = 6450,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_GET_DATA_RSP")]
	CmdTownActivityGetDataRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SAVE_REQ")]
	CmdTownActivitySaveReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SAVE_RSP")]
	CmdTownActivitySaveRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_BUILD_REQ")]
	CmdTownActivityBuildReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_BUILD_RSP")]
	CmdTownActivityBuildRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_BATTLE_REQ")]
	CmdTownActivityBattleReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_BATTLE_RSP")]
	CmdTownActivityBattleRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SELL_BUILDING_REQ")]
	CmdTownActivitySellBuildingReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SELL_BUILDING_RSP")]
	CmdTownActivitySellBuildingRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_HEAL_AVATAR_REQ")]
	CmdTownActivityHealAvatarReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_HEAL_AVATAR_RSP")]
	CmdTownActivityHealAvatarRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SELL_BRICK_REQ")]
	CmdTownActivitySellBrickReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SELL_BRICK_RSP")]
	CmdTownActivitySellBrickRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_END_NOTIFY")]
	CmdTownActivityEndNotify,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_BOSS_ATTACK_NOTIFY")]
	CmdTownActivityBossAttackNotify,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_GET_SHOP_REQ")]
	CmdTownActivityGetShopReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_GET_SHOP_RSP")]
	CmdTownActivityGetShopRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SHOP_BUY_REQ")]
	CmdTownActivityShopBuyReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SHOP_BUY_RSP")]
	CmdTownActivityShopBuyRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_MATERIAL_USE_REQ")]
	CmdTownActivityMaterialUseReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_MATERIAL_USE_RSP")]
	CmdTownActivityMaterialUseRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SPEED_UP_HEAL_REQ")]
	CmdTownActivitySpeedUpHealReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_SPEED_UP_HEAL_RSP")]
	CmdTownActivitySpeedUpHealRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_CHECK_IN_OPTIONAL_REQ")]
	CmdTownActivityCheckInOptionalReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_CHECK_IN_OPTIONAL_RSP")]
	CmdTownActivityCheckInOptionalRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_RESET_REQ")]
	CmdTownActivityResetReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_RESET_RSP")]
	CmdTownActivityResetRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_ALTER_PATH_REQ")]
	CmdTownActivityAlterPathReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_ALTER_PATH_RSP")]
	CmdTownActivityAlterPathRsp,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_ATTACK_ENEMY_BUFF_NOTIFY")]
	CmdTownActivityAttackEnemyBuffNotify,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_REFRESH_BRICK_REQ")]
	CmdTownActivityRefreshBrickReq,
	[ProtoEnum(Name = "CMD_TOWN_ACTIVITY_REFRESH_BRICK_RSP")]
	CmdTownActivityRefreshBrickRsp
}
