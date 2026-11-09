using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MpStageMatchType
{
	[ProtoEnum(Name = "CREATE_ENTER")]
	CreateEnter = 1,
	[ProtoEnum(Name = "MATCH_ENTER")]
	MatchEnter
}
