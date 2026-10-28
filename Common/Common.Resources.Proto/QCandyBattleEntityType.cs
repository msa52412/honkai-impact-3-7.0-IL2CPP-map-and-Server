using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyBattleEntityType
{
	[ProtoEnum(Name = "QCANDY_BATTLE_ENTITY_TYPE_NONE")]
	QcandyBattleEntityTypeNone,
	[ProtoEnum(Name = "QCANDY_BATTLE_ENTITY_TYPE_PLAYER")]
	QcandyBattleEntityTypePlayer,
	[ProtoEnum(Name = "QCANDY_BATTLE_ENTITY_TYPE_PROP")]
	QcandyBattleEntityTypeProp
}
