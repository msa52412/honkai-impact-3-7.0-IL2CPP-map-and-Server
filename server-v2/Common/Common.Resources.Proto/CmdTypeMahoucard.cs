using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeMahoucard
{
	[ProtoEnum(Name = "CMD_MAHOU_CARD_GET_ACTIVITY_REQ")]
	CmdMahouCardGetActivityReq = 7900,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_GET_ACTIVITY_RSP")]
	CmdMahouCardGetActivityRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_ROLE_LEVEL_UP_REQ")]
	CmdMahouCardRoleLevelUpReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_ROLE_LEVEL_UP_RSP")]
	CmdMahouCardRoleLevelUpRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_ENTER_SITE_REQ")]
	CmdMahouCardEnterSiteReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_ENTER_SITE_RSP")]
	CmdMahouCardEnterSiteRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_ENTER_ROOM_REQ")]
	CmdMahouCardEnterRoomReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_ENTER_ROOM_RSP")]
	CmdMahouCardEnterRoomRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_EXIT_ROOM_REQ")]
	CmdMahouCardExitRoomReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_EXIT_ROOM_RSP")]
	CmdMahouCardExitRoomRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_GET_CUR_SITE_INFO_REQ")]
	CmdMahouCardGetCurSiteInfoReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_GET_CUR_SITE_INFO_RSP")]
	CmdMahouCardGetCurSiteInfoRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_HAND_CARD_CHANGE_NOTIFY")]
	CmdMahouCardHandCardChangeNotify,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_EXCHANGE_CARD_REQ")]
	CmdMahouCardExchangeCardReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_EXCHANGE_CARD_RSP")]
	CmdMahouCardExchangeCardRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_DISCARD_CARD_REQ")]
	CmdMahouCardDiscardCardReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_DISCARD_CARD_RSP")]
	CmdMahouCardDiscardCardRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_LEVEL_UP_CARD_REQ")]
	CmdMahouCardLevelUpCardReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_LEVEL_UP_CARD_RSP")]
	CmdMahouCardLevelUpCardRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_SET_BATTLE_CARD_REQ")]
	CmdMahouCardSetBattleCardReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_SET_BATTLE_CARD_RSP")]
	CmdMahouCardSetBattleCardRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_RECALL_BATTLE_CARD_REQ")]
	CmdMahouCardRecallBattleCardReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_RECALL_BATTLE_CARD_RSP")]
	CmdMahouCardRecallBattleCardRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_SELECT_PROPHESY_REQ")]
	CmdMahouCardSelectProphesyReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_SELECT_PROPHESY_RSP")]
	CmdMahouCardSelectProphesyRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_EXIT_SITE_REQ")]
	CmdMahouCardExitSiteReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_EXIT_SITE_RSP")]
	CmdMahouCardExitSiteRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_CHOOSE_EVENT_OPTION_REQ")]
	CmdMahouCardChooseEventOptionReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_CHOOSE_EVENT_OPTION_RSP")]
	CmdMahouCardChooseEventOptionRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_EVENT_NOTIFY")]
	CmdMahouCardEventNotify,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_BATTLE_BEGIN_REQ")]
	CmdMahouCardBattleBeginReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_BATTLE_BEGIN_RSP")]
	CmdMahouCardBattleBeginRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_BATTLE_END_REQ")]
	CmdMahouCardBattleEndReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_BATTLE_END_RSP")]
	CmdMahouCardBattleEndRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_UNLOCK_DAILY_SITE_REQ")]
	CmdMahouCardUnlockDailySiteReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_UNLOCK_DAILY_SITE_RSP")]
	CmdMahouCardUnlockDailySiteRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_GET_DAILY_SITE_RANK_REQ")]
	CmdMahouCardGetDailySiteRankReq,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_GET_DAILY_SITE_RANK_RSP")]
	CmdMahouCardGetDailySiteRankRsp,
	[ProtoEnum(Name = "CMD_MAHOU_CARD_OVERDRAWN_NOTIFY")]
	CmdMahouCardOverdrawnNotify
}
