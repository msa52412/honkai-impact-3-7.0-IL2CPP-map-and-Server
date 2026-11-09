using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum IslandPositionType
{
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_POWER")]
	IslandPositionCabinPower = 1,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_ENHANCE_KIANA")]
	IslandPositionCabinEnhanceKiana = 2,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_COLLECT")]
	IslandPositionCabinCollect = 3,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_MISC")]
	IslandPositionCabinMisc = 4,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_VENTURE")]
	IslandPositionCabinVenture = 5,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_ENHANCE_MEI")]
	IslandPositionCabinEnhanceMei = 6,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_ENHANCE_BRONYA")]
	IslandPositionCabinEnhanceBronya = 7,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_ENHANCE_HIMEKO")]
	IslandPositionCabinEnhanceHimeko = 8,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_ENHANCE_THERESA")]
	IslandPositionCabinEnhanceTheresa = 9,
	[ProtoEnum(Name = "ISLAND_POSITION_CABIN_ENHANCE_FUKA")]
	IslandPositionCabinEnhanceFuka = 10,
	[ProtoEnum(Name = "ISLAND_POSITION_NULL")]
	IslandPositionNull = 21,
	[ProtoEnum(Name = "ISLAND_POSITION_MAIN")]
	IslandPositionMain = 22
}
