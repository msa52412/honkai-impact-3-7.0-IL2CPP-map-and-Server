using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GachaFreeType
{
	[ProtoEnum(Name = "GACHA_FREE_NONE")]
	GachaFreeNone,
	[ProtoEnum(Name = "GACHA_FREE_ONE_PULL")]
	GachaFreeOnePull,
	[ProtoEnum(Name = "GACHA_FREE_TEN_PULLS")]
	GachaFreeTenPulls
}
