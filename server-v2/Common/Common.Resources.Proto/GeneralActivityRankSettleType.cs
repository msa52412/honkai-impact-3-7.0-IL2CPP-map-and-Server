using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GeneralActivityRankSettleType
{
	[ProtoEnum(Name = "GENERAL_ACTIVITY_RANK_SETTLE_TYPE_DEFAULT")]
	GeneralActivityRankSettleTypeDefault,
	[ProtoEnum(Name = "GENERAL_ACTIVITY_RANK_SETTLE_TYPE_ACTIVITY")]
	GeneralActivityRankSettleTypeActivity
}
