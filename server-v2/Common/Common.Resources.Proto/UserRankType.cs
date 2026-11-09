using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum UserRankType
{
	[ProtoEnum(Name = "USER_RANK_NONE")]
	UserRankNone,
	[ProtoEnum(Name = "USER_RANK_NORMAL")]
	UserRankNormal,
	[ProtoEnum(Name = "USER_RANK_PERCENT")]
	UserRankPercent
}
