using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MechaSlot
{
	[ProtoEnum(Name = "MECHA_SLOT_NONE")]
	MechaSlotNone,
	[ProtoEnum(Name = "MECHA_SLOT_LEFT_1")]
	MechaSlotLeft1,
	[ProtoEnum(Name = "MECHA_SLOT_LEFT_2")]
	MechaSlotLeft2,
	[ProtoEnum(Name = "MECHA_SLOT_CENTER")]
	MechaSlotCenter,
	[ProtoEnum(Name = "MECHA_SLOT_RIGHT_2")]
	MechaSlotRight2,
	[ProtoEnum(Name = "MECHA_SLOT_RIGHT_1")]
	MechaSlotRight1
}
