using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeQavatarbattle
{
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_BEGIN_NOTIFY")]
	CmdQavatarBattleBeginNotify = 5550,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_SETTLE_NOTIFY")]
	CmdQavatarBattleSettleNotify = 5551,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_GET_LOBBY_INFO_REQ")]
	CmdQavatarBattleGetLobbyInfoReq = 5552,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_GET_LOBBY_INFO_RSP")]
	CmdQavatarBattleGetLobbyInfoRsp = 5553,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_DATA_NOTIFY")]
	CmdQavatarBattleDataNotify = 5600,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_ENTER_BATTLE_REQ")]
	CmdQavatarBattleEnterBattleReq = 5601,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_ENTER_BATTLE_RSP")]
	CmdQavatarBattleEnterBattleRsp = 5602,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_MOVE_NOTIFY")]
	CmdQavatarBattleMoveNotify = 5603,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_START_SKILL_REQ")]
	CmdQavatarBattleStartSkillReq = 5604,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_START_SKILL_RSP")]
	CmdQavatarBattleStartSkillRsp = 5605,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_START_SKILL_NOTIFY")]
	CmdQavatarBattleStartSkillNotify = 5606,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_END_SKILL_REQ")]
	CmdQavatarBattleEndSkillReq = 5607,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_END_SKILL_RSP")]
	CmdQavatarBattleEndSkillRsp = 5608,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_END_SKILL_NOTIFY")]
	CmdQavatarBattleEndSkillNotify = 5609,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_SKILL_TRIGGER_NOTIFY")]
	CmdQavatarBattleSkillTriggerNotify = 5610,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_COMPONENT_OP_NOTIFY")]
	CmdQavatarBattleComponentOpNotify = 5611,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_DISCARD_STIGMATA_REQ")]
	CmdQavatarBattleDiscardStigmataReq = 5612,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_DISCARD_STIGMATA_RSP")]
	CmdQavatarBattleDiscardStigmataRsp = 5613,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_GET_DATA_NOTIFY")]
	CmdQavatarBattleGetDataNotify = 5614,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_KILL_NOTIFY")]
	CmdQavatarBattleKillNotify = 5621,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_REFRESH_GADGET_FORECAST_NOTIFY")]
	CmdQavatarBattleRefreshGadgetForecastNotify = 5622,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_REFRESH_GADGET_GENERATE_NOTIFY")]
	CmdQavatarBattleRefreshGadgetGenerateNotify = 5623,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_END_BATTLE_NOTIFY")]
	CmdQavatarBattleEndBattleNotify = 5630,
	[ProtoEnum(Name = "CMD_QAVATAR_BATTLE_DUMMY")]
	CmdQavatarBattleDummy = 5649
}
