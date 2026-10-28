using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EquipmentType
{
	[ProtoEnum(Name = "EQUIPMENT_MATERIAL")]
	EquipmentMaterial = 1,
	[ProtoEnum(Name = "EQUIPMENT_AVATAR_FRAGMENT")]
	EquipmentAvatarFragment = 2,
	[ProtoEnum(Name = "EQUIPMENT_WEAPON")]
	EquipmentWeapon = 3,
	[ProtoEnum(Name = "EQUIPMENT_STIGMATA")]
	EquipmentStigmata = 4,
	[ProtoEnum(Name = "EQUIPMENT_AVATAR_CARD")]
	EquipmentAvatarCard = 5,
	[ProtoEnum(Name = "EQUIPMENT_ENDLESS_ITEM")]
	EquipmentEndlessItem = 6,
	[ProtoEnum(Name = "EQUIPMENT_DRESS")]
	EquipmentDress = 7,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL")]
	EquipmentVirtual = 8,
	[ProtoEnum(Name = "EQUIPMENT_MEDAL")]
	EquipmentMedal = 10,
	[ProtoEnum(Name = "EQUIPMENT_FURNITURE")]
	EquipmentFurniture = 14,
	[ProtoEnum(Name = "EQUIPMENT_OPENWORLD_ENDLESS_ITEM")]
	EquipmentOpenworldEndlessItem = 15,
	[ProtoEnum(Name = "EQUIPMENT_CUSTOM_HEAD")]
	EquipmentCustomHead = 16,
	[ProtoEnum(Name = "EQUIPMENT_FRAME")]
	EquipmentFrame = 20,
	[ProtoEnum(Name = "EQUIPMENT_SHOPPING_MALL_GIFT")]
	EquipmentShoppingMallGift = 30,
	[ProtoEnum(Name = "EQUIPMENT_MECHA")]
	EquipmentMecha = 34,
	[ProtoEnum(Name = "EQUIPMENT_PHONE_PENDANT")]
	EquipmentPhonePendant = 35,
	[ProtoEnum(Name = "EQUIPMENT_EMOJI")]
	EquipmentEmoji = 36,
	[ProtoEnum(Name = "EQUIPMENT_ELF_FRAGMENT")]
	EquipmentElfFragment = 37,
	[ProtoEnum(Name = "EQUIPMENT_ELF_CARD")]
	EquipmentElfCard = 38,
	[ProtoEnum(Name = "EQUIPMENT_DLC_AVATAR_CARD")]
	EquipmentDlcAvatarCard = 39,
	[ProtoEnum(Name = "EQUIPMENT_WARSHIP")]
	EquipmentWarship = 40,
	[ProtoEnum(Name = "EQUIPMENT_TRIAL_WARSHIP")]
	EquipmentTrialWarship = 41,
	[ProtoEnum(Name = "EQUIPMENT_ERROR")]
	EquipmentError = 100
}
