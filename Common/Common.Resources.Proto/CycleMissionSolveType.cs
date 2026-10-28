using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CycleMissionSolveType
{
	[ProtoEnum(Name = "CYCLE_MISSION_SOLVE_CLOSE")]
	CycleMissionSolveClose,
	[ProtoEnum(Name = "CYCLE_MISSION_SOLVE_RESERVE")]
	CycleMissionSolveReserve
}
