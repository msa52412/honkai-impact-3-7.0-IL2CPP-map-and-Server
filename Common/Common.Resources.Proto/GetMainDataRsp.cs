using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMainDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 11
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Nickname;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__Hcoin;

	private uint? __pbn__Scoin;

	private uint? __pbn__Stamina;

	private uint? __pbn__StaminaRecoverLeftTime;

	private uint? __pbn__StaminaRecoverConfigTime;

	private uint? __pbn__EquipmentSizeLimit;

	private string __pbn__SelfDesc;

	private uint? __pbn__PayHcoin;

	private uint? __pbn__FreeHcoin;

	private uint? __pbn__AssistantAvatarId;

	private bool? __pbn__IsAllowCostSeniorEquipOnCurDevice;

	private uint? __pbn__Birthday;

	private uint? __pbn__MonthRechargePrice;

	private uint? __pbn__WaitDelHcoin;

	private uint? __pbn__OnMedalId;

	private uint? __pbn__TodayRechargePrice;

	private uint? __pbn__UseFrameId;

	private bool? __pbn__IsAll;

	private uint? __pbn__Mcoin;

	private uint? __pbn__OnPhonePendantId;

	private uint? __pbn__NextEvaluateTime;

	private uint? __pbn__LevelLockId;

	private uint? __pbn__CustomHeadId;

	private uint? __pbn__TotalLoginDays;

	private uint? __pbn__RegisterTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(4, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(5, Name = "hcoin")]
	public uint Hcoin
	{
		get
		{
			return __pbn__Hcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Hcoin = value;
		}
	}

	[ProtoMember(6, Name = "scoin")]
	public uint Scoin
	{
		get
		{
			return __pbn__Scoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Scoin = value;
		}
	}

	[ProtoMember(7, Name = "stamina")]
	public uint Stamina
	{
		get
		{
			return __pbn__Stamina.GetValueOrDefault();
		}
		set
		{
			__pbn__Stamina = value;
		}
	}

	[ProtoMember(8, Name = "stamina_recover_left_time")]
	public uint StaminaRecoverLeftTime
	{
		get
		{
			return __pbn__StaminaRecoverLeftTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StaminaRecoverLeftTime = value;
		}
	}

	[ProtoMember(9, Name = "stamina_recover_config_time")]
	public uint StaminaRecoverConfigTime
	{
		get
		{
			return __pbn__StaminaRecoverConfigTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StaminaRecoverConfigTime = value;
		}
	}

	[ProtoMember(14, Name = "equipment_size_limit")]
	public uint EquipmentSizeLimit
	{
		get
		{
			return __pbn__EquipmentSizeLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__EquipmentSizeLimit = value;
		}
	}

	[ProtoMember(16, Name = "self_desc")]
	[DefaultValue("")]
	public string SelfDesc
	{
		get
		{
			return __pbn__SelfDesc ?? "";
		}
		set
		{
			__pbn__SelfDesc = value;
		}
	}

	[ProtoMember(17, Name = "pay_hcoin")]
	public uint PayHcoin
	{
		get
		{
			return __pbn__PayHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__PayHcoin = value;
		}
	}

	[ProtoMember(18, Name = "free_hcoin")]
	public uint FreeHcoin
	{
		get
		{
			return __pbn__FreeHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeHcoin = value;
		}
	}

	[ProtoMember(19, Name = "assistant_avatar_id")]
	public uint AssistantAvatarId
	{
		get
		{
			return __pbn__AssistantAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistantAvatarId = value;
		}
	}

	[ProtoMember(20, Name = "is_allow_cost_senior_equip_on_cur_device")]
	public bool IsAllowCostSeniorEquipOnCurDevice
	{
		get
		{
			return __pbn__IsAllowCostSeniorEquipOnCurDevice.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAllowCostSeniorEquipOnCurDevice = value;
		}
	}

	[ProtoMember(21, Name = "birthday")]
	public uint Birthday
	{
		get
		{
			return __pbn__Birthday.GetValueOrDefault();
		}
		set
		{
			__pbn__Birthday = value;
		}
	}

	[ProtoMember(22, Name = "month_recharge_price")]
	public uint MonthRechargePrice
	{
		get
		{
			return __pbn__MonthRechargePrice.GetValueOrDefault();
		}
		set
		{
			__pbn__MonthRechargePrice = value;
		}
	}

	[ProtoMember(23, Name = "wait_del_hcoin")]
	public uint WaitDelHcoin
	{
		get
		{
			return __pbn__WaitDelHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__WaitDelHcoin = value;
		}
	}

	[ProtoMember(24, Name = "on_medal_id")]
	public uint OnMedalId
	{
		get
		{
			return __pbn__OnMedalId.GetValueOrDefault();
		}
		set
		{
			__pbn__OnMedalId = value;
		}
	}

	[ProtoMember(26, Name = "today_recharge_price")]
	public uint TodayRechargePrice
	{
		get
		{
			return __pbn__TodayRechargePrice.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayRechargePrice = value;
		}
	}

	[ProtoMember(27, Name = "use_frame_id")]
	public uint UseFrameId
	{
		get
		{
			return __pbn__UseFrameId.GetValueOrDefault();
		}
		set
		{
			__pbn__UseFrameId = value;
		}
	}

	[ProtoMember(28, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

	[ProtoMember(29, Name = "mcoin")]
	public uint Mcoin
	{
		get
		{
			return __pbn__Mcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Mcoin = value;
		}
	}

	[ProtoMember(30, Name = "open_panel_activity_list")]
	public uint[] OpenPanelActivityLists { get; set; }

	[ProtoMember(31, Name = "video_popup_list")]
	public List<VideoPopupData> VideoPopupLists { get; } = new List<VideoPopupData>();


	[ProtoMember(32, Name = "on_phone_pendant_id")]
	public uint OnPhonePendantId
	{
		get
		{
			return __pbn__OnPhonePendantId.GetValueOrDefault();
		}
		set
		{
			__pbn__OnPhonePendantId = value;
		}
	}

	[ProtoMember(33, Name = "next_evaluate_time")]
	public uint NextEvaluateTime
	{
		get
		{
			return __pbn__NextEvaluateTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextEvaluateTime = value;
		}
	}

	[ProtoMember(34, Name = "chatworld_activity_info")]
	public ChatworldActivityInfo ChatworldActivityInfo { get; set; }

	[ProtoMember(35, Name = "level_lock_id")]
	public uint LevelLockId
	{
		get
		{
			return __pbn__LevelLockId.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelLockId = value;
		}
	}

	[ProtoMember(37, Name = "warship_avatar")]
	public WarshipAvatarData WarshipAvatar { get; set; }

	[ProtoMember(38, Name = "custom_head_id")]
	public uint CustomHeadId
	{
		get
		{
			return __pbn__CustomHeadId.GetValueOrDefault();
		}
		set
		{
			__pbn__CustomHeadId = value;
		}
	}

	[ProtoMember(39, Name = "total_login_days")]
	public uint TotalLoginDays
	{
		get
		{
			return __pbn__TotalLoginDays.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalLoginDays = value;
		}
	}

	[ProtoMember(40, Name = "register_time")]
	public uint RegisterTime
	{
		get
		{
			return __pbn__RegisterTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RegisterTime = value;
		}
	}

	[ProtoMember(41, Name = "warship_theme")]
	public WarshipThemeData WarshipTheme { get; set; }

	[ProtoMember(42, Name = "wait_del_material_list")]
	public List<WaitDelMaterialData> WaitDelMaterialLists { get; } = new List<WaitDelMaterialData>();


	[ProtoMember(43, Name = "on_medal")]
	public Medal OnMedal { get; set; }

	[ProtoMember(44, Name = "type_list")]
	public uint[] TypeLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeHcoin()
	{
		return __pbn__Hcoin.HasValue;
	}

	public void ResetHcoin()
	{
		__pbn__Hcoin = null;
	}

	public bool ShouldSerializeScoin()
	{
		return __pbn__Scoin.HasValue;
	}

	public void ResetScoin()
	{
		__pbn__Scoin = null;
	}

	public bool ShouldSerializeStamina()
	{
		return __pbn__Stamina.HasValue;
	}

	public void ResetStamina()
	{
		__pbn__Stamina = null;
	}

	public bool ShouldSerializeStaminaRecoverLeftTime()
	{
		return __pbn__StaminaRecoverLeftTime.HasValue;
	}

	public void ResetStaminaRecoverLeftTime()
	{
		__pbn__StaminaRecoverLeftTime = null;
	}

	public bool ShouldSerializeStaminaRecoverConfigTime()
	{
		return __pbn__StaminaRecoverConfigTime.HasValue;
	}

	public void ResetStaminaRecoverConfigTime()
	{
		__pbn__StaminaRecoverConfigTime = null;
	}

	public bool ShouldSerializeEquipmentSizeLimit()
	{
		return __pbn__EquipmentSizeLimit.HasValue;
	}

	public void ResetEquipmentSizeLimit()
	{
		__pbn__EquipmentSizeLimit = null;
	}

	public bool ShouldSerializeSelfDesc()
	{
		return __pbn__SelfDesc != null;
	}

	public void ResetSelfDesc()
	{
		__pbn__SelfDesc = null;
	}

	public bool ShouldSerializePayHcoin()
	{
		return __pbn__PayHcoin.HasValue;
	}

	public void ResetPayHcoin()
	{
		__pbn__PayHcoin = null;
	}

	public bool ShouldSerializeFreeHcoin()
	{
		return __pbn__FreeHcoin.HasValue;
	}

	public void ResetFreeHcoin()
	{
		__pbn__FreeHcoin = null;
	}

	public bool ShouldSerializeAssistantAvatarId()
	{
		return __pbn__AssistantAvatarId.HasValue;
	}

	public void ResetAssistantAvatarId()
	{
		__pbn__AssistantAvatarId = null;
	}

	public bool ShouldSerializeIsAllowCostSeniorEquipOnCurDevice()
	{
		return __pbn__IsAllowCostSeniorEquipOnCurDevice.HasValue;
	}

	public void ResetIsAllowCostSeniorEquipOnCurDevice()
	{
		__pbn__IsAllowCostSeniorEquipOnCurDevice = null;
	}

	public bool ShouldSerializeBirthday()
	{
		return __pbn__Birthday.HasValue;
	}

	public void ResetBirthday()
	{
		__pbn__Birthday = null;
	}

	public bool ShouldSerializeMonthRechargePrice()
	{
		return __pbn__MonthRechargePrice.HasValue;
	}

	public void ResetMonthRechargePrice()
	{
		__pbn__MonthRechargePrice = null;
	}

	public bool ShouldSerializeWaitDelHcoin()
	{
		return __pbn__WaitDelHcoin.HasValue;
	}

	public void ResetWaitDelHcoin()
	{
		__pbn__WaitDelHcoin = null;
	}

	public bool ShouldSerializeOnMedalId()
	{
		return __pbn__OnMedalId.HasValue;
	}

	public void ResetOnMedalId()
	{
		__pbn__OnMedalId = null;
	}

	public bool ShouldSerializeTodayRechargePrice()
	{
		return __pbn__TodayRechargePrice.HasValue;
	}

	public void ResetTodayRechargePrice()
	{
		__pbn__TodayRechargePrice = null;
	}

	public bool ShouldSerializeUseFrameId()
	{
		return __pbn__UseFrameId.HasValue;
	}

	public void ResetUseFrameId()
	{
		__pbn__UseFrameId = null;
	}

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}

	public bool ShouldSerializeMcoin()
	{
		return __pbn__Mcoin.HasValue;
	}

	public void ResetMcoin()
	{
		__pbn__Mcoin = null;
	}

	public bool ShouldSerializeOnPhonePendantId()
	{
		return __pbn__OnPhonePendantId.HasValue;
	}

	public void ResetOnPhonePendantId()
	{
		__pbn__OnPhonePendantId = null;
	}

	public bool ShouldSerializeNextEvaluateTime()
	{
		return __pbn__NextEvaluateTime.HasValue;
	}

	public void ResetNextEvaluateTime()
	{
		__pbn__NextEvaluateTime = null;
	}

	public bool ShouldSerializeLevelLockId()
	{
		return __pbn__LevelLockId.HasValue;
	}

	public void ResetLevelLockId()
	{
		__pbn__LevelLockId = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeTotalLoginDays()
	{
		return __pbn__TotalLoginDays.HasValue;
	}

	public void ResetTotalLoginDays()
	{
		__pbn__TotalLoginDays = null;
	}

	public bool ShouldSerializeRegisterTime()
	{
		return __pbn__RegisterTime.HasValue;
	}

	public void ResetRegisterTime()
	{
		__pbn__RegisterTime = null;
	}


}
