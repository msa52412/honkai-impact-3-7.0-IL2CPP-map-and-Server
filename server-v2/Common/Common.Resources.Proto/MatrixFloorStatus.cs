using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatrixFloorStatus
{
	[ProtoEnum(Name = "MATRIX_FLOOR_STATUS_INVALID")]
	MatrixFloorStatusInvalid = 0,
	[ProtoEnum(Name = "MATRIX_FLOOR_STATUS_LOCKED")]
	MatrixFloorStatusLocked = 1,
	[ProtoEnum(Name = "MATRIX_FLOOR_STATUS_UNLOCKED")]
	MatrixFloorStatusUnlocked = 2,
	[ProtoEnum(Name = "MATRIX_FLOOR_STATUS_FREE")]
	MatrixFloorStatusFree = 3,
	[ProtoEnum(Name = "MATRIX_FLOOR_STATUS_IN_STAGE")]
	MatrixFloorStatusInStage = 11,
	[ProtoEnum(Name = "MATRIX_FLOOR_STATUS_IN_PLOT")]
	MatrixFloorStatusInPlot = 12
}
