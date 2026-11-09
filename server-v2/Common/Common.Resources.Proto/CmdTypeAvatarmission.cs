using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeAvatarmission
{
	[ProtoEnum(Name = "CMD_GET_AVATAR_MISSION_ACTIVITY_REQ")]
	CmdGetAvatarMissionActivityReq = 3000,
	[ProtoEnum(Name = "CMD_GET_AVATAR_MISSION_ACTIVITY_RSP")]
	CmdGetAvatarMissionActivityRsp,
	[ProtoEnum(Name = "CMD_CHOOSE_AVATAR_MISSION_AVATAR_REQ")]
	CmdChooseAvatarMissionAvatarReq,
	[ProtoEnum(Name = "CMD_CHOOSE_AVATAR_MISSION_AVATAR_RSP")]
	CmdChooseAvatarMissionAvatarRsp,
	[ProtoEnum(Name = "CMD_TAKE_AVATAR_MISSION_DAILY_REWARD_REQ")]
	CmdTakeAvatarMissionDailyRewardReq,
	[ProtoEnum(Name = "CMD_TAKE_AVATAR_MISSION_DAILY_REWARD_RSP")]
	CmdTakeAvatarMissionDailyRewardRsp,
	[ProtoEnum(Name = "CMD_TAKE_AVATAR_MISSION_PHASE_REWARD_REQ")]
	CmdTakeAvatarMissionPhaseRewardReq,
	[ProtoEnum(Name = "CMD_TAKE_AVATAR_MISSION_PHASE_REWARD_RSP")]
	CmdTakeAvatarMissionPhaseRewardRsp,
	[ProtoEnum(Name = "CMD_RESET_AVATAR_MISSION_AVATAR_REQ")]
	CmdResetAvatarMissionAvatarReq,
	[ProtoEnum(Name = "CMD_RESET_AVATAR_MISSION_AVATAR_RSP")]
	CmdResetAvatarMissionAvatarRsp
}
