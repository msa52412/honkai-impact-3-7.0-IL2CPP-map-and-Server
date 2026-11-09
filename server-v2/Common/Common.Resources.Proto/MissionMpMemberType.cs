using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MissionMpMemberType
{
	[ProtoEnum(Name = "MISSION_MP_LEADER")]
	MissionMpLeader = 1,
	[ProtoEnum(Name = "MISSION_MP_ASSISTANT")]
	MissionMpAssistant
}
