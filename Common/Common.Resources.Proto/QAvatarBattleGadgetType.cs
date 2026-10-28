using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleGadgetType
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_TYPE_BREAKABLE_BOX")]
	QavatarBattleGadgetTypeBreakableBox = 1,
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_TYPE_DROP_ITEM")]
	QavatarBattleGadgetTypeDropItem,
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_TYPE_DAMAGE_AREA")]
	QavatarBattleGadgetTypeDamageArea,
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_TYPE_DELAYED_BOMB")]
	QavatarBattleGadgetTypeDelayedBomb,
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_TYPE_UNSTABLE_DELAYED_BOMB")]
	QavatarBattleGadgetTypeUnstableDelayedBomb,
	[ProtoEnum(Name = "QAVATAR_BATTLE_GADGET_TYPE_BREAKABLE_DELAYED_BOMB")]
	QavatarBattleGadgetTypeBreakableDelayedBomb
}
