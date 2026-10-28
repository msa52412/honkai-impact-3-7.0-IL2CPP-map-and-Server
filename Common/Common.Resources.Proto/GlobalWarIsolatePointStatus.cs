using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalWarIsolatePointStatus
{
	[ProtoEnum(Name = "GLOBAL_WAR_ISOLATE_POINT_STATUS_NONE")]
	GlobalWarIsolatePointStatusNone,
	[ProtoEnum(Name = "GLOBAL_WAR_ISOLATE_POINT_STATUS_LOCKED")]
	GlobalWarIsolatePointStatusLocked,
	[ProtoEnum(Name = "GLOBAL_WAR_ISOLATE_POINT_STATUS_ACTIVE")]
	GlobalWarIsolatePointStatusActive,
	[ProtoEnum(Name = "GLOBAL_WAR_ISOLATE_POINT_STATUS_CLOSE")]
	GlobalWarIsolatePointStatusClose
}
