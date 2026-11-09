using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeGlobalwar
{
	[ProtoEnum(Name = "CMD_GET_GLOBAL_WAR_ACTIVITY_REQ")]
	CmdGetGlobalWarActivityReq = 3950,
	[ProtoEnum(Name = "CMD_GET_GLOBAL_WAR_ACTIVITY_RSP")]
	CmdGetGlobalWarActivityRsp = 3951,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_TAKE_POINT_REWARD_REQ")]
	CmdGlobalWarTakePointRewardReq = 3952,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_TAKE_POINT_REWARD_RSP")]
	CmdGlobalWarTakePointRewardRsp = 3953,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_ADD_CURRENCY_NOTIFY")]
	CmdGlobalWarAddCurrencyNotify = 3954,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_EXCHANGE_CURRENCY_REQ")]
	CmdGlobalWarExchangeCurrencyReq = 3955,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_EXCHANGE_CURRENCY_RSP")]
	CmdGlobalWarExchangeCurrencyRsp = 3956,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_GET_RANK_REQ")]
	CmdGlobalWarGetRankReq = 3957,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_GET_RANK_RSP")]
	CmdGlobalWarGetRankRsp = 3958,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_TAKE_SWEEP_REWARD_REQ")]
	CmdGlobalWarTakeSweepRewardReq = 3959,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_TAKE_SWEEP_REWARD_RSP")]
	CmdGlobalWarTakeSweepRewardRsp = 3960,
	[ProtoEnum(Name = "CMD_GLOBAL_WAR_DUMMY")]
	CmdGlobalWarDummy = 3999
}
