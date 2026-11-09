using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleTickComponentEventType
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_TICK_EVENT_TYPE_ENABLE")]
	QavatarBattleTickEventTypeEnable = 1,
	[ProtoEnum(Name = "QAVATAR_BATTLE_TICK_EVENT_TYPE_INTERVAL")]
	QavatarBattleTickEventTypeInterval
}
