using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatrixGridType
{
	[ProtoEnum(Name = "MATRIX_GRID_NONE")]
	MatrixGridNone = 0,
	[ProtoEnum(Name = "MATRIX_GRID_EMPTY")]
	MatrixGridEmpty = 1,
	[ProtoEnum(Name = "MATRIX_GRID_BARRIER")]
	MatrixGridBarrier = 2,
	[ProtoEnum(Name = "MATRIX_GRID_START")]
	MatrixGridStart = 3,
	[ProtoEnum(Name = "MATRIX_GRID_ITEM")]
	MatrixGridItem = 4,
	[ProtoEnum(Name = "MATRIX_GRID_MONSTER")]
	MatrixGridMonster = 5,
	[ProtoEnum(Name = "MATRIX_GRID_RANDOM")]
	MatrixGridRandom = 6,
	[ProtoEnum(Name = "MATRIX_GRID_TRAP")]
	MatrixGridTrap = 7,
	[ProtoEnum(Name = "MATRIX_GRID_SHOP")]
	MatrixGridShop = 8,
	[ProtoEnum(Name = "MATRIX_GRID_EXIT")]
	MatrixGridExit = 9,
	[ProtoEnum(Name = "MATRIX_GRID_DIALOG")]
	MatrixGridDialog = 10,
	[ProtoEnum(Name = "MATRIX_GRID_TELEPORT")]
	MatrixGridTeleport = 11,
	[ProtoEnum(Name = "MATRIX_GRID_ICE")]
	MatrixGridIce = 12,
	[ProtoEnum(Name = "MATRIX_GRID_INVALID")]
	MatrixGridInvalid = -1
}
