using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BackEquipmentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3664
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
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
		[ProtoEnum(Name = "EQUIPMENT_NOT_OPEN")]
		EquipmentNotOpen,
		[ProtoEnum(Name = "TIMES_LACK")]
		TimesLack,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_FACILITY")]
		ConsumeItemInFacility,
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

	private uint? __pbn__RemainBackTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "unique_id_list")]
	public uint[] UniqueIdLists { get; set; }

	[ProtoMember(3, Name = "material_list")]
	public List<Material> MaterialLists { get; } = new List<Material>();


	[ProtoMember(4, Name = "remain_back_times")]
	public uint RemainBackTimes
	{
		get
		{
			return __pbn__RemainBackTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RemainBackTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRemainBackTimes()
	{
		return __pbn__RemainBackTimes.HasValue;
	}

	public void ResetRemainBackTimes()
	{
		__pbn__RemainBackTimes = null;
	}
}
