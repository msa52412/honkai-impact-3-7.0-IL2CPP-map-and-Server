using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SecurityPasswordOpType
{
	[ProtoEnum(Name = "SECURITY_PASSWORD_NONE_OP")]
	SecurityPasswordNoneOp,
	[ProtoEnum(Name = "SECURITY_PASSWORD_FAST_UNPROTECT_WEAPON")]
	SecurityPasswordFastUnprotectWeapon,
	[ProtoEnum(Name = "SECURITY_PASSWORD_FAST_UNPROTECT_STIGMATA")]
	SecurityPasswordFastUnprotectStigmata
}
