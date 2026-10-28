using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CreditType
{
	[ProtoEnum(Name = "CREDIT_NONE")]
	CreditNone,
	[ProtoEnum(Name = "CREDIT_TVT")]
	CreditTvt
}
