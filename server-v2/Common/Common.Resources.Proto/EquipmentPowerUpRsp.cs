using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipmentPowerUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 0x20
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAIN_ITEM_NOT_EXIST")]
		MainItemNotExist,
		[ProtoEnum(Name = "MAIN_ITEM_LEVEL_FULL")]
		MainItemLevelFull,
		[ProtoEnum(Name = "CONSUME_ITEM_NOT_EXIST")]
		ConsumeItemNotExist,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_USE")]
		ConsumeItemInUse,
		[ProtoEnum(Name = "CONSUME_ITEM_REPEAT")]
		ConsumeItemRepeat,
		[ProtoEnum(Name = "MAIN_ITEM_IN_CONSUME_ITEM_LIST")]
		MainItemInConsumeItemList,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED")]
		ConsumeItemProtected,
		[ProtoEnum(Name = "MAIN_ITEM_AFFIX_NOT_IDENTIFY")]
		MainItemAffixNotIdentify,
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

	private uint? __pbn__BoostRate;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "main_item")]
	public EquipmentItem MainItem { get; set; }

	[ProtoMember(3, Name = "boost_rate")]
	public uint BoostRate
	{
		get
		{
			return __pbn__BoostRate.GetValueOrDefault();
		}
		set
		{
			__pbn__BoostRate = value;
		}
	}

	[ProtoMember(4, Name = "return_material_list")]
	public List<Material> ReturnMaterialLists { get; } = new List<Material>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBoostRate()
	{
		return __pbn__BoostRate.HasValue;
	}

	public void ResetBoostRate()
	{
		__pbn__BoostRate = null;
	}
}
