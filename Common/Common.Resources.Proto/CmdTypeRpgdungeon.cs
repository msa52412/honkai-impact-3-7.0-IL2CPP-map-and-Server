using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeRpgdungeon
{
	[ProtoEnum(Name = "CMD_RPG_DUNGEON_GET_ACTIVITY_REQ")]
	CmdRpgDungeonGetActivityReq = 5500,
	[ProtoEnum(Name = "CMD_RPG_DUNGEON_GET_ACTIVITY_RSP")]
	CmdRpgDungeonGetActivityRsp,
	[ProtoEnum(Name = "CMD_RPG_DUNGEON_SELECT_QUEST_REQ")]
	CmdRpgDungeonSelectQuestReq,
	[ProtoEnum(Name = "CMD_RPG_DUNGEON_SELECT_QUEST_RSP")]
	CmdRpgDungeonSelectQuestRsp,
	[ProtoEnum(Name = "CMD_RPG_DUNGEON_GET_CANDIDATE_BUFF_REQ")]
	CmdRpgDungeonGetCandidateBuffReq,
	[ProtoEnum(Name = "CMD_RPG_DUNGEON_GET_CANDIDATE_BUFF_RSP")]
	CmdRpgDungeonGetCandidateBuffRsp,
	[ProtoEnum(Name = "CMD_RPG_DUNGEON_SELECT_BUFF_REQ")]
	CmdRpgDungeonSelectBuffReq,
	[ProtoEnum(Name = "CMD_RPG_DUNGEON_SELECT_BUFF_RSP")]
	CmdRpgDungeonSelectBuffRsp
}
