using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TeamAssaultActivityScoreType
{
	[ProtoEnum(Name = "TEAM_ASSAULT_ACTIVITY_SCORE_ACCUM")]
	TeamAssaultActivityScoreAccum = 1,
	[ProtoEnum(Name = "TEAM_ASSAULT_ACTIVITY_SCORE_MAX")]
	TeamAssaultActivityScoreMax
}
