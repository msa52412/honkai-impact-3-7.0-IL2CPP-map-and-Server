using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WaveRushLimitConditionType
{
	[ProtoEnum(Name = "WAVE_RUSH_LIMIT_CONDITION_NONE")]
	WaveRushLimitConditionNone,
	[ProtoEnum(Name = "WAVE_RUSH_LIMIT_CONDITION_PRE_SITE")]
	WaveRushLimitConditionPreSite
}
