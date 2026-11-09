using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EndlessSingleModeActivityType
{
	[ProtoEnum(Name = "ENDLESS_SINGLE_MODE_ACTIVITY_TYPE_NONE")]
	EndlessSingleModeActivityTypeNone,
	[ProtoEnum(Name = "ENDLESS_SINGLE_MODE_ACTIVITY_TYPE_NORMAL")]
	EndlessSingleModeActivityTypeNormal,
	[ProtoEnum(Name = "ENDLESS_SINGLE_MODE_ACTIVITY_TYPE_OW")]
	EndlessSingleModeActivityTypeOw
}
