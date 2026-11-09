using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeQcandybattle
{
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_ENTER_BATTLE_REQ")]
	CmdQcandyBattleEnterBattleReq = 6570,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_ENTER_BATTLE_RSP")]
	CmdQcandyBattleEnterBattleRsp = 6571,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_DATA_NOTIFY")]
	CmdQcandyBattleDataNotify = 6572,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_END_NOTIFY")]
	CmdQcandyBattleEndNotify = 6573,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_MOVE_NOTIFY")]
	CmdQcandyBattleMoveNotify = 6574,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_TRIGGER_ENTITY_REQ")]
	CmdQcandyBattleTriggerEntityReq = 6580,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_TRIGGER_ENTITY_RSP")]
	CmdQcandyBattleTriggerEntityRsp = 6581,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_LIFE_TIMES_CHANGE_NOTIFY")]
	CmdQcandyBattleLifeTimesChangeNotify = 6582,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_ARRIVE_REQ")]
	CmdQcandyBattleArriveReq = 6583,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_ARRIVE_RSP")]
	CmdQcandyBattleArriveRsp = 6584,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_USE_SKILL_REQ")]
	CmdQcandyBattleUseSkillReq = 6585,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_USE_SKILL_RSP")]
	CmdQcandyBattleUseSkillRsp = 6586,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_PLAYER_USE_SKILL_NOTIFY")]
	CmdQcandyBattlePlayerUseSkillNotify = 6587,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_AVATAR_STATE_CLIENT_NOTIFY")]
	CmdQcandyBattleAvatarStateClientNotify = 6588,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_AVATAR_STATE_SERVER_NOTIFY")]
	CmdQcandyBattleAvatarStateServerNotify = 6589,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_SKILL_EFFECT_NOTIFY")]
	CmdQcandyBattleSkillEffectNotify = 6590,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_FINISH_BATTLE_NOTIFY")]
	CmdQcandyBattleFinishBattleNotify = 6591,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_LEAVE_BATTLE_REQ")]
	CmdQcandyBattleLeaveBattleReq = 6592,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_LEAVE_BATTLE_RSP")]
	CmdQcandyBattleLeaveBattleRsp = 6593,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_LEAVE_BATTLE_NOTIFY")]
	CmdQcandyBattleLeaveBattleNotify = 6594,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_START_TIMELINE_NOTIFY")]
	CmdQcandyBattleStartTimelineNotify = 6595,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_SKILL_USE_TIMES_CHANGE_NOTIFY")]
	CmdQcandyBattleSkillUseTimesChangeNotify = 6596,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_SEND_EMOJI_REQ")]
	CmdQcandyBattleSendEmojiReq = 6597,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_SEND_EMOJI_RSP")]
	CmdQcandyBattleSendEmojiRsp = 6598,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_SEND_EMOJI_NOTIFY")]
	CmdQcandyBattleSendEmojiNotify = 6599,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_GET_ACTIVITY_REQ")]
	CmdQcandyBattleGetActivityReq = 6601,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_GET_ACTIVITY_RSP")]
	CmdQcandyBattleGetActivityRsp = 6602,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_BEGIN_NOTIFY")]
	CmdQcandyBattleBeginNotify = 6603,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_CHOOSE_AVATAR_REQ")]
	CmdQcandyBattleChooseAvatarReq = 6604,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_CHOOSE_AVATAR_RSP")]
	CmdQcandyBattleChooseAvatarRsp = 6605,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_UNLOCK_AVATAR_REQ")]
	CmdQcandyBattleUnlockAvatarReq = 6606,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_UNLOCK_AVATAR_RSP")]
	CmdQcandyBattleUnlockAvatarRsp = 6607,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_SETTLE_BATTLE_NOTIFY")]
	CmdQcandyBattleSettleBattleNotify = 6608,
	[ProtoEnum(Name = "CMD_QCANDY_BATTLE_DUMMY")]
	CmdQcandyBattleDummy = 6649
}
