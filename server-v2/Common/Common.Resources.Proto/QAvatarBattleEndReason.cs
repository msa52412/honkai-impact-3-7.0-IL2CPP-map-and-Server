using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBattleEndReason
{
	[ProtoEnum(Name = "QAVATAR_BATTLE_END_REASON_NONE")]
	QavatarBattleEndReasonNone,
	[ProtoEnum(Name = "QAVATAR_BATTLE_END_REASON_FAIL")]
	QavatarBattleEndReasonFail,
	[ProtoEnum(Name = "QAVATAR_BATTLE_END_REASON_TIMEOUT")]
	QavatarBattleEndReasonTimeout,
	[ProtoEnum(Name = "QAVATAR_BATTLE_END_REASON_TIMEOUT_INVALID")]
	QavatarBattleEndReasonTimeoutInvalid,
	[ProtoEnum(Name = "QAVATAR_BATTLE_END_REASON_KILL_NUM")]
	QavatarBattleEndReasonKillNum,
	[ProtoEnum(Name = "QAVATAR_BATTLE_END_REASON_GM")]
	QavatarBattleEndReasonGm
}
