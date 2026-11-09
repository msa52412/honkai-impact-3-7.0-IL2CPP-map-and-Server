using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DisjoinType
{
	[ProtoEnum(Name = "DISJOIN_TOTAL")]
	DisjoinTotal = 1,
	[ProtoEnum(Name = "DISJOIN_TITLE")]
	DisjoinTitle,
	[ProtoEnum(Name = "DISJOIN_EX")]
	DisjoinEx
}
