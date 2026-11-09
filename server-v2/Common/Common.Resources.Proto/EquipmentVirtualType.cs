using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum EquipmentVirtualType
{
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_ARMADA_FUND")]
	EquipmentVirtualArmadaFund = 80001,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_MP_EXP")]
	EquipmentVirtualMpExp = 80002,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_SCOIN")]
	EquipmentVirtualScoin = 80006,
	[ProtoEnum(Name = "EQUIPMENT_MASTER_FAME_EXP")]
	EquipmentMasterFameExp = 80007,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_SEASON_EXP")]
	EquipmentVirtualSeasonExp = 80008,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_PLAYER_EXP")]
	EquipmentVirtualPlayerExp = 80009,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_ARMADA_CONTRIBUTION")]
	EquipmentVirtualArmadaContribution = 80011,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_VITALITY")]
	EquipmentVirtualVitality = 80012,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_CARD_PRODUCT")]
	EquipmentVirtualCardProduct = 80013,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_CONTRACT_POINT")]
	EquipmentVirtualContractPoint = 80014,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_DLC_EXP")]
	EquipmentVirtualDlcExp = 80015,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_BATTLE_PASS_EXP_ACTIVITY")]
	EquipmentVirtualBattlePassExpActivity = 80016,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_ADVENTURE_BADGE")]
	EquipmentVirtualAdventureBadge = 80017,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_ADVENTURE_GRAIN")]
	EquipmentVirtualAdventureGrain = 80018,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_CHAPTER_ACTIVITY_EXP")]
	EquipmentVirtualChapterActivityExp = 80019,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_PICTURE_SCORE")]
	EquipmentVirtualPictureScore = 80020,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_OW_FOX_EXP")]
	EquipmentVirtualOwFoxExp = 80021,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_OW_AI_EXP")]
	EquipmentVirtualOwAiExp = 80022,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_SANCTUARY_EXP")]
	EquipmentVirtualSanctuaryExp = 80023,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_CUSTOM_GACHA_ENERGY")]
	EquipmentVirtualCustomGachaEnergy = 80024,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_BATTLE_PASS_THEME_EXP")]
	EquipmentVirtualBattlePassThemeExp = 80025,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_OPTIONAL_GACHA_ENERGY")]
	EquipmentVirtualOptionalGachaEnergy = 80026,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_NEWBIE_PRIVILEGE")]
	EquipmentVirtualNewbiePrivilege = 80027,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_SC_DLC_EXP")]
	EquipmentVirtualScDlcExp = 80028,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_BATTLE_PASS_EXP_DUTY")]
	EquipmentVirtualBattlePassExpDuty = 80029,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_CUSTOM_GOBACK_GACHA_ENERGY")]
	EquipmentVirtualCustomGobackGachaEnergy = 80030,
	[ProtoEnum(Name = "EQUIPMENT_VIRTUAL_OW_TERMINAL_EXP")]
	EquipmentVirtualOwTerminalExp = 80031
}
