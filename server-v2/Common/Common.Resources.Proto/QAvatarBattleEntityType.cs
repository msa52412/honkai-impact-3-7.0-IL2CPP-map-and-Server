using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleEntityType
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_TYPE_NONE")]
	QavatarBattleEntityTypeNone,
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_TYPE_PLAYER")]
	QavatarBattleEntityTypePlayer,
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_TYPE_BULLET")]
	QavatarBattleEntityTypeBullet,
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_TYPE_GADGET")]
	QavatarBattleEntityTypeGadget,
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_TYPE_SCHEDULED_TASK")]
	QavatarBattleEntityTypeScheduledTask
}
