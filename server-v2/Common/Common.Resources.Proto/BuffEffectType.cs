using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BuffEffectType
{
	[ProtoEnum(Name = "BUFF_EFFECT_COMMANDER_EXP")]
	BuffEffectCommanderExp = 1,
	[ProtoEnum(Name = "BUFF_EFFECT_AVATAR_EXP")]
	BuffEffectAvatarExp,
	[ProtoEnum(Name = "BUFF_EFFECT_SCOIN")]
	BuffEffectScoin,
	[ProtoEnum(Name = "BUFF_EFFECT_NEWBIE")]
	BuffEffectNewbie,
	[ProtoEnum(Name = "BUFF_EFFECT_GOBACK")]
	BuffEffectGoback
}
