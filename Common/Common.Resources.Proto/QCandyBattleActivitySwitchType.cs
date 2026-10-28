using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyBattleActivitySwitchType
{
	[ProtoEnum(Name = "QCANDY_BATTLE_ACTIVITY_SWITCH_TYPE_NONE")]
	QcandyBattleActivitySwitchTypeNone,
	[ProtoEnum(Name = "QCANDY_BATTLE_ACTIVITY_SWITCH_TYPE_PVP")]
	QcandyBattleActivitySwitchTypePvp,
	[ProtoEnum(Name = "QCANDY_BATTLE_ACTIVITY_SWITCH_TYPE_RANK")]
	QcandyBattleActivitySwitchTypeRank
}
