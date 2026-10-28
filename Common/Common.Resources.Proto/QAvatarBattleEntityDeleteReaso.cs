using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleEntityDeleteReasonType
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_DELETE_REASON_NONE")]
	QavatarBattleEntityDeleteReasonNone,
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_DELETE_REASON_EXPIRE")]
	QavatarBattleEntityDeleteReasonExpire,
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_DELETE_REASON_HIT")]
	QavatarBattleEntityDeleteReasonHit,
	[ProtoEnum(Name = "QAVATAR_BATTLE_ENTITY_DELETE_REASON_PICK_UP")]
	QavatarBattleEntityDeleteReasonPickUp
}
