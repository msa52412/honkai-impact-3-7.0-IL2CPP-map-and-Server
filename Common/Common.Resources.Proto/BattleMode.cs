using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BattleMode
{
	[ProtoEnum(Name = "BATTLE_MODE_NONE")]
	BattleModeNone,
	[ProtoEnum(Name = "BATTLE_MODE_QAVATAR_BATTLE")]
	BattleModeQavatarBattle
}
