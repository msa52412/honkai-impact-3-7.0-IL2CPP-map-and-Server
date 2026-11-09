using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FacilityType
{
	[ProtoEnum(Name = "FACILITY_TYPE_AVATAR_EXP")]
	FacilityTypeAvatarExp = 1,
	[ProtoEnum(Name = "FACILITY_TYPE_AVATAR_SKILL_POINT")]
	FacilityTypeAvatarSkillPoint,
	[ProtoEnum(Name = "FACILITY_TYPE_WEAPON_EXP")]
	FacilityTypeWeaponExp,
	[ProtoEnum(Name = "FACILITY_TYPE_STIGMATA_EXP")]
	FacilityTypeStigmataExp,
	[ProtoEnum(Name = "FACILITY_TYPE_BAR")]
	FacilityTypeBar,
	[ProtoEnum(Name = "FACILITY_TYPE_AUDIO_PLAYER")]
	FacilityTypeAudioPlayer,
	[ProtoEnum(Name = "FACILITY_TYPE_ENERGY")]
	FacilityTypeEnergy,
	[ProtoEnum(Name = "FACILITY_TYPE_HOMELAND")]
	FacilityTypeHomeland,
	[ProtoEnum(Name = "FACILITY_TYPE_REFRIGERATOR")]
	FacilityTypeRefrigerator,
	[ProtoEnum(Name = "FACILITY_TYPE_DOOR")]
	FacilityTypeDoor
}
