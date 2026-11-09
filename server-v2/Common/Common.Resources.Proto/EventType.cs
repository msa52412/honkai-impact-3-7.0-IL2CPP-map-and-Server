using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EventType
{
	[ProtoEnum(Name = "EVENT_TYPE_ISLAND")]
	EventTypeIsland = 1,
	[ProtoEnum(Name = "EVENT_TYPE_CITY_ACTIVITY")]
	EventTypeCityActivity
}
