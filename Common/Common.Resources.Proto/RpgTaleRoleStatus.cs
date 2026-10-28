using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleRoleStatus
{
	[ProtoEnum(Name = "RPG_TALE_ROLE_STATUS_INVALID")]
	RpgTaleRoleStatusInvalid,
	[ProtoEnum(Name = "RPG_TALE_ROLE_STATUS_LOCKED")]
	RpgTaleRoleStatusLocked,
	[ProtoEnum(Name = "RPG_TALE_ROLE_STATUS_UNLOCKED")]
	RpgTaleRoleStatusUnlocked,
	[ProtoEnum(Name = "RPG_TALE_ROLE_STATUS_AWAY")]
	RpgTaleRoleStatusAway
}
