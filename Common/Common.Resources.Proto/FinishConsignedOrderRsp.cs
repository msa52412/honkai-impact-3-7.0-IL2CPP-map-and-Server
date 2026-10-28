using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishConsignedOrderRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2644
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "IN_CD_TIME")]
		InCdTime,
		[ProtoEnum(Name = "TIMES_LACK")]
		TimesLack,
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
		[ProtoEnum(Name = "ITEM_LACK")]
		ItemLack,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError,
		[ProtoEnum(Name = "ORDER_FINISHED")]
		OrderFinished,
		[ProtoEnum(Name = "ORDER_PROGRESS_CHANGE")]
		OrderProgressChange,
		[ProtoEnum(Name = "CONSUME_ITEM_IN_FACILITY")]
		ConsumeItemInFacility,
		[ProtoEnum(Name = "CONSUME_ITEM_GRAND_KEY")]
		ConsumeItemGrandKey,
		[ProtoEnum(Name = "CONSUME_ITEM_PROTECTED_BY_SECURITY_PASSWORD")]
		ConsumeItemProtectedBySecurityPassword
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ConsignId;

	private uint? __pbn__ReliableId;

	private uint? __pbn__Progress;

	private uint? __pbn__FinishOrderTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(4, Name = "item")]
	public EquipmentItem Item { get; set; }

	[ProtoMember(5, Name = "consign_id")]
	public uint ConsignId
	{
		get
		{
			return __pbn__ConsignId.GetValueOrDefault();
		}
		set
		{
			__pbn__ConsignId = value;
		}
	}

	[ProtoMember(8, Name = "reliable_id")]
	public uint ReliableId
	{
		get
		{
			return __pbn__ReliableId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReliableId = value;
		}
	}

	[ProtoMember(9, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(10, Name = "finish_order_times")]
	public uint FinishOrderTimes
	{
		get
		{
			return __pbn__FinishOrderTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishOrderTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeConsignId()
	{
		return __pbn__ConsignId.HasValue;
	}

	public void ResetConsignId()
	{
		__pbn__ConsignId = null;
	}

	public bool ShouldSerializeReliableId()
	{
		return __pbn__ReliableId.HasValue;
	}

	public void ResetReliableId()
	{
		__pbn__ReliableId = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeFinishOrderTimes()
	{
		return __pbn__FinishOrderTimes.HasValue;
	}

	public void ResetFinishOrderTimes()
	{
		__pbn__FinishOrderTimes = null;
	}
}
