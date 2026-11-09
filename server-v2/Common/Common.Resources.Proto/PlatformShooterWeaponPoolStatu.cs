using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlatformShooterWeaponPoolStatus
{
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_POOL_NONE")]
	PlatformShooterWeaponPoolNone,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_POOL_LOCKED")]
	PlatformShooterWeaponPoolLocked,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_POOL_DOING")]
	PlatformShooterWeaponPoolDoing,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_WEAPON_POOL_FINISHED")]
	PlatformShooterWeaponPoolFinished
}
