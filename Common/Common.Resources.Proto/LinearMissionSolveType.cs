using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LinearMissionSolveType
{
	[ProtoEnum(Name = "LINEAR_MISSION_SOLVE_CLOSE")]
	LinearMissionSolveClose,
	[ProtoEnum(Name = "LINEAR_MISSION_SOLVE_RESERVE")]
	LinearMissionSolveReserve,
	[ProtoEnum(Name = "LINEAR_MISSION_SOLVE_AUTO")]
	LinearMissionSolveAuto
}
