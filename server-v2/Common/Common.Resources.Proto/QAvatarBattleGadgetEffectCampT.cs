using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleGadgetEffectCampType
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_EFFECT_CAMP_TYPE_ALL")]
	QavatarBattleGadgetEffectCampTypeAll,
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_EFFECT_CAMP_TYPE_ALLIES")]
	QavatarBattleGadgetEffectCampTypeAllies,
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_EFFECT_CAMP_TYPE_ENEMIES")]
	QavatarBattleGadgetEffectCampTypeEnemies
}
