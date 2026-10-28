using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class IslandDisjoinEquipmentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 180
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CABIN_TECH_NOT_ADD")]
		CabinTechNotAdd,
		[ProtoEnum(Name = "CONSUME_ITEM_NOT_EXIST")]
		ConsumeItemNotExist,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_USE")]
		ConsumeItemInUse,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED")]
		ConsumeItemProtected,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "COST_SENIOR_EQUIP_FROM_NEW_DEVICE")]
		CostSeniorEquipFromNewDevice,
		[ProtoEnum(Name = "COST_SENIOR_EQUIP_CANCEL_PROTECT_TOO_SOON")]
		CostSeniorEquipCancelProtectTooSoon,
		[ProtoEnum(Name = "TITLE_LEVEL_ERROR")]
		TitleLevelError,
		[ProtoEnum(Name = "BATCH_NUM")]
		BatchNum,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "CONSUME_ITEM_REPEAT")]
		ConsumeItemRepeat,
		[ProtoEnum(Name = "COST_GACHA_TICKET")]
		CostGachaTicket,
		[ProtoEnum(Name = "CONSUME_ITEM_GRAND_KEY")]
		ConsumeItemGrandKey,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_FACILITY")]
		ConsumeItemInFacility,
		[ProtoEnum(Name = "EX_DISJOIN_CURRENCY_LACK")]
		ExDisjoinCurrencyLack,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED_BY_SECURITY_PASSWORD")]
		ConsumeItemProtectedBySecurityPassword
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "disjoin_unique_id_list")]
	public uint[] DisjoinUniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
