using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeBattlepass
{
	[ProtoEnum(Name = "CMD_GET_BATTLE_PASS_REQ")]
	CmdGetBattlePassReq = 3750,
	[ProtoEnum(Name = "CMD_GET_BATTLE_PASS_RSP")]
	CmdGetBattlePassRsp,
	[ProtoEnum(Name = "CMD_BUY_BATTLE_PASS_TICKET_REQ")]
	CmdBuyBattlePassTicketReq,
	[ProtoEnum(Name = "CMD_BUY_BATTLE_PASS_TICKET_RSP")]
	CmdBuyBattlePassTicketRsp,
	[ProtoEnum(Name = "CMD_TAKE_BATTLE_PASS_LEVEL_REWARD_REQ")]
	CmdTakeBattlePassLevelRewardReq,
	[ProtoEnum(Name = "CMD_TAKE_BATTLE_PASS_LEVEL_REWARD_RSP")]
	CmdTakeBattlePassLevelRewardRsp,
	[ProtoEnum(Name = "CMD_BUY_BATTLE_PASS_LEVEL_REQ")]
	CmdBuyBattlePassLevelReq,
	[ProtoEnum(Name = "CMD_BUY_BATTLE_PASS_LEVEL_RSP")]
	CmdBuyBattlePassLevelRsp,
	[ProtoEnum(Name = "CMD_TAKE_BATTLE_PASS_PHASE_EXP_REQ")]
	CmdTakeBattlePassPhaseExpReq,
	[ProtoEnum(Name = "CMD_TAKE_BATTLE_PASS_PHASE_EXP_RSP")]
	CmdTakeBattlePassPhaseExpRsp,
	[ProtoEnum(Name = "CMD_GET_BATTLE_PASS_THEME_REQ")]
	CmdGetBattlePassThemeReq,
	[ProtoEnum(Name = "CMD_GET_BATTLE_PASS_THEME_RSP")]
	CmdGetBattlePassThemeRsp,
	[ProtoEnum(Name = "CMD_TAKE_BATTLE_PASS_THEME_LEVEL_REWARD_REQ")]
	CmdTakeBattlePassThemeLevelRewardReq,
	[ProtoEnum(Name = "CMD_TAKE_BATTLE_PASS_THEME_LEVEL_REWARD_RSP")]
	CmdTakeBattlePassThemeLevelRewardRsp,
	[ProtoEnum(Name = "CMD_BUY_BATTLE_PASS_THEME_LEVEL_REQ")]
	CmdBuyBattlePassThemeLevelReq,
	[ProtoEnum(Name = "CMD_BUY_BATTLE_PASS_THEME_LEVEL_RSP")]
	CmdBuyBattlePassThemeLevelRsp,
	[ProtoEnum(Name = "CMD_BATTLE_PASS_THEME_EXP_TRANSFORM_NOTIFY")]
	CmdBattlePassThemeExpTransformNotify
}
