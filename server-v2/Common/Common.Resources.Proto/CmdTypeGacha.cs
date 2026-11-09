using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeGacha
{
	[ProtoEnum(Name = "CMD_GACHA_REQ")]
	CmdGachaReq = 4700,
	[ProtoEnum(Name = "CMD_GACHA_RSP")]
	CmdGachaRsp = 4701,
	[ProtoEnum(Name = "CMD_GET_GACHA_DISPLAY_REQ")]
	CmdGetGachaDisplayReq = 4702,
	[ProtoEnum(Name = "CMD_GET_GACHA_DISPLAY_RSP")]
	CmdGetGachaDisplayRsp = 4703,
	[ProtoEnum(Name = "CMD_BUY_GACHA_TICKET_REQ")]
	CmdBuyGachaTicketReq = 4704,
	[ProtoEnum(Name = "CMD_BUY_GACHA_TICKET_RSP")]
	CmdBuyGachaTicketRsp = 4705,
	[ProtoEnum(Name = "CMD_GET_GACHA_LOG_REQ")]
	CmdGetGachaLogReq = 4706,
	[ProtoEnum(Name = "CMD_GET_GACHA_LOG_RSP")]
	CmdGetGachaLogRsp = 4707,
	[ProtoEnum(Name = "CMD_GET_GACHA_PROB_REQ")]
	CmdGetGachaProbReq = 4708,
	[ProtoEnum(Name = "CMD_GET_GACHA_PROB_RSP")]
	CmdGetGachaProbRsp = 4709,
	[ProtoEnum(Name = "CMD_WISH_WELL_REQ")]
	CmdWishWellReq = 4710,
	[ProtoEnum(Name = "CMD_WISH_WELL_RSP")]
	CmdWishWellRsp = 4711,
	[ProtoEnum(Name = "CMD_BUY_CONTROLLABLE_BOX_GACHA_TICKET_REQ")]
	CmdBuyControllableBoxGachaTicketReq = 4712,
	[ProtoEnum(Name = "CMD_BUY_CONTROLLABLE_BOX_GACHA_TICKET_RSP")]
	CmdBuyControllableBoxGachaTicketRsp = 4713,
	[ProtoEnum(Name = "CMD_TAKE_GACHA_EX_REWARD_REQ")]
	CmdTakeGachaExRewardReq = 4714,
	[ProtoEnum(Name = "CMD_TAKE_GACHA_EX_REWARD_RSP")]
	CmdTakeGachaExRewardRsp = 4715,
	[ProtoEnum(Name = "CMD_CHOOSE_OPTIONAL_GACHA_REQ")]
	CmdChooseOptionalGachaReq = 4716,
	[ProtoEnum(Name = "CMD_CHOOSE_OPTIONAL_GACHA_RSP")]
	CmdChooseOptionalGachaRsp = 4717,
	[ProtoEnum(Name = "CMD_SELECT_CONTROLLABLE_BOX_GACHA_SELECTABLE_GROUP_ITEM_REQ")]
	CmdSelectControllableBoxGachaSelectableGroupItemReq = 4718,
	[ProtoEnum(Name = "CMD_SELECT_CONTROLLABLE_BOX_GACHA_SELECTABLE_GROUP_ITEM_RSP")]
	CmdSelectControllableBoxGachaSelectableGroupItemRsp = 4719,
	[ProtoEnum(Name = "CMD_UNIQUE_GACHA_UPDATE_ITEM_POOL_REQ")]
	CmdUniqueGachaUpdateItemPoolReq = 4722,
	[ProtoEnum(Name = "CMD_UNIQUE_GACHA_UPDATE_ITEM_POOL_RSP")]
	CmdUniqueGachaUpdateItemPoolRsp = 4723
}
