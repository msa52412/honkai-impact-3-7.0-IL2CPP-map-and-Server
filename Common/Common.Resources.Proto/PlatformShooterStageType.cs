using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlatformShooterStageType
{
	[ProtoEnum(Name = "PLATFORM_SHOOTER_STAGE_INVALID")]
	PlatformShooterStageInvalid,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_STAGE_MAIN")]
	PlatformShooterStageMain,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_STAGE_DAILY")]
	PlatformShooterStageDaily,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_STAGE_DAILY_CHALLENGE")]
	PlatformShooterStageDailyChallenge,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_STAGE_TOWER")]
	PlatformShooterStageTower,
	[ProtoEnum(Name = "PLATFORM_SHOOTER_STAGE_QAVATAR_CHALLENGE")]
	PlatformShooterStageQavatarChallenge
}
