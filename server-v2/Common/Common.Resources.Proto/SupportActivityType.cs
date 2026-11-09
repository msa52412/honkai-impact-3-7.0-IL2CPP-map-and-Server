using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SupportActivityType
{
	[ProtoEnum(Name = "ACTIVITY_TYPE_GLOBAL_SUPPORT")]
	ActivityTypeGlobalSupport,
	[ProtoEnum(Name = "ACTIVITY_TYPE_NORMAL")]
	ActivityTypeNormal
}
