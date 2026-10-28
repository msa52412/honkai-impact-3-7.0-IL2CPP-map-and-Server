using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CabinTechEffectType
{
	[ProtoEnum(Name = "CABIN_TECH_ADD_AVATAR_ATTR")]
	CabinTechAddAvatarAttr = 1,
	[ProtoEnum(Name = "CABIN_TECH_ADD_VENTURE_MAX_NUM")]
	CabinTechAddVentureMaxNum,
	[ProtoEnum(Name = "CABIN_TECH_ADD_PROCESS_VENTURE_MAX_NUM")]
	CabinTechAddProcessVentureMaxNum,
	[ProtoEnum(Name = "CABIN_TECH_ADD_REFRESH_VENTURE_ATTR")]
	CabinTechAddRefreshVentureAttr,
	[ProtoEnum(Name = "CABIN_TECH_ADD_FRIEND_NUM")]
	CabinTechAddFriendNum,
	[ProtoEnum(Name = "CABIN_TECH_DISJOIN_EQUIPMENT")]
	CabinTechDisjoinEquipment,
	[ProtoEnum(Name = "CABIN_TECH_ADD_COLLECT_SPEED")]
	CabinTechAddCollectSpeed,
	[ProtoEnum(Name = "CABIN_TECH_ADD_COLLECT_STORAGE")]
	CabinTechAddCollectStorage,
	[ProtoEnum(Name = "CABIN_TECH_ADD_COLLECT_EXTRA_PERCENT")]
	CabinTechAddCollectExtraPercent,
	[ProtoEnum(Name = "CABIN_TECH_ADD_COLLECT_EXTRA_RATE")]
	CabinTechAddCollectExtraRate,
	[ProtoEnum(Name = "CABIN_TECH_ADD_SKILL_POINT_LIMIT")]
	CabinTechAddSkillPointLimit,
	[ProtoEnum(Name = "CABIN_TECH_ADD_COLLECT_EQUIPMENT_DROP")]
	CabinTechAddCollectEquipmentDrop,
	[ProtoEnum(Name = "CABIN_TECH_ADD_COLLECT_EQUIPMENT_PACKAGE_NUM")]
	CabinTechAddCollectEquipmentPackageNum,
	[ProtoEnum(Name = "CABIN_TECH_SUB_COLLECT_EQUIPMENT_TIME")]
	CabinTechSubCollectEquipmentTime
}
