using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ForgeEquipmentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3653
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit,
		[ProtoEnum(Name = "NO_TIMES")]
		NoTimes,
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
		[ProtoEnum(Name = "CONSUME_ITEM_GRAND_KEY")]
		ConsumeItemGrandKey,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_FACILITY")]
		ConsumeItemInFacility,
		[ProtoEnum(Name = "CURRENCY_LACK")]
		CurrencyLack,
		[ProtoEnum(Name = "CONSUME_ITEM_MATCHING_REDUNDANT")]
		ConsumeItemMatchingRedundant,
		[ProtoEnum(Name = "CONSUME_ITEM_MATCHING_LACK")]
		ConsumeItemMatchingLack,
		[ProtoEnum(Name = "CONSUME_WEAPON_LOCKED")]
		ConsumeWeaponLocked,
		[ProtoEnum(Name = "QUANTUM_WEAPON_NUM_LIMIT")]
		QuantumWeaponNumLimit,
		[ProtoEnum(Name = "QUANTUM_FORGE_CLOSED")]
		QuantumForgeClosed,
		[ProtoEnum(Name = "WAIT_SELECTED_RUNE")]
		WaitSelectedRune,
		[ProtoEnum(Name = "NO_RUNE")]
		NoRune,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED_BY_SECURITY_PASSWORD")]
		ConsumeItemProtectedBySecurityPassword,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ForgeId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "forge_id")]
	public uint ForgeId
	{
		get
		{
			return __pbn__ForgeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ForgeId = value;
		}
	}

	[ProtoMember(3, Name = "return_material_list")]
	public List<Material> ReturnMaterialLists { get; } = new List<Material>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeForgeId()
	{
		return __pbn__ForgeId.HasValue;
	}

	public void ResetForgeId()
	{
		__pbn__ForgeId = null;
	}
}
