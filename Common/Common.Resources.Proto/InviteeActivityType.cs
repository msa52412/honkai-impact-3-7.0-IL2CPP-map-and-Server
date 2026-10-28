using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum InviteeActivityType
{
	[ProtoEnum(Name = "INVITEE_ACTIVITY_TYPE_NONE")]
	InviteeActivityTypeNone,
	[ProtoEnum(Name = "INVITEE_ACTIVITY_TYPE_NEWBIE")]
	InviteeActivityTypeNewbie,
	[ProtoEnum(Name = "INVITEE_ACTIVITY_TYPE_GOBACK")]
	InviteeActivityTypeGoback
}
