using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipmentSellRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 34
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
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
		[ProtoEnum(Name = "SELL_PRICE")]
		SellPrice,
		[ProtoEnum(Name = "CONSUME_ITEM_GRAND_KEY")]
		ConsumeItemGrandKey,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_FACILITY")]
		ConsumeItemInFacility,
		[ProtoEnum(Name = "LACK_VITALITY")]
		LackVitality,
		[ProtoEnum(Name = "CONTAINS_EQUIP_CAN_NOT_SELL")]
		ContainsEquipCanNotSell,
		[ProtoEnum(Name = "GREEN_STONE_QUANTITY_LIMIT")]
		GreenStoneQuantityLimit,
		[ProtoEnum(Name = "PURPLE_JADE_QUANTITY_LIMIT")]
		PurpleJadeQuantityLimit,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED_BY_SECURITY_PASSWORD")]
		ConsumeItemProtectedBySecurityPassword
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScoinAdd;

	private uint? __pbn__PurpleJadeAdd;

	private uint? __pbn__CostVitality;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "scoin_add")]
	public uint ScoinAdd
	{
		get
		{
			return __pbn__ScoinAdd.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinAdd = value;
		}
	}

	[ProtoMember(4, Name = "purple_jade_add")]
	public uint PurpleJadeAdd
	{
		get
		{
			return __pbn__PurpleJadeAdd.GetValueOrDefault();
		}
		set
		{
			__pbn__PurpleJadeAdd = value;
		}
	}

	[ProtoMember(5, Name = "cost_vitality")]
	public uint CostVitality
	{
		get
		{
			return __pbn__CostVitality.GetValueOrDefault();
		}
		set
		{
			__pbn__CostVitality = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScoinAdd()
	{
		return __pbn__ScoinAdd.HasValue;
	}

	public void ResetScoinAdd()
	{
		__pbn__ScoinAdd = null;
	}

	public bool ShouldSerializePurpleJadeAdd()
	{
		return __pbn__PurpleJadeAdd.HasValue;
	}

	public void ResetPurpleJadeAdd()
	{
		__pbn__PurpleJadeAdd = null;
	}

	public bool ShouldSerializeCostVitality()
	{
		return __pbn__CostVitality.HasValue;
	}

	public void ResetCostVitality()
	{
		__pbn__CostVitality = null;
	}
}
