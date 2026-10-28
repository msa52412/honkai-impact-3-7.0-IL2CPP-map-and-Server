using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FarmSlotStatus
{
	[ProtoEnum(Name = "FARM_SLOT_NONE")]
	FarmSlotNone,
	[ProtoEnum(Name = "FARM_SLOT_LOCKED")]
	FarmSlotLocked,
	[ProtoEnum(Name = "FARM_SLOT_FREE")]
	FarmSlotFree,
	[ProtoEnum(Name = "FARM_SLOT_IN_PRODUCE")]
	FarmSlotInProduce,
	[ProtoEnum(Name = "FARM_SLOT_FINISH_PRODUCE")]
	FarmSlotFinishProduce
}
