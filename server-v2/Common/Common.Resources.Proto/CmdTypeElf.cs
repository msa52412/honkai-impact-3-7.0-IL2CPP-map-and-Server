using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeElf
{
	[ProtoEnum(Name = "CMD_GET_ELF_DATA_REQ")]
	CmdGetElfDataReq = 2100,
	[ProtoEnum(Name = "CMD_GET_ELF_DATA_RSP")]
	CmdGetElfDataRsp = 2101,
	[ProtoEnum(Name = "CMD_SYNC_ELF_DATA_NOTIFY")]
	CmdSyncElfDataNotify = 2102,
	[ProtoEnum(Name = "CMD_SYNC_ELF_FRAGMENT_NOTIFY")]
	CmdSyncElfFragmentNotify = 2103,
	[ProtoEnum(Name = "CMD_SYNC_ELF_TALENT_NOTIFY")]
	CmdSyncElfTalentNotify = 2104,
	[ProtoEnum(Name = "CMD_ELF_STAR_UP_REQ")]
	CmdElfStarUpReq = 2105,
	[ProtoEnum(Name = "CMD_ELF_STAR_UP_RSP")]
	CmdElfStarUpRsp = 2106,
	[ProtoEnum(Name = "CMD_ADD_ELF_EXP_BY_MATERIAL_REQ")]
	CmdAddElfExpByMaterialReq = 2107,
	[ProtoEnum(Name = "CMD_ADD_ELF_EXP_BY_MATERIAL_RSP")]
	CmdAddElfExpByMaterialRsp = 2108,
	[ProtoEnum(Name = "CMD_ELF_UPGRADE_LEVEL_PERIOD_REQ")]
	CmdElfUpgradeLevelPeriodReq = 2109,
	[ProtoEnum(Name = "CMD_ELF_UPGRADE_LEVEL_PERIOD_RSP")]
	CmdElfUpgradeLevelPeriodRsp = 2110,
	[ProtoEnum(Name = "CMD_ELF_FRAGMENT_TRANSFORM_REQ")]
	CmdElfFragmentTransformReq = 2121,
	[ProtoEnum(Name = "CMD_ELF_FRAGMENT_TRANSFORM_RSP")]
	CmdElfFragmentTransformRsp = 2122,
	[ProtoEnum(Name = "CMD_ELF_SKILL_LEVEL_UP_REQ")]
	CmdElfSkillLevelUpReq = 2123,
	[ProtoEnum(Name = "CMD_ELF_SKILL_LEVEL_UP_RSP")]
	CmdElfSkillLevelUpRsp = 2124,
	[ProtoEnum(Name = "CMD_ELF_TAKE_COMPENSATION_REQ")]
	CmdElfTakeCompensationReq = 2125,
	[ProtoEnum(Name = "CMD_ELF_TAKE_COMPENSATION_RSP")]
	CmdElfTakeCompensationRsp = 2126
}
