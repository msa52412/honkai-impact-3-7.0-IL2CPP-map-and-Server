using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CompensationType
{
	[ProtoEnum(Name = "COMPENSATION_NONE")]
	CompensationNone,
	[ProtoEnum(Name = "COMPENSATION_ISLAND")]
	CompensationIsland,
	[ProtoEnum(Name = "COMPENSATION_DORM")]
	CompensationDorm
}
