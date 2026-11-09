using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeKingdom
{
	[ProtoEnum(Name = "CMD_GET_KINGDOM_WAR_REQ")]
	CmdGetKingdomWarReq = 3050,
	[ProtoEnum(Name = "CMD_GET_KINGDOM_WAR_RSP")]
	CmdGetKingdomWarRsp = 3051,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_MOVE_REQ")]
	CmdKingdomWarMoveReq = 3052,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_MOVE_RSP")]
	CmdKingdomWarMoveRsp = 3053,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_VOTE_REQ")]
	CmdKingdomWarVoteReq = 3054,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_VOTE_RSP")]
	CmdKingdomWarVoteRsp = 3055,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_GET_RANK_REQ")]
	CmdKingdomWarGetRankReq = 3058,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_GET_RANK_RSP")]
	CmdKingdomWarGetRankRsp = 3059,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_TAKE_DAILY_REWARD_REQ")]
	CmdKingdomWarTakeDailyRewardReq = 3060,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_TAKE_DAILY_REWARD_RSP")]
	CmdKingdomWarTakeDailyRewardRsp = 3061,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_EXCHANGE_CURRENCY_REQ")]
	CmdKingdomWarExchangeCurrencyReq = 3062,
	[ProtoEnum(Name = "CMD_KIGNDOM_WAR_EXCHANGE_CURRENCY_RSP")]
	CmdKigndomWarExchangeCurrencyRsp = 3063,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_GET_POINT_RANK_REQ")]
	CmdKingdomWarGetPointRankReq = 3064,
	[ProtoEnum(Name = "CMD_KINGDOM_WAR_GET_POINT_RANK_RSP")]
	CmdKingdomWarGetPointRankRsp = 3065
}
