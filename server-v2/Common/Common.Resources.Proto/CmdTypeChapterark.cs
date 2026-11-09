using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeChapterark
{
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_GET_DATA_REQ")]
	CmdChapterArkGetDataReq = 7950,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_GET_DATA_RSP")]
	CmdChapterArkGetDataRsp,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_ROLE_LEVEL_UP_REQ")]
	CmdChapterArkRoleLevelUpReq,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_ROLE_LEVEL_UP_RSP")]
	CmdChapterArkRoleLevelUpRsp,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_SKILL_UNLOCK_REQ")]
	CmdChapterArkSkillUnlockReq,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_SKILL_UNLOCK_RSP")]
	CmdChapterArkSkillUnlockRsp,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_SKILL_LEVEL_UP_REQ")]
	CmdChapterArkSkillLevelUpReq,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_SKILL_LEVEL_UP_RSP")]
	CmdChapterArkSkillLevelUpRsp,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_TAKE_REWARD_LINE_REWARD_REQ")]
	CmdChapterArkTakeRewardLineRewardReq,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_TAKE_REWARD_LINE_REWARD_RSP")]
	CmdChapterArkTakeRewardLineRewardRsp,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_GET_RANK_REQ")]
	CmdChapterArkGetRankReq,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_GET_RANK_RSP")]
	CmdChapterArkGetRankRsp,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_FINISH_OPENING_REQ")]
	CmdChapterArkFinishOpeningReq,
	[ProtoEnum(Name = "CMD_CHAPTER_ARK_FINISH_OPENING_RSP")]
	CmdChapterArkFinishOpeningRsp
}
