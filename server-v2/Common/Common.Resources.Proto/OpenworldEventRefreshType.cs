using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldEventRefreshType
{
	[ProtoEnum(Name = "OPENWORLD_EVENT_REFRESH_CYCLE")]
	OpenworldEventRefreshCycle = 1,
	[ProtoEnum(Name = "OPENWORLD_EVENT_REFRESH_ONCE")]
	OpenworldEventRefreshOnce,
	[ProtoEnum(Name = "OPENWORLD_EVENT_REFRESH_TIME")]
	OpenworldEventRefreshTime
}
