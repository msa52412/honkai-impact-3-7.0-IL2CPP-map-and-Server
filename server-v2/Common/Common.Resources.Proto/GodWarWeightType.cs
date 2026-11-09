using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarWeightType
{
	[ProtoEnum(Name = "GOD_WAR_WEIGHT_NUM")]
	GodWarWeightNum = 1,
	[ProtoEnum(Name = "GOD_WAR_WEIGHT_OVERALL")]
	GodWarWeightOverall
}
