using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypePlatformshooter
{
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_GET_ACTIVITY_REQ")]
	CmdPlatformShooterGetActivityReq = 6950,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_GET_ACTIVITY_RSP")]
	CmdPlatformShooterGetActivityRsp,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_QAVATAR_LEVEL_UP_REQ")]
	CmdPlatformShooterQavatarLevelUpReq,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_QAVATAR_LEVEL_UP_RSP")]
	CmdPlatformShooterQavatarLevelUpRsp,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_WEAPON_POOL_GET_DISPLAY_REQ")]
	CmdPlatformShooterWeaponPoolGetDisplayReq,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_WEAPON_POOL_GET_DISPLAY_RSP")]
	CmdPlatformShooterWeaponPoolGetDisplayRsp,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_WEAPON_POOL_GACHA_REQ")]
	CmdPlatformShooterWeaponPoolGachaReq,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_WEAPON_POOL_GACHA_RSP")]
	CmdPlatformShooterWeaponPoolGachaRsp,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_WEAPON_AFFIX_REFINE_REQ")]
	CmdPlatformShooterWeaponAffixRefineReq,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_WEAPON_AFFIX_REFINE_RSP")]
	CmdPlatformShooterWeaponAffixRefineRsp,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_WEAPON_AFFIX_CONFIRM_REQ")]
	CmdPlatformShooterWeaponAffixConfirmReq,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_WEAPON_AFFIX_CONFIRM_RSP")]
	CmdPlatformShooterWeaponAffixConfirmRsp,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_GET_RANK_REQ")]
	CmdPlatformShooterGetRankReq,
	[ProtoEnum(Name = "CMD_PLATFORM_SHOOTER_GET_RANK_RSP")]
	CmdPlatformShooterGetRankRsp
}
