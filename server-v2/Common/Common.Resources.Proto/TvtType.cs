using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TvtType
{
	[ProtoEnum(Name = "TVT_NONE")]
	TvtNone,
	[ProtoEnum(Name = "TVT_NORMAL")]
	TvtNormal,
	[ProtoEnum(Name = "TVT_RANKING")]
	TvtRanking
}
