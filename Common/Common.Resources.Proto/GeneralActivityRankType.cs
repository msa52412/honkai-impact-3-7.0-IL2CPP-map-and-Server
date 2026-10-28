using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralActivityRankType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_RANK_TYPE_NONE")]
	GeneralActivityRankTypeNone,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_RANK_TYPE_ABS")]
	GeneralActivityRankTypeAbs,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_RANK_TYPE_PERCENT")]
	GeneralActivityRankTypePercent
}
