using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldStatId
{
	[ProtoEnum(Name = "OPENWORLD_STAT_QUEST")]
	OpenworldStatQuest = 1,
	[ProtoEnum(Name = "OPENWORLD_STAT_EVENT_MONSTER")]
	OpenworldStatEventMonster,
	[ProtoEnum(Name = "OPENWORLD_STAT_EVENT_EMERGENCY")]
	OpenworldStatEventEmergency,
	[ProtoEnum(Name = "OPENWORLD_STAT_EVENT_COLLECT")]
	OpenworldStatEventCollect,
	[ProtoEnum(Name = "OPENWORLD_STAT_MECHA")]
	OpenworldStatMecha
}
