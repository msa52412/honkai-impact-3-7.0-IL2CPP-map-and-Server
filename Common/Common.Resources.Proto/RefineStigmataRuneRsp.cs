using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RefineStigmataRuneRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 194
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STIGMATA_NOT_EXIST")]
		StigmataNotExist,
		[ProtoEnum(Name = "STIGMATA_NOT_IDENTIFY")]
		StigmataNotIdentify,
		[ProtoEnum(Name = "STIGMATA_CAN_NOT_REFINE")]
		StigmataCanNotRefine,
		[ProtoEnum(Name = "STIGMATA_WAIT_RUNE_EXIST")]
		StigmataWaitRuneExist,
		[ProtoEnum(Name = "CONSUME_ITEM_NOT_EXIST")]
		ConsumeItemNotExist,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_USE")]
		ConsumeItemInUse,
		[ProtoEnum(Name = "CONSUME_ITEM_REPEAT")]
		ConsumeItemRepeat,
		[ProtoEnum(Name = "CONSUME_ITEM_UN_NECESSARY")]
		ConsumeItemUnNecessary,
		[ProtoEnum(Name = "CONSUME_ITEM_LACK")]
		ConsumeItemLack,
		[ProtoEnum(Name = "ITEM_IN_CONSUME_ITEM_LIST")]
		ItemInConsumeItemList,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED")]
		ConsumeItemProtected,
		[ProtoEnum(Name = "COST_GACHA_TICKET")]
		CostGachaTicket,
		[ProtoEnum(Name = "COST_SENIOR_EQUIP_FROM_NEW_DEVICE")]
		CostSeniorEquipFromNewDevice,
		[ProtoEnum(Name = "COST_SENIOR_EQUIP_CANCEL_PROTECT_TOO_SOON")]
		CostSeniorEquipCancelProtectTooSoon,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_FACILITY")]
		ConsumeItemInFacility,
		[ProtoEnum(Name = "CONSUME_ITEM_GRAND_KEY")]
		ConsumeItemGrandKey,
		[ProtoEnum(Name = "SLOT_LACK")]
		SlotLack,
		[ProtoEnum(Name = "TRIGGER_MATERIAL_LACK")]
		TriggerMaterialLack,
		[ProtoEnum(Name = "LOCK_INDEX_ERROR")]
		LockIndexError,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED_BY_SECURITY_PASSWORD")]
		ConsumeItemProtectedBySecurityPassword,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "TOO_MANY_WAIT_RUNE")]
		TooManyWaitRune
	}

	private IExtension __pbn__extensionData;

	private StigmataRefineTimesType? __pbn__TimesType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "rune_list")]
	public List<StigmataRune> RuneLists { get; } = new List<StigmataRune>();


	[ProtoMember(3, Name = "times_type")]
	[DefaultValue(StigmataRefineTimesType.StigmataRefineTimesOne)]
	public StigmataRefineTimesType TimesType
	{
		get
		{
			return __pbn__TimesType ?? StigmataRefineTimesType.StigmataRefineTimesOne;
		}
		set
		{
			__pbn__TimesType = value;
		}
	}

	[ProtoMember(4, Name = "rune_group_list")]
	public List<StigmataRuneGroup> RuneGroupLists { get; } = new List<StigmataRuneGroup>();


	[ProtoMember(5, Name = "wait_select_stgimata_list")]
	public uint[] WaitSelectStgimataLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTimesType()
	{
		return __pbn__TimesType.HasValue;
	}

	public void ResetTimesType()
	{
		__pbn__TimesType = null;
	}
}
