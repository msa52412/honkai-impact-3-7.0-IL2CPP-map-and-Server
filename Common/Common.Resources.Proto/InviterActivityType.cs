using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum InviterActivityType
{
	[ProtoEnum(Name = "INVITER_ACTIVITY_TYPE_NONE")]
	InviterActivityTypeNone,
	[ProtoEnum(Name = "INVITER_ACTIVITY_TYPE_NORMAL")]
	InviterActivityTypeNormal,
	[ProtoEnum(Name = "INVITER_ACTIVITY_TYPE_LIMIT_TIME")]
	InviterActivityTypeLimitTime
}
