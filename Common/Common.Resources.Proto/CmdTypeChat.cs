using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeChat
{
	[ProtoEnum(Name = "CMD_CREATE_CHATGROUP_REQ")]
	CmdCreateChatgroupReq = 2200,
	[ProtoEnum(Name = "CMD_CREATE_CHATGROUP_RSP")]
	CmdCreateChatgroupRsp = 2201,
	[ProtoEnum(Name = "CMD_INVITE_CHATGROUP_REQ")]
	CmdInviteChatgroupReq = 2202,
	[ProtoEnum(Name = "CMD_INVITE_CHATGROUP_RSP")]
	CmdInviteChatgroupRsp = 2203,
	[ProtoEnum(Name = "CMD_GET_CHATGROUP_LIST_REQ")]
	CmdGetChatgroupListReq = 2204,
	[ProtoEnum(Name = "CMD_GET_CHATGROUP_LIST_RSP")]
	CmdGetChatgroupListRsp = 2205,
	[ProtoEnum(Name = "CMD_SET_CHATGROUP_REQ")]
	CmdSetChatgroupReq = 2206,
	[ProtoEnum(Name = "CMD_SET_CHATGROUP_RSP")]
	CmdSetChatgroupRsp = 2207,
	[ProtoEnum(Name = "CMD_LEAVE_CHATGROUP_REQ")]
	CmdLeaveChatgroupReq = 2208,
	[ProtoEnum(Name = "CMD_LEAVE_CHATGROUP_RSP")]
	CmdLeaveChatgroupRsp = 2209,
	[ProtoEnum(Name = "CMD_CHATGROUP_SYSTEM_NOTIFY")]
	CmdChatgroupSystemNotify = 2211,
	[ProtoEnum(Name = "CMD_KICK_CHATGROUP_MEMBER_REQ")]
	CmdKickChatgroupMemberReq = 2212,
	[ProtoEnum(Name = "CMD_KICK_CHATGROUP_MEMBER_RSP")]
	CmdKickChatgroupMemberRsp = 2213,
	[ProtoEnum(Name = "CMD_DEAL_CHATGROUP_INVITE_REQ")]
	CmdDealChatgroupInviteReq = 2214,
	[ProtoEnum(Name = "CMD_DEAL_CHATGROUP_INVITE_RSP")]
	CmdDealChatgroupInviteRsp = 2215,
	[ProtoEnum(Name = "CMD_INVITE_TO_MULTI_CHATGROUP_REQ")]
	CmdInviteToMultiChatgroupReq = 2216,
	[ProtoEnum(Name = "CMD_INVITE_TO_MULTI_CHATGROUP_RSP")]
	CmdInviteToMultiChatgroupRsp = 2217,
	[ProtoEnum(Name = "CMD_GET_CHATGROUP_HISTORY_CHAT_MSG_REQ")]
	CmdGetChatgroupHistoryChatMsgReq = 2218,
	[ProtoEnum(Name = "CMD_GET_CHATGROUP_HISTORY_CHAT_MSG_RSP")]
	CmdGetChatgroupHistoryChatMsgRsp = 2219,
	[ProtoEnum(Name = "CMD_ENTER_WORLD_CHATROOM_REQ")]
	CmdEnterWorldChatroomReq = 2231,
	[ProtoEnum(Name = "CMD_ENTER_WORLD_CHATROOM_RSP")]
	CmdEnterWorldChatroomRsp = 2232,
	[ProtoEnum(Name = "CMD_LEAVE_CHATROOM_NOTIFY")]
	CmdLeaveChatroomNotify = 2233,
	[ProtoEnum(Name = "CMD_SEND_CHAT_MSG_NOTIFY")]
	CmdSendChatMsgNotify = 2234,
	[ProtoEnum(Name = "CMD_RECV_CHAT_MSG_NOTIFY")]
	CmdRecvChatMsgNotify = 2235,
	[ProtoEnum(Name = "CMD_RECV_OFFLINE_PRIVATE_CHAT_MSG_NOTIFY")]
	CmdRecvOfflinePrivateChatMsgNotify = 2236,
	[ProtoEnum(Name = "CMD_GET_HISTORY_PRIVATE_CHAT_MSG_REQ")]
	CmdGetHistoryPrivateChatMsgReq = 2237,
	[ProtoEnum(Name = "CMD_GET_HISTORY_PRIVATE_CHAT_MSG_RSP")]
	CmdGetHistoryPrivateChatMsgRsp = 2238,
	[ProtoEnum(Name = "CMD_CHAT_CLOSED_NOTIFY")]
	CmdChatClosedNotify = 2239
}
