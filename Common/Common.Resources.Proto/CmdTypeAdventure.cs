using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeAdventure
{
	[ProtoEnum(Name = "GET_ADVENTURE_GROUP_REQ")]
	GetAdventureGroupReq = 3900,
	[ProtoEnum(Name = "GET_ADVENTURE_GROUP_RSP")]
	GetAdventureGroupRsp = 3901,
	[ProtoEnum(Name = "SELECT_ADVENTURE_QUEST_REQ")]
	SelectAdventureQuestReq = 3902,
	[ProtoEnum(Name = "SELECT_ADVENTURE_QUEST_RSP")]
	SelectAdventureQuestRsp = 3903,
	[ProtoEnum(Name = "TAKE_ADVENTURE_QUEST_REWARD_REQ")]
	TakeAdventureQuestRewardReq = 3904,
	[ProtoEnum(Name = "TAKE_ADVENTURE_QUEST_REWARD_RSP")]
	TakeAdventureQuestRewardRsp = 3905,
	[ProtoEnum(Name = "TAKE_ADVENTURE_LEVEL_REWARD_REQ")]
	TakeAdventureLevelRewardReq = 3908,
	[ProtoEnum(Name = "TAKE_ADVENTURE_LEVEL_REWARD_RSP")]
	TakeAdventureLevelRewardRsp = 3909,
	[ProtoEnum(Name = "CMD_CLAIM_SCOIN_REQ")]
	CmdClaimScoinReq = 3910,
	[ProtoEnum(Name = "CMD_CLAIM_SCOIN_RSP")]
	CmdClaimScoinRsp = 3911,
	[ProtoEnum(Name = "CMD_ADVENTURE_STORY_SWEEP_REQ")]
	CmdAdventureStorySweepReq = 3912,
	[ProtoEnum(Name = "CMD_ADVENTURE_STORY_SWEEP_RSP")]
	CmdAdventureStorySweepRsp = 3913,
	[ProtoEnum(Name = "CMD_GET_ADVENTURE_STORY_SWEEP_INFO_REQ")]
	CmdGetAdventureStorySweepInfoReq = 3914,
	[ProtoEnum(Name = "CMD_GET_ADVENTURE_STORY_SWEEP_INFO_RSP")]
	CmdGetAdventureStorySweepInfoRsp = 3915,
	[ProtoEnum(Name = "CMD_TAKE_ADVENTURE_STORY_SWEEP_REWARD_REQ")]
	CmdTakeAdventureStorySweepRewardReq = 3916,
	[ProtoEnum(Name = "CMD_TAKE_ADVENTURE_STORY_SWEEP_REWARD_RSP")]
	CmdTakeAdventureStorySweepRewardRsp = 3917,
	[ProtoEnum(Name = "CMD_TAKE_ADVENTURE_COMPENSATION_REQ")]
	CmdTakeAdventureCompensationReq = 3918,
	[ProtoEnum(Name = "CMD_TAKE_ADVENTURE_COMPENSATION_RSP")]
	CmdTakeAdventureCompensationRsp = 3919,
	[ProtoEnum(Name = "CMD_EDIT_ADVENTURE_ROOM_REQ")]
	CmdEditAdventureRoomReq = 3920,
	[ProtoEnum(Name = "CMD_EDIT_ADVENTURE_ROOM_RSP")]
	CmdEditAdventureRoomRsp = 3921,
	[ProtoEnum(Name = "CANCEL_ADVENTURE_QUEST_REQ")]
	CancelAdventureQuestReq = 3922,
	[ProtoEnum(Name = "CANCEL_ADVENTURE_QUEST_RSP")]
	CancelAdventureQuestRsp = 3923,
	[ProtoEnum(Name = "RESET_ADVENTURE_QUEST_REQ")]
	ResetAdventureQuestReq = 3924,
	[ProtoEnum(Name = "RESET_ADVENTURE_QUEST_RSP")]
	ResetAdventureQuestRsp = 3925
}
