using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleDropItemType
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_DROP_ITEM_TYPE_BUFF")]
	QavatarBattleDropItemTypeBuff = 1,
	[ProtoEnum(Name = "QAVATAR_BATTLE_DROP_ITEM_TYPE_STIGMATA")]
	QavatarBattleDropItemTypeStigmata,
	[ProtoEnum(Name = "QAVATAR_BATTLE_DROP_ITEM_TYPE_INSTANT_HEAL_HP")]
	QavatarBattleDropItemTypeInstantHealHp
}
