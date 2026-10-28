using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldMapStatus
{
	[ProtoEnum(Name = "OPENWORLD_MAP_STATUS_NONE")]
	OpenworldMapStatusNone,
	[ProtoEnum(Name = "OPENWORLD_MAP_STATUS_ACTIVATE")]
	OpenworldMapStatusActivate,
	[ProtoEnum(Name = "OPENWORLD_MAP_STATUS_AUTO_ACTIVATE")]
	OpenworldMapStatusAutoActivate,
	[ProtoEnum(Name = "OPENWORLD_MAP_STATUS_LOCK")]
	OpenworldMapStatusLock
}
