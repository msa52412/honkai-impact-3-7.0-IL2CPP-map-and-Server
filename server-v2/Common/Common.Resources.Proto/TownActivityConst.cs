using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownActivityConst
{
	[ProtoEnum(Name = "TOWN_ACTIVITY_ROUND_PER_SECOND")]
	TownActivityRoundPerSecond = 50,
	[ProtoEnum(Name = "TOWN_ACTIVITY_STEP_PER_TILE_CELL")]
	TownActivityStepPerTileCell = 10000,
	[ProtoEnum(Name = "TOWN_ACTIVITY_MAX_HP_RATIO")]
	TownActivityMaxHpRatio = 100000
}
