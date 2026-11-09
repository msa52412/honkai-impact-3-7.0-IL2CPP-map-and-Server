using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMainDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 10
	}

	[ProtoContract]
	public enum DataType
	{
		[ProtoEnum(Name = "ALL")]
		All = 0,
		[ProtoEnum(Name = "NICKNAME")]
		Nickname = 2,
		[ProtoEnum(Name = "LEVEL")]
		Level = 3,
		[ProtoEnum(Name = "EXP")]
		Exp = 4,
		[ProtoEnum(Name = "HCOIN")]
		Hcoin = 5,
		[ProtoEnum(Name = "SCOIN")]
		Scoin = 6,
		[ProtoEnum(Name = "STAMINA")]
		Stamina = 7,
		[ProtoEnum(Name = "STAMINA_RECOVER_LEFT_TIME")]
		StaminaRecoverLeftTime = 8,
		[ProtoEnum(Name = "STAMINA_RECOVER_CONFIG_TIME")]
		StaminaRecoverConfigTime = 9,
		[ProtoEnum(Name = "EQUIPMENT_SIZE_LIMIT")]
		EquipmentSizeLimit = 14,
		[ProtoEnum(Name = "SELF_DESC")]
		SelfDesc = 16,
		[ProtoEnum(Name = "PAY_HCOIN")]
		PayHcoin = 17,
		[ProtoEnum(Name = "FREE_HCOIN")]
		FreeHcoin = 18,
		[ProtoEnum(Name = "ASSISTANT_AVATAR_ID")]
		AssistantAvatarId = 19,
		[ProtoEnum(Name = "IS_ALLOW_COST_SENIOR_EQUIP_ON_CUR_DEVICE")]
		IsAllowCostSeniorEquipOnCurDevice = 20,
		[ProtoEnum(Name = "BIRTHDAY")]
		Birthday = 21,
		[ProtoEnum(Name = "RECHARGE_PRICE")]
		RechargePrice = 22,
		[ProtoEnum(Name = "WAIT_DEL_HCOIN")]
		WaitDelHcoin = 23,
		[ProtoEnum(Name = "ON_MEDAL_ID")]
		OnMedalId = 24,
		[ProtoEnum(Name = "USE_FRAME_ID")]
		UseFrameId = 26,
		[ProtoEnum(Name = "MCOIN")]
		Mcoin = 27,
		[ProtoEnum(Name = "PANEL_ACTIVITY")]
		PanelActivity = 28,
		[ProtoEnum(Name = "VIDEO_POPUP")]
		VideoPopup = 29,
		[ProtoEnum(Name = "ON_PHONE_PENDANT_ID")]
		OnPhonePendantId = 30,
		[ProtoEnum(Name = "STORE_EVALUATE")]
		StoreEvaluate = 31,
		[ProtoEnum(Name = "CHATWORLD_ACTIVITY")]
		ChatworldActivity = 32,
		[ProtoEnum(Name = "LEVEL_LOCK")]
		LevelLock = 33,
		[ProtoEnum(Name = "WARSHIP_AVATAR")]
		WarshipAvatar = 35,
		[ProtoEnum(Name = "USE_CUSTOM_HEAD")]
		UseCustomHead = 36,
		[ProtoEnum(Name = "TOTAL_LOGIN_DAYS")]
		TotalLoginDays = 37,
		[ProtoEnum(Name = "WARSHIP_THEME")]
		WarshipTheme = 38,
		[ProtoEnum(Name = "WAIT_DEL_MATERIAL")]
		WaitDelMaterial = 39
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "type_list")]
	public List<DataType> TypeLists { get; } = new List<DataType>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
