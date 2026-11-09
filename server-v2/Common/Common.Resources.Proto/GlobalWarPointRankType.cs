using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalWarPointRankType
{
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_RANK_NONE")]
	GlobalWarPointRankNone,
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_RANK_CURRENCY")]
	GlobalWarPointRankCurrency,
	[ProtoEnum(Name = "GLOBAL_WAR_POINT_RANK_SCORE")]
	GlobalWarPointRankScore
}
