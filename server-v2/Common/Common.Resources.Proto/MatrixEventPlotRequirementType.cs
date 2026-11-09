using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatrixEventPlotRequirementType
{
	[ProtoEnum(Name = "MATRIX_EVENT_PLOT_REQUIREMENT_INVALID")]
	MatrixEventPlotRequirementInvalid,
	[ProtoEnum(Name = "MATRIX_EVENT_PLOT_REQUIREMENT_MATERIAL")]
	MatrixEventPlotRequirementMaterial,
	[ProtoEnum(Name = "MATRIX_EVENT_PLOT_REQUIREMENT_MISSION")]
	MatrixEventPlotRequirementMission,
	[ProtoEnum(Name = "MATRIX_EVENT_PLOT_REQUIREMENT_DIALOG")]
	MatrixEventPlotRequirementDialog,
	[ProtoEnum(Name = "MATRIX_EVENT_PLOT_REQUIREMENT_FLOOR_EVENT")]
	MatrixEventPlotRequirementFloorEvent
}
