using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UseMaterialRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 252
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "EQUIPMENT_MATERIAL_LIMIT")]
		EquipmentMaterialLimit,
		[ProtoEnum(Name = "EQUIPMENT_FRAGMENT_LIMIT")]
		EquipmentFragmentLimit,
		[ProtoEnum(Name = "STAMINA_FULL")]
		StaminaFull,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "EXTRA_STORY_CLOSED")]
		ExtraStoryClosed,
		[ProtoEnum(Name = "EQUIPMENT_EXPIRE")]
		EquipmentExpire,
		[ProtoEnum(Name = "BUFF_MATERIAL_FAIL")]
		BuffMaterialFail,
		[ProtoEnum(Name = "ACTIVATE_AVATAR_REPEATE_FAIL")]
		ActivateAvatarRepeateFail,
		[ProtoEnum(Name = "ACTIVATE_AVATAR_LEVEL_LIMIT")]
		ActivateAvatarLevelLimit,
		[ProtoEnum(Name = "NO_ARMADA")]
		NoArmada,
		[ProtoEnum(Name = "MATRIX_CLOSE")]
		MatrixClose,
		[ProtoEnum(Name = "EXPLORE_POWER_LIMIT")]
		ExplorePowerLimit,
		[ProtoEnum(Name = "INVALID_NUM")]
		InvalidNum,
		[ProtoEnum(Name = "TEAM_NOT_EXIST")]
		TeamNotExist,
		[ProtoEnum(Name = "TEAM_FULL_ENERGE")]
		TeamFullEnerge,
		[ProtoEnum(Name = "VITALITY_FULL")]
		VitalityFull,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "AVATAR_NEED")]
		AvatarNeed,
		[ProtoEnum(Name = "CONSUME_ITEM_NOT_EXIST")]
		ConsumeItemNotExist,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_USE")]
		ConsumeItemInUse,
		[ProtoEnum(Name = "CONSUME_ITEM_REPEAT")]
		ConsumeItemRepeat,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED")]
		ConsumeItemProtected,
		[ProtoEnum(Name = "COST_GACHA_TICKET")]
		CostGachaTicket,
		[ProtoEnum(Name = "COST_SENIOR_EQUIP_FROM_NEW_DEVICE")]
		CostSeniorEquipFromNewDevice,
		[ProtoEnum(Name = "COST_SENIOR_EQUIP_CANCEL_PROTECT_TOO_SOON")]
		CostSeniorEquipCancelProtectTooSoon,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_FACILITY")]
		ConsumeItemInFacility,
		[ProtoEnum(Name = "CONSUME_ITEM_GRAND_KEY")]
		ConsumeItemGrandKey,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED_BY_SECURITY_PASSWORD")]
		ConsumeItemProtectedBySecurityPassword
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__Num;

	private uint? __pbn__OverflowItemId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "gift_reward_list")]
	public List<RewardData> GiftRewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(4, Name = "material_id")]
	public uint MaterialId
	{
		get
		{
			return __pbn__MaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialId = value;
		}
	}

	[ProtoMember(5, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	[ProtoMember(6, Name = "overflow_item_id")]
	public uint OverflowItemId
	{
		get
		{
			return __pbn__OverflowItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__OverflowItemId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}

	public bool ShouldSerializeOverflowItemId()
	{
		return __pbn__OverflowItemId.HasValue;
	}

	public void ResetOverflowItemId()
	{
		__pbn__OverflowItemId = null;
	}
}
