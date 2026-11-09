using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GratuityAcceptType
{
	[ProtoEnum(Name = "GRATUITY_ACCEPT_NONE")]
	GratuityAcceptNone,
	[ProtoEnum(Name = "GRATUITY_ACCEPT_FREE")]
	GratuityAcceptFree,
	[ProtoEnum(Name = "GRATUITY_ACCEPT_ACTIVITY")]
	GratuityAcceptActivity
}
