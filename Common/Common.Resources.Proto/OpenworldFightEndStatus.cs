using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldFightEndStatus
{
	[ProtoEnum(Name = "OPENWORLD_FIGHT_STATUS_NONE")]
	OpenworldFightStatusNone,
	[ProtoEnum(Name = "OPENWORLD_FIGHT_STATUS_WIN")]
	OpenworldFightStatusWin,
	[ProtoEnum(Name = "OPENWORLD_FIGHT_STATUS_EXCAPE")]
	OpenworldFightStatusExcape,
	[ProtoEnum(Name = "OPENWORLD_FIGHT_STATUS_ALL_DEAD")]
	OpenworldFightStatusAllDead
}
