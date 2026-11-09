using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DiscountType
{
	[ProtoEnum(Name = "DISCOUNT_EQUIP_POWER_UP_SCOIN")]
	DiscountEquipPowerUpScoin = 1,
	[ProtoEnum(Name = "DISCOUNT_AVATAR_SKILL_UP_SCOIN")]
	DiscountAvatarSkillUpScoin,
	[ProtoEnum(Name = "DISCOUNT_EQUIP_POWER_UP_BOOST_RATE")]
	DiscountEquipPowerUpBoostRate,
	[ProtoEnum(Name = "DISCOUNT_EQUIP_POWER_UP_BOOST_PROB")]
	DiscountEquipPowerUpBoostProb
}
