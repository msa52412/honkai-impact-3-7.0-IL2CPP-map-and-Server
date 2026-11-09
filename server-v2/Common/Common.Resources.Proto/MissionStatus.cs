using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MissionStatus
{
	[ProtoEnum(Name = "MISSION_STATUS_NONE")]
	MissionStatusNone,
	[ProtoEnum(Name = "MISSION_NOT_BEGIN")]
	MissionNotBegin,
	[ProtoEnum(Name = "MISSION_DOING")]
	MissionDoing,
	[ProtoEnum(Name = "MISSION_FINISH")]
	MissionFinish,
	[ProtoEnum(Name = "MISSION_TIMEOUT")]
	MissionTimeout,
	[ProtoEnum(Name = "MISSION_CLOSE")]
	MissionClose,
	[ProtoEnum(Name = "MISSION_WAIT_DELETE")]
	MissionWaitDelete
}
