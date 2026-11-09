using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReforgeEquipmentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3658
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "JUST_SUPPORT_WEAPON")]
		JustSupportWeapon,
		[ProtoEnum(Name = "CONSUME_ITEM_NOT_EXIST")]
		ConsumeItemNotExist,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_USE")]
		ConsumeItemInUse,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED")]
		ConsumeItemProtected,
		[ProtoEnum(Name = "COST_SENIOR_EQUIP_FROM_NEW_DEVICE")]
		CostSeniorEquipFromNewDevice,
		[ProtoEnum(Name = "COST_SENIOR_EQUIP_CANCEL_PROTECT_TOO_SOON")]
		CostSeniorEquipCancelProtectTooSoon,
		[ProtoEnum(Name = "EQUIPMENT_NOT_EXTRACTED")]
		EquipmentNotExtracted,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "EQUIPMENT_NOT_OPEN")]
		EquipmentNotOpen,
		[ProtoEnum(Name = "TIMES_LACK")]
		TimesLack,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_FACILITY")]
		ConsumeItemInFacility,
		[ProtoEnum(Name = "TARGET_NOT_FITTED")]
		TargetNotFitted,
		[ProtoEnum(Name = "CONSUME_ITEM_GRAND_KEY")]
		ConsumeItemGrandKey,
		[ProtoEnum(Name = "CONSUME_ITEM_REPEAT")]
		ConsumeItemRepeat,
		[ProtoEnum(Name = "COST_GACHA_TICKET")]
		CostGachaTicket,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED_BY_SECURITY_PASSWORD")]
		ConsumeItemProtectedBySecurityPassword
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__RemainReforgeTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(3, Name = "remain_reforge_times")]
	public uint RemainReforgeTimes
	{
		get
		{
			return __pbn__RemainReforgeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RemainReforgeTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeRemainReforgeTimes()
	{
		return __pbn__RemainReforgeTimes.HasValue;
	}

	public void ResetRemainReforgeTimes()
	{
		__pbn__RemainReforgeTimes = null;
	}
}
