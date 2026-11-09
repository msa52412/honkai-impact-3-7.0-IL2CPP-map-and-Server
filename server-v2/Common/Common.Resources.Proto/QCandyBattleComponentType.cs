using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyBattleComponentType
{
	[ProtoEnum(Name = "QCANDY_BATTLE_COMPONENT_TYPE_NONE")]
	QcandyBattleComponentTypeNone,
	[ProtoEnum(Name = "QCANDY_BATTLE_COMPONENT_TYPE_POSITION")]
	QcandyBattleComponentTypePosition
}
