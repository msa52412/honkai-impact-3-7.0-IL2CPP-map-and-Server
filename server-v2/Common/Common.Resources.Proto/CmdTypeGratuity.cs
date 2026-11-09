using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeGratuity
{
	[ProtoEnum(Name = "CMD_GET_GRATUITY_ACTIVITY_REQ")]
	CmdGetGratuityActivityReq = 4100,
	[ProtoEnum(Name = "CMD_GET_GRATUITY_ACTIVITY_RSP")]
	CmdGetGratuityActivityRsp = 4101,
	[ProtoEnum(Name = "CMD_ACCEPT_GRATUITY_STAGE_REQ")]
	CmdAcceptGratuityStageReq = 4102,
	[ProtoEnum(Name = "CMD_ACCEPT_GRATUITY_STAGE_RSP")]
	CmdAcceptGratuityStageRsp = 4103,
	[ProtoEnum(Name = "CMD_ASSIST_GRATUITY_STAGE_REQ")]
	CmdAssistGratuityStageReq = 4104,
	[ProtoEnum(Name = "CMD_ASSIST_GRATUITY_STAGE_RSP")]
	CmdAssistGratuityStageRsp = 4105,
	[ProtoEnum(Name = "CMD_CANCEL_GRATUITY_STAGE_REQ")]
	CmdCancelGratuityStageReq = 4106,
	[ProtoEnum(Name = "CMD_CANCEL_GRATUITY_STAGE_RSP")]
	CmdCancelGratuityStageRsp = 4107,
	[ProtoEnum(Name = "CMD_GET_GRATUITY_BATTLE_HISTORY_REQ")]
	CmdGetGratuityBattleHistoryReq = 4108,
	[ProtoEnum(Name = "CMD_GET_GRATUITY_BATTLE_HISTORY_RSP")]
	CmdGetGratuityBattleHistoryRsp = 4109,
	[ProtoEnum(Name = "CMD_SYNC_GRATUITY_STAGE_REQ")]
	CmdSyncGratuityStageReq = 4112,
	[ProtoEnum(Name = "CMD_SYNC_GRATUITY_STAGE_RSP")]
	CmdSyncGratuityStageRsp = 4113,
	[ProtoEnum(Name = "CMD_PUBLISH_GRATUITY_STAGE_REQ")]
	CmdPublishGratuityStageReq = 4114,
	[ProtoEnum(Name = "CMD_PUBLISH_GRATUITY_STAGE_RSP")]
	CmdPublishGratuityStageRsp = 4115,
	[ProtoEnum(Name = "CMD_GET_GRATUITY_STAGE_LIST_REQ")]
	CmdGetGratuityStageListReq = 4116,
	[ProtoEnum(Name = "CMD_GET_GRATUITY_STAGE_LIST_RSP")]
	CmdGetGratuityStageListRsp = 4117,
	[ProtoEnum(Name = "CMD_REJECT_GRATUITY_STAGE_REQ")]
	CmdRejectGratuityStageReq = 4118,
	[ProtoEnum(Name = "CMD_REJECT_GRATUITY_STAGE_RSP")]
	CmdRejectGratuityStageRsp = 4119,
	[ProtoEnum(Name = "CMD_GRATUITY_STAGE_INVITE_NOTIFY")]
	CmdGratuityStageInviteNotify = 4120,
	[ProtoEnum(Name = "CMD_GRATUITY_STAGE_FINISH_NOTIFY")]
	CmdGratuityStageFinishNotify = 4121
}
