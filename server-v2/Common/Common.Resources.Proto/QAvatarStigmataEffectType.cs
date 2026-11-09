using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarStigmataEffectType
{
	[ProtoEnum(Name = "QAVATAR_STIGMATA_EFFECT_NONE")]
	QavatarStigmataEffectNone = 0,
	[ProtoEnum(Name = "QAVATAR_STIGMATA_EFFECT_DIRECT")]
	QavatarStigmataEffectDirect = 1,
	[ProtoEnum(Name = "QAVATAR_STIGMATA_EFFECT_SKILL_TRIGGER")]
	QavatarStigmataEffectSkillTrigger = 2,
	[ProtoEnum(Name = "QAVATAR_STIGMATA_EFFECT_ROLE")]
	QavatarStigmataEffectRole = 3,
	[ProtoEnum(Name = "QAVATAR_STIGMATA_EFFECT_AVATAR_ID")]
	QavatarStigmataEffectAvatarId = 4,
	[ProtoEnum(Name = "QAVATAR_STIGMATA_EFFECT_ATTACK_ON")]
	QavatarStigmataEffectAttackOn = 5,
	[ProtoEnum(Name = "QAVATAR_STIGMATA_EFFECT_DEL_HP")]
	QavatarStigmataEffectDelHp = 8
}
