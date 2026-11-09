using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypePvz
{
	[ProtoEnum(Name = "CMD_PVZ_GET_ACTIVITY_REQ")]
	CmdPvzGetActivityReq = 6550,
	[ProtoEnum(Name = "CMD_PVZ_GET_ACTIVITY_RSP")]
	CmdPvzGetActivityRsp,
	[ProtoEnum(Name = "CMD_PVZ_BATTLE_BEGIN_REQ")]
	CmdPvzBattleBeginReq,
	[ProtoEnum(Name = "CMD_PVZ_BATTLE_BEGIN_RSP")]
	CmdPvzBattleBeginRsp,
	[ProtoEnum(Name = "CMD_PVZ_BATTLE_END_REQ")]
	CmdPvzBattleEndReq,
	[ProtoEnum(Name = "CMD_PVZ_BATTLE_END_RSP")]
	CmdPvzBattleEndRsp,
	[ProtoEnum(Name = "CMD_PVZ_QAVATAR_LEVEL_UP_REQ")]
	CmdPvzQavatarLevelUpReq,
	[ProtoEnum(Name = "CMD_PVZ_QAVATAR_LEVEL_UP_RSP")]
	CmdPvzQavatarLevelUpRsp
}
