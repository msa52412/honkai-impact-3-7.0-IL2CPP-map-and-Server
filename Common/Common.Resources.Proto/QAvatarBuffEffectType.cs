using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBuffEffectType
{
	[ProtoEnum(Name = "QAVATAR_BUFF_EFFECT_NONE")]
	QavatarBuffEffectNone,
	[ProtoEnum(Name = "QAVATAR_BUFF_EFFECT_HP_GREATER")]
	QavatarBuffEffectHpGreater,
	[ProtoEnum(Name = "QAVATAR_BUFF_EFFECT_HP_LESS")]
	QavatarBuffEffectHpLess,
	[ProtoEnum(Name = "QAVATAR_BUFF_EFFECT_OUT_OF_BATTLE")]
	QavatarBuffEffectOutOfBattle
}
