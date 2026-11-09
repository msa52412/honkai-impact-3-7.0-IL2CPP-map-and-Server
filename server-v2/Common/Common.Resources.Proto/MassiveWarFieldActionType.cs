using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MassiveWarFieldActionType
{
	[ProtoEnum(Name = "MASSIVE_WAR_FIELD_ACTION_NONE")]
	MassiveWarFieldActionNone,
	[ProtoEnum(Name = "MASSIVE_WAR_FIELD_ACTION_JOIN")]
	MassiveWarFieldActionJoin,
	[ProtoEnum(Name = "MASSIVE_WAR_FIELD_ACTION_LEAVE")]
	MassiveWarFieldActionLeave
}
