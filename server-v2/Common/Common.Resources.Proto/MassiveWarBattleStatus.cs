using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MassiveWarBattleStatus
{
	[ProtoEnum(Name = "MASSIVE_WAR_BATTLE_STATUS_NONE")]
	MassiveWarBattleStatusNone,
	[ProtoEnum(Name = "MASSIVE_WAR_BATTLE_STATUS_PREPARE")]
	MassiveWarBattleStatusPrepare,
	[ProtoEnum(Name = "MASSIVE_WAR_BATTLE_STATUS_FIGHTING")]
	MassiveWarBattleStatusFighting,
	[ProtoEnum(Name = "MASSIVE_WAR_BATTLE_STATUS_WAIT_SETTLE")]
	MassiveWarBattleStatusWaitSettle,
	[ProtoEnum(Name = "MASSIVE_WAR_BATTLE_STATUS_CLOSE")]
	MassiveWarBattleStatusClose,
	[ProtoEnum(Name = "MASSIVE_WAR_BATTLE_STATUS_DELETE")]
	MassiveWarBattleStatusDelete
}
