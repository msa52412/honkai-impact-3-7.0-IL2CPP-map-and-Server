using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SecurityPasswordStatus
{
	[ProtoEnum(Name = "SECURITY_PASSWORD_NOT_SET")]
	SecurityPasswordNotSet,
	[ProtoEnum(Name = "SECURITY_PASSWORD_IS_SET")]
	SecurityPasswordIsSet,
	[ProtoEnum(Name = "SECURITY_PASSWORD_TOO_MANY_FAILS")]
	SecurityPasswordTooManyFails
}
