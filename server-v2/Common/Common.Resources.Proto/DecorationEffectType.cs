using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DecorationEffectType
{
	[ProtoEnum(Name = "DECORATION_EFFECT_FURNITURE_LIMIT")]
	DecorationEffectFurnitureLimit = 1,
	[ProtoEnum(Name = "DECORATION_EFFECT_AVATAR_LIMIT")]
	DecorationEffectAvatarLimit,
	[ProtoEnum(Name = "DECORATION_EFFECT_UNLOCK_ROOM")]
	DecorationEffectUnlockRoom,
	[ProtoEnum(Name = "DECORATION_EFFECT_POWER_LIMIT")]
	DecorationEffectPowerLimit
}
