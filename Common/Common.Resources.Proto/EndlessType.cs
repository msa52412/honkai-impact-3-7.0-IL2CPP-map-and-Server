using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EndlessType
{
	[ProtoEnum(Name = "ENDLESS_TYPE_NONE")]
	EndlessTypeNone,
	[ProtoEnum(Name = "ENDLESS_TYPE_NORMAL")]
	EndlessTypeNormal,
	[ProtoEnum(Name = "ENDLESS_TYPE_OPENWORLD")]
	EndlessTypeOpenworld,
	[ProtoEnum(Name = "ENDLESS_TYPE_QUANTUM")]
	EndlessTypeQuantum,
	[ProtoEnum(Name = "ENDLESS_TYPE_ULTRA")]
	EndlessTypeUltra,
	[ProtoEnum(Name = "ENDLESS_TYPE_GREEDY")]
	EndlessTypeGreedy,
	[ProtoEnum(Name = "ENDLESS_TYPE_FRONT")]
	EndlessTypeFront
}
