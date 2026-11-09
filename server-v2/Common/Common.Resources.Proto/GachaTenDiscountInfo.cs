using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaTenDiscountInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__TicketCostNum;

	private bool? __pbn__IsUsed;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(4, Name = "ticket_cost_num")]
	public uint TicketCostNum
	{
		get
		{
			return __pbn__TicketCostNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TicketCostNum = value;
		}
	}

	[ProtoMember(5, Name = "is_used")]
	public bool IsUsed
	{
		get
		{
			return __pbn__IsUsed.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUsed = value;
		}
	}

	[ProtoMember(6, Name = "gacha_type_list")]
	public uint[] GachaTypeLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeTicketCostNum()
	{
		return __pbn__TicketCostNum.HasValue;
	}

	public void ResetTicketCostNum()
	{
		__pbn__TicketCostNum = null;
	}

	public bool ShouldSerializeIsUsed()
	{
		return __pbn__IsUsed.HasValue;
	}

	public void ResetIsUsed()
	{
		__pbn__IsUsed = null;
	}
}
