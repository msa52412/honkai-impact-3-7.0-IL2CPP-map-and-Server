using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatrixFingerType
{
	[ProtoEnum(Name = "MATRIX_FINGER_NONE")]
	MatrixFingerNone,
	[ProtoEnum(Name = "MATRIX_FINGER_SCISSORS")]
	MatrixFingerScissors,
	[ProtoEnum(Name = "MATRIX_FINGER_STONE")]
	MatrixFingerStone,
	[ProtoEnum(Name = "MATRIX_FINGER_CLOTH")]
	MatrixFingerCloth
}
