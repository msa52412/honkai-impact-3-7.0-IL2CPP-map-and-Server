using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SpecificRankType
{
	[ProtoEnum(Name = "SPECIFIC_RANK_NONE")]
	SpecificRankNone,
	[ProtoEnum(Name = "SPECIFIC_RANK_GLOBAL_GOODS")]
	SpecificRankGlobalGoods
}
