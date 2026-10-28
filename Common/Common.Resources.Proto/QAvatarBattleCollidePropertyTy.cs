using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleCollidePropertyType
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_COLLIDE_PROPERTY_TYPE_WALL")]
	QavatarBattleCollidePropertyTypeWall,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COLLIDE_PROPERTY_TYPE_AVATAR_SENSOR")]
	QavatarBattleCollidePropertyTypeAvatarSensor,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COLLIDE_PROPERTY_TYPE_BULLET")]
	QavatarBattleCollidePropertyTypeBullet,
	[ProtoEnum(Name = "QAVATAR_BATTLE_COLLIDE_PROPERTY_TYPE_AVATAR")]
	QavatarBattleCollidePropertyTypeAvatar
}
