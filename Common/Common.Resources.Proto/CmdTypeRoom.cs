using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeRoom
{
	[ProtoEnum(Name = "CMD_GET_ROOM_DATA_REQ")]
	CmdGetRoomDataReq = 3150,
	[ProtoEnum(Name = "CMD_GET_ROOM_DATA_RSP")]
	CmdGetRoomDataRsp,
	[ProtoEnum(Name = "CMD_SYNC_ROOM_DATA_NOTIFY")]
	CmdSyncRoomDataNotify,
	[ProtoEnum(Name = "CMD_CREATE_ROOM_REQ")]
	CmdCreateRoomReq,
	[ProtoEnum(Name = "CMD_CREATE_ROOM_RSP")]
	CmdCreateRoomRsp,
	[ProtoEnum(Name = "CMD_ENTER_ROOM_REQ")]
	CmdEnterRoomReq,
	[ProtoEnum(Name = "CMD_ENTER_ROOM_RSP")]
	CmdEnterRoomRsp,
	[ProtoEnum(Name = "CMD_EXIT_ROOM_REQ")]
	CmdExitRoomReq,
	[ProtoEnum(Name = "CMD_EXIT_ROOM_RSP")]
	CmdExitRoomRsp,
	[ProtoEnum(Name = "CMD_EXIT_ROOM_NOTIFY")]
	CmdExitRoomNotify,
	[ProtoEnum(Name = "CMD_INVITE_ROOM_REQ")]
	CmdInviteRoomReq,
	[ProtoEnum(Name = "CMD_INVITE_ROOM_RSP")]
	CmdInviteRoomRsp,
	[ProtoEnum(Name = "CMD_ROOM_INVITATION_NOTIFY")]
	CmdRoomInvitationNotify,
	[ProtoEnum(Name = "CMD_SEND_ROOM_CHAT_MSG_NOTIFY")]
	CmdSendRoomChatMsgNotify,
	[ProtoEnum(Name = "CMD_RECV_ROOM_CHAT_MSG_NOTIFY")]
	CmdRecvRoomChatMsgNotify,
	[ProtoEnum(Name = "CMD_START_ROOM_MATCH_REQ")]
	CmdStartRoomMatchReq,
	[ProtoEnum(Name = "CMD_START_ROOM_MATCH_RSP")]
	CmdStartRoomMatchRsp,
	[ProtoEnum(Name = "CMD_CANCEL_ROOM_MATCH_REQ")]
	CmdCancelRoomMatchReq,
	[ProtoEnum(Name = "CMD_CANCEL_ROOM_MATCH_RSP")]
	CmdCancelRoomMatchRsp,
	[ProtoEnum(Name = "CMD_SYNC_ROOM_MATCH_DATA_NOTIFY")]
	CmdSyncRoomMatchDataNotify,
	[ProtoEnum(Name = "CMD_ROOM_MATCH_TIMEOUT_NOTIFY")]
	CmdRoomMatchTimeoutNotify,
	[ProtoEnum(Name = "CMD_UPDATE_ROOM_INFO_REQ")]
	CmdUpdateRoomInfoReq,
	[ProtoEnum(Name = "CMD_UPDATE_ROOM_INFO_RSP")]
	CmdUpdateRoomInfoRsp,
	[ProtoEnum(Name = "CMD_ROOM_SWITCH_MEMBER_STATUS_REQ")]
	CmdRoomSwitchMemberStatusReq,
	[ProtoEnum(Name = "CMD_ROOM_SWITCH_MEMBER_STATUS_RSP")]
	CmdRoomSwitchMemberStatusRsp,
	[ProtoEnum(Name = "CMD_ROOM_KICK_MEMBER_REQ")]
	CmdRoomKickMemberReq,
	[ProtoEnum(Name = "CMD_ROOM_KICK_MEMBER_RSP")]
	CmdRoomKickMemberRsp,
	[ProtoEnum(Name = "CMD_ROOM_TRANSFER_LEADER_REQ")]
	CmdRoomTransferLeaderReq,
	[ProtoEnum(Name = "CMD_ROOM_TRANSFER_LEADER_RSP")]
	CmdRoomTransferLeaderRsp,
	[ProtoEnum(Name = "CMD_ROOM_MEMBER_SET_VOICE_ID_REQ")]
	CmdRoomMemberSetVoiceIdReq,
	[ProtoEnum(Name = "CMD_ROOM_MEMBER_SET_VOICE_ID_RSP")]
	CmdRoomMemberSetVoiceIdRsp,
	[ProtoEnum(Name = "CMD_ROOM_GET_STATUS_REQ")]
	CmdRoomGetStatusReq,
	[ProtoEnum(Name = "CMD_ROOM_GET_STATUS_RSP")]
	CmdRoomGetStatusRsp,
	[ProtoEnum(Name = "CMD_DISMISS_ROOM_REQ")]
	CmdDismissRoomReq,
	[ProtoEnum(Name = "CMD_DISMISS_ROOM_RSP")]
	CmdDismissRoomRsp
}
