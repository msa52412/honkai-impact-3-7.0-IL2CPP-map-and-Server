using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum HuntActivityEntityType
{
	[ProtoEnum(Name = "HUNT_ACTIVITY_TYPE_NONE")]
	HuntActivityTypeNone,
	[ProtoEnum(Name = "HUNT_ACTIVITY_TYPE_HUNTER")]
	HuntActivityTypeHunter,
	[ProtoEnum(Name = "HUNT_ACTIVITY_TYPE_STRONGHOLD")]
	HuntActivityTypeStronghold,
	[ProtoEnum(Name = "HUNT_ACTIVITY_TYPE_MACHINE")]
	HuntActivityTypeMachine
}
