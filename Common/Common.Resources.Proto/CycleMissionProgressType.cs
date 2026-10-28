using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CycleMissionProgressType
{
	[ProtoEnum(Name = "CYCLE_MISSION_PROGRESS_ALL")]
	CycleMissionProgressAll = 1,
	[ProtoEnum(Name = "CYCLE_MISSION_PROGRESS_ONE_BY_ONE")]
	CycleMissionProgressOneByOne
}
