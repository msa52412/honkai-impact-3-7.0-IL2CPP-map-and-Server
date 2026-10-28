using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GachaProtectType
{
	[ProtoEnum(Name = "GACHA_PROTECT_NONE")]
	GachaProtectNone,
	[ProtoEnum(Name = "GACHA_PROTECT_UP")]
	GachaProtectUp,
	[ProtoEnum(Name = "GACHA_PROTECT_NO_UP")]
	GachaProtectNoUp
}
