using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatrixMonopolyBuffType
{
	[ProtoEnum(Name = "MATRIX_MONOPOLY_BUFF_NONE")]
	MatrixMonopolyBuffNone,
	[ProtoEnum(Name = "MATRIX_MONOPOLY_BUFF_DICE")]
	MatrixMonopolyBuffDice,
	[ProtoEnum(Name = "MATRIX_MONOPOLY_BUFF_STAGE")]
	MatrixMonopolyBuffStage,
	[ProtoEnum(Name = "MATRIX_MONOPOLY_BUFF_DROP")]
	MatrixMonopolyBuffDrop
}
