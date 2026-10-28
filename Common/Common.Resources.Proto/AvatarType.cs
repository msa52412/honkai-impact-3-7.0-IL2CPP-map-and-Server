using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarType
{
	[ProtoEnum(Name = "AVATAR_TRIAL_TYPE")]
	AvatarTrialType = 1,
	[ProtoEnum(Name = "AVATAR_LIMIT_TYPE")]
	AvatarLimitType,
	[ProtoEnum(Name = "AVATAR_FORMAL_TYPE")]
	AvatarFormalType
}
