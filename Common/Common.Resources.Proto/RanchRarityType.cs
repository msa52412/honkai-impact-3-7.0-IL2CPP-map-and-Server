using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RanchRarityType
{
	[ProtoEnum(Name = "RANCH_LEGENDARY")]
	RanchLegendary = 4
}
