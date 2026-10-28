using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WaveRushStageType
{
	[ProtoEnum(Name = "WAVE_RUSH_STAGE_MAINLINE")]
	WaveRushStageMainline = 1,
	[ProtoEnum(Name = "WAVE_RUSH_STAGE_DAILY")]
	WaveRushStageDaily,
	[ProtoEnum(Name = "WAVE_RUSH_STAGE_TOWER")]
	WaveRushStageTower
}
