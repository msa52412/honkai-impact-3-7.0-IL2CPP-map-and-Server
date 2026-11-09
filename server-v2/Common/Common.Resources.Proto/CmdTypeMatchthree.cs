using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeMatchthree
{
	[ProtoEnum(Name = "CMD_MATCH_THREE_GET_ACTIVITY_REQ")]
	CmdMatchThreeGetActivityReq = 7601,
	[ProtoEnum(Name = "CMD_MATCH_THREE_GET_ACTIVITY_RSP")]
	CmdMatchThreeGetActivityRsp,
	[ProtoEnum(Name = "CMD_MATCH_THREE_ROLE_LEVEL_UP_REQ")]
	CmdMatchThreeRoleLevelUpReq,
	[ProtoEnum(Name = "CMD_MATCH_THREE_ROLE_LEVEL_UP_RSP")]
	CmdMatchThreeRoleLevelUpRsp,
	[ProtoEnum(Name = "CMD_MATCH_THREE_GEM_GACHA_DISPLAY_REQ")]
	CmdMatchThreeGemGachaDisplayReq,
	[ProtoEnum(Name = "CMD_MATCH_THREE_GEM_GACHA_DISPLAY_RSP")]
	CmdMatchThreeGemGachaDisplayRsp,
	[ProtoEnum(Name = "CMD_MATCH_THREE_GEM_GACHA_REQ")]
	CmdMatchThreeGemGachaReq,
	[ProtoEnum(Name = "CMD_MATCH_THREE_GEM_GACHA_RSP")]
	CmdMatchThreeGemGachaRsp,
	[ProtoEnum(Name = "CMD_MATCH_THREE_GET_RANK_REQ")]
	CmdMatchThreeGetRankReq,
	[ProtoEnum(Name = "CMD_MATCH_THREE_GET_RANK_RSP")]
	CmdMatchThreeGetRankRsp,
	[ProtoEnum(Name = "CMD_MATCH_THREE_ROOM_BEGIN_REQ")]
	CmdMatchThreeRoomBeginReq,
	[ProtoEnum(Name = "CMD_MATCH_THREE_ROOM_BEGIN_RSP")]
	CmdMatchThreeRoomBeginRsp,
	[ProtoEnum(Name = "CMD_MATCH_THREE_ROOM_END_REQ")]
	CmdMatchThreeRoomEndReq,
	[ProtoEnum(Name = "CMD_MATCH_THREE_ROOM_END_RSP")]
	CmdMatchThreeRoomEndRsp
}
