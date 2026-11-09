using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WarshipSettingType
{
	[ProtoEnum(Name = "WARSHIP_SETTING_BGM_PLAY_MODE")]
	WarshipSettingBgmPlayMode = 1,
	[ProtoEnum(Name = "WARSHIP_SETTING_BGM_WEATHER")]
	WarshipSettingBgmWeather
}
