using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeVirtualtrain
{
	[ProtoEnum(Name = "CMD_GET_VIRTUAL_AVATAR_GROUP_DETAIL_REQ")]
	CmdGetVirtualAvatarGroupDetailReq = 3502,
	[ProtoEnum(Name = "CMD_GET_VIRTUAL_AVATAR_GROUP_DETAIL_RSP")]
	CmdGetVirtualAvatarGroupDetailRsp = 3503,
	[ProtoEnum(Name = "CMD_SET_VIRTUAL_AVATAR_TEAM_REQ")]
	CmdSetVirtualAvatarTeamReq = 3504,
	[ProtoEnum(Name = "CMD_SET_VIRTUAL_AVATAR_TEAM_RSP")]
	CmdSetVirtualAvatarTeamRsp = 3505,
	[ProtoEnum(Name = "CMD_DRESS_VIRTUAL_EQUIPMENT_REQ")]
	CmdDressVirtualEquipmentReq = 3506,
	[ProtoEnum(Name = "CMD_DRESS_VIRTUAL_EQUIPMENT_RSP")]
	CmdDressVirtualEquipmentRsp = 3507,
	[ProtoEnum(Name = "CMD_VIRTUAL_GACHA_REQ")]
	CmdVirtualGachaReq = 3508,
	[ProtoEnum(Name = "CMD_VIRTUAL_GACHA_RSP")]
	CmdVirtualGachaRsp = 3509,
	[ProtoEnum(Name = "CMD_GET_VIRTUAL_GACHA_STATUS_REQ")]
	CmdGetVirtualGachaStatusReq = 3510,
	[ProtoEnum(Name = "CMD_GET_VIRTUAL_GACHA_STATUS_RSP")]
	CmdGetVirtualGachaStatusRsp = 3511,
	[ProtoEnum(Name = "CMD_VIRTUAL_AVATAR_GROUP_CHANGE_NOTIFY")]
	CmdVirtualAvatarGroupChangeNotify = 3512,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_DIRECT_GACHA_NOTIFY")]
	CmdVirtualTrainDirectGachaNotify = 3513,
	[ProtoEnum(Name = "CMD_CHOOSE_VIRTUAL_GACHA_ITEM_REQ")]
	CmdChooseVirtualGachaItemReq = 3514,
	[ProtoEnum(Name = "CMD_CHOOSE_VIRTUAL_GACHA_ITEM_RSP")]
	CmdChooseVirtualGachaItemRsp = 3515,
	[ProtoEnum(Name = "CMD_CHOOSE_VIRTUAL_DEFAULT_AVATAR_NOTIFY")]
	CmdChooseVirtualDefaultAvatarNotify = 3516,
	[ProtoEnum(Name = "CMD_SYNC_VIRTUAL_GACHA_STATUS_NOTIFY")]
	CmdSyncVirtualGachaStatusNotify = 3517,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_ROLE_CUSTOM_LEVEL_UP_REQ")]
	CmdVirtualTrainRoleCustomLevelUpReq = 3518,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_ROLE_CUSTOM_LEVEL_UP_RSP")]
	CmdVirtualTrainRoleCustomLevelUpRsp = 3519,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_ROLE_CUSTOM_CHOOSE_REQ")]
	CmdVirtualTrainRoleCustomChooseReq = 3520,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_ROLE_CUSTOM_CHOOSE_RSP")]
	CmdVirtualTrainRoleCustomChooseRsp = 3521,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_EVO_REQ")]
	CmdVirtualTrainEvoReq = 3522,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_EVO_RSP")]
	CmdVirtualTrainEvoRsp = 3523,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_ITEM_DROP_NOTIFY")]
	CmdVirtualTrainItemDropNotify = 3524,
	[ProtoEnum(Name = "CMD_VIRTUAL_TRAIN_DUMMY")]
	CmdVirtualTrainDummy = 3549
}
