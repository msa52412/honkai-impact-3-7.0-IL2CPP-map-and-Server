using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EquipmentSlot
{
	[ProtoEnum(Name = "EQUIPMENT_SLOT_WEAPON_1")]
	EquipmentSlotWeapon1 = 1,
	[ProtoEnum(Name = "EQUIPMENT_SLOT_STIGMATA_1")]
	EquipmentSlotStigmata1,
	[ProtoEnum(Name = "EQUIPMENT_SLOT_STIGMATA_2")]
	EquipmentSlotStigmata2,
	[ProtoEnum(Name = "EQUIPMENT_SLOT_STIGMATA_3")]
	EquipmentSlotStigmata3
}
