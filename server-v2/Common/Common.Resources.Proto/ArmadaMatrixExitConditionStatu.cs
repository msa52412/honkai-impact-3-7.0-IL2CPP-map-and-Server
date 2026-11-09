using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaMatrixExitConditionStatus
{
	[ProtoEnum(Name = "ARMADA_MATRIX_EXIT_CONDITION_STATUS_NONE")]
	ArmadaMatrixExitConditionStatusNone,
	[ProtoEnum(Name = "ARMADA_MATRIX_EXIT_CONDITION_STATUS_DOING")]
	ArmadaMatrixExitConditionStatusDoing,
	[ProtoEnum(Name = "ARMADA_MATRIX_EXIT_CONDITION_STATUS_FINISH")]
	ArmadaMatrixExitConditionStatusFinish
}
