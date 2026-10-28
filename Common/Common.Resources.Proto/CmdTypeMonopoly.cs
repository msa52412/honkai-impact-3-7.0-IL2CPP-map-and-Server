using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeMonopoly
{
	[ProtoEnum(Name = "CMD_GET_MONOPOLY_ACTIVITY_REQ")]
	CmdGetMonopolyActivityReq = 3850,
	[ProtoEnum(Name = "CMD_GET_MONOPOLY_ACTIVITY_RSP")]
	CmdGetMonopolyActivityRsp = 3851,
	[ProtoEnum(Name = "CMD_MONOPOLY_THROW_DICE_REQ")]
	CmdMonopolyThrowDiceReq = 3852,
	[ProtoEnum(Name = "CMD_MONOPOLY_THROW_DICE_RSP")]
	CmdMonopolyThrowDiceRsp = 3853,
	[ProtoEnum(Name = "CMD_MONOPOLY_BUILDING_CONSTRUCT_REQ")]
	CmdMonopolyBuildingConstructReq = 3854,
	[ProtoEnum(Name = "CMD_MONOPOLY_BUILDING_CONSTRUCT_RSP")]
	CmdMonopolyBuildingConstructRsp = 3855,
	[ProtoEnum(Name = "CMD_MONOPOLY_END_ROUND_REQ")]
	CmdMonopolyEndRoundReq = 3856,
	[ProtoEnum(Name = "CMD_MONOPOLY_END_ROUND_RSP")]
	CmdMonopolyEndRoundRsp = 3857,
	[ProtoEnum(Name = "CMD_MONOPOLY_USE_ITEM_REQ")]
	CmdMonopolyUseItemReq = 3858,
	[ProtoEnum(Name = "CMD_MONOPOLY_USE_ITEM_RSP")]
	CmdMonopolyUseItemRsp = 3859,
	[ProtoEnum(Name = "CMD_MONOPOLY_REWARD_NOTIFY")]
	CmdMonopolyRewardNotify = 3860,
	[ProtoEnum(Name = "CMD_MONOPOLY_BUY_GOODS_REQ")]
	CmdMonopolyBuyGoodsReq = 3861,
	[ProtoEnum(Name = "CMD_MONOPOLY_BUY_GOODS_RSP")]
	CmdMonopolyBuyGoodsRsp = 3862,
	[ProtoEnum(Name = "CMD_MONOPOLY_SKIP_BATTLE_REQ")]
	CmdMonopolySkipBattleReq = 3863,
	[ProtoEnum(Name = "CMD_MONOPOLY_SKIP_BATTLE_RSP")]
	CmdMonopolySkipBattleRsp = 3864,
	[ProtoEnum(Name = "CMD_MONOPOLY_RESET_FLOOR_REQ")]
	CmdMonopolyResetFloorReq = 3865,
	[ProtoEnum(Name = "CMD_MONOPOLY_RESET_FLOOR_RSP")]
	CmdMonopolyResetFloorRsp = 3866,
	[ProtoEnum(Name = "CMD_MONOPOLY_ADD_MONSTER_REQ")]
	CmdMonopolyAddMonsterReq = 3867,
	[ProtoEnum(Name = "CMD_MONOPOLY_ADD_MONSTER_RSP")]
	CmdMonopolyAddMonsterRsp = 3868,
	[ProtoEnum(Name = "CMD_EXCHANGE_RAFFLE_MATERIAL_REQ")]
	CmdExchangeRaffleMaterialReq = 3890,
	[ProtoEnum(Name = "CMD_EXCHANGE_RAFFLE_MATERIAL_RSP")]
	CmdExchangeRaffleMaterialRsp = 3891,
	[ProtoEnum(Name = "CMD_TAKE_RAFFLE_REWARD_REQ")]
	CmdTakeRaffleRewardReq = 3894,
	[ProtoEnum(Name = "CMD_TAKE_RAFFLE_REWARD_RSP")]
	CmdTakeRaffleRewardRsp = 3895,
	[ProtoEnum(Name = "CMD_DRAW_RAFFLE_TICKET_REQ")]
	CmdDrawRaffleTicketReq = 3896,
	[ProtoEnum(Name = "CMD_DRAW_RAFFLE_TICKET_RSP")]
	CmdDrawRaffleTicketRsp = 3897,
	[ProtoEnum(Name = "CMD_GET_RAFFLE_ACTIVITY_REQ")]
	CmdGetRaffleActivityReq = 3898,
	[ProtoEnum(Name = "CMD_GET_RAFFLE_ACTIVITY_RSP")]
	CmdGetRaffleActivityRsp = 3899
}
