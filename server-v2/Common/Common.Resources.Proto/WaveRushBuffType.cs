using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WaveRushBuffType
{
	[ProtoEnum(Name = "WAVE_RUSH_CORE_BUFF")]
	WaveRushCoreBuff = 1,
	[ProtoEnum(Name = "WAVE_RUSH_NORMAL_BUFF")]
	WaveRushNormalBuff,
	[ProtoEnum(Name = "WAVE_RUSH_WEATHER_BUFF")]
	WaveRushWeatherBuff,
	[ProtoEnum(Name = "WAVE_RUSH_ATTR_BUFF")]
	WaveRushAttrBuff
}
