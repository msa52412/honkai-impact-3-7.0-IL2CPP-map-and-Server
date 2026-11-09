using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatrixFloorType
{
	[ProtoEnum(Name = "MATRIX_FLOOR_INVALID")]
	MatrixFloorInvalid,
	[ProtoEnum(Name = "MATRIX_FLOOR_NORMAL")]
	MatrixFloorNormal,
	[ProtoEnum(Name = "MATRIX_FLOOR_EXPLORE")]
	MatrixFloorExplore,
	[ProtoEnum(Name = "MATRIX_FLOOR_MONOPOLY")]
	MatrixFloorMonopoly
}
