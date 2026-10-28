using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalWarPointType
{
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_TYPE_NONE")]
	GlobalWarPointTypeNone,
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_TYPE_NORMAL")]
	GlobalWarPointTypeNormal,
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_TYPE_CRITICAL")]
	GlobalWarPointTypeCritical,
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_TYPE_BOSS")]
	GlobalWarPointTypeBoss
}
