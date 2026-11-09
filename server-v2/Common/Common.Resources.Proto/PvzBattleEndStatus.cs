using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PvzBattleEndStatus
{
	[ProtoEnum(Name = "PVZ_BATTLE_END_NONE")]
	PvzBattleEndNone,
	[ProtoEnum(Name = "PVZ_BATTLE_END_SUCC")]
	PvzBattleEndSucc,
	[ProtoEnum(Name = "PVZ_BATTLE_END_FAIL")]
	PvzBattleEndFail,
	[ProtoEnum(Name = "PVZ_BATTLE_END_QUIT")]
	PvzBattleEndQuit
}
