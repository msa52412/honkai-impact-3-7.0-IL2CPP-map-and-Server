using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WarshipBgmPlayMode
{
	[ProtoEnum(Name = "WARSHIP_BGM_PLAY_MODE_RANDOM")]
	WarshipBgmPlayModeRandom,
	[ProtoEnum(Name = "WARSHIP_BGM_PLAY_MODE_SINGLE")]
	WarshipBgmPlayModeSingle
}
