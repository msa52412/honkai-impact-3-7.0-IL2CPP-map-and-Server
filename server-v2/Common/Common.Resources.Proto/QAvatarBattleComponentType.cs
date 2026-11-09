using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleComponentType
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_NONE")]
	QavatarBattleComponentTypeNone,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_POSITION")]
	QavatarBattleComponentTypePosition,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_SPEED")]
	QavatarBattleComponentTypeSpeed,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_RIGID_BODY")]
	QavatarBattleComponentTypeRigidBody,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_SKILL")]
	QavatarBattleComponentTypeSkill,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_BUFF")]
	QavatarBattleComponentTypeBuff,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_EVADE")]
	QavatarBattleComponentTypeEvade,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_STIGMATA")]
	QavatarBattleComponentTypeStigmata,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_CONTACT")]
	QavatarBattleComponentTypeContact,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COMPONENT_TYPE_TICK")]
	QavatarBattleComponentTypeTick
}
