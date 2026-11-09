using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeGlobalexplore
{
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_GET_ACTIVITY_REQ")]
	CmdGlobalExploreGetActivityReq = 6900,
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_GET_ACTIVITY_RSP")]
	CmdGlobalExploreGetActivityRsp,
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_NOTIFY")]
	CmdGlobalExploreNotify,
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_MOVE_REQ")]
	CmdGlobalExploreMoveReq,
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_MOVE_RSP")]
	CmdGlobalExploreMoveRsp,
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_INTERACT_REQ")]
	CmdGlobalExploreInteractReq,
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_INTERACT_RSP")]
	CmdGlobalExploreInteractRsp,
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_ACTION_REQ")]
	CmdGlobalExploreActionReq,
	[ProtoEnum(Name = "CMD_GLOBAL_EXPLORE_ACTION_RSP")]
	CmdGlobalExploreActionRsp
}
