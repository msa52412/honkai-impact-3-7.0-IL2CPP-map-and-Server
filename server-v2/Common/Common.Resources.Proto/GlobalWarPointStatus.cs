using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalWarPointStatus
{
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_STATUS_NONE")]
	GlobalWarPointStatusNone,
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_STATUS_LOCKED")]
	GlobalWarPointStatusLocked,
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_STATUS_ACTIVE")]
	GlobalWarPointStatusActive,
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_STATUS_CLOSE")]
	GlobalWarPointStatusClose
}
