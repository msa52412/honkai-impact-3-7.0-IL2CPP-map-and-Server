using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlotType
{
	[ProtoEnum(Name = "PLOT_TYPE_NORMAL")]
	PlotTypeNormal,
	[ProtoEnum(Name = "PLOT_TYPE_DORM")]
	PlotTypeDorm,
	[ProtoEnum(Name = "PLOT_TYPE_MATRIX")]
	PlotTypeMatrix,
	[ProtoEnum(Name = "PLOT_TYPE_RPG")]
	PlotTypeRpg,
	[ProtoEnum(Name = "PLOT_TYPE_ISLAND")]
	PlotTypeIsland,
	[ProtoEnum(Name = "PLOT_TYPE_CITY_ACTIVITY")]
	PlotTypeCityActivity
}
