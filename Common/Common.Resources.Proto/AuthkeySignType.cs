using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AuthkeySignType
{
	[ProtoEnum(Name = "AUTHKEY_SIGN_TYPE_DEFAULT")]
	AuthkeySignTypeDefault = 1,
	[ProtoEnum(Name = "AUTHKEY_SIGN_TYPE_RSA")]
	AuthkeySignTypeRsa
}
