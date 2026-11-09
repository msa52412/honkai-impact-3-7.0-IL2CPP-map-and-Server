using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyBattleDelayActionType
{
	[ProtoEnum(Name = "QCANDY_BATTLE_DELAY_ACTION_TYPE_NONE")]
	QcandyBattleDelayActionTypeNone,
	[ProtoEnum(Name = "QCANDY_BATTLE_DELAY_ACTION_TYPE_STATE_RESET")]
	QcandyBattleDelayActionTypeStateReset,
	[ProtoEnum(Name = "QCANDY_BATTLE_DELAY_ACTION_TYPE_SKILL_END")]
	QcandyBattleDelayActionTypeSkillEnd
}
