using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatrixTriggerType
{
	[ProtoEnum(Name = "MATRIX_TRIGGER_NONE")]
	MatrixTriggerNone,
	[ProtoEnum(Name = "MATRIX_TRIGGER_STEP_NUM_GE")]
	MatrixTriggerStepNumGe,
	[ProtoEnum(Name = "MATRIX_TRIGGER_POSITION")]
	MatrixTriggerPosition,
	[ProtoEnum(Name = "MATRIX_TRIGGER_STEP_NUM_LE")]
	MatrixTriggerStepNumLe
}
