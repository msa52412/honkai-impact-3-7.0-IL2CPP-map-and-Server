using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalWarActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScheduleId;

	private uint? __pbn__DisplayBeginTime;

	private uint? __pbn__DisplayEndTime;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__TotalGetCurrency;

	private uint? __pbn__LastDailyGetCurrency;

	private uint? __pbn__TotalCostTicketNum;

	private uint? __pbn__BuffLevel;

	[ProtoMember(1, Name = "cur_schedule_id")]
	public uint CurScheduleId
	{
		get
		{
			return __pbn__CurScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "display_begin_time")]
	public uint DisplayBeginTime
	{
		get
		{
			return __pbn__DisplayBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayBeginTime = value;
		}
	}

	[ProtoMember(3, Name = "display_end_time")]
	public uint DisplayEndTime
	{
		get
		{
			return __pbn__DisplayEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayEndTime = value;
		}
	}

	[ProtoMember(4, Name = "begin_time")]
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

	[ProtoMember(5, Name = "end_time")]
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

	[ProtoMember(6, Name = "total_get_currency")]
	public uint TotalGetCurrency
	{
		get
		{
			return __pbn__TotalGetCurrency.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalGetCurrency = value;
		}
	}

	[ProtoMember(7, Name = "last_daily_get_currency")]
	public uint LastDailyGetCurrency
	{
		get
		{
			return __pbn__LastDailyGetCurrency.GetValueOrDefault();
		}
		set
		{
			__pbn__LastDailyGetCurrency = value;
		}
	}

	[ProtoMember(8, Name = "total_cost_ticket_num")]
	public uint TotalCostTicketNum
	{
		get
		{
			return __pbn__TotalCostTicketNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalCostTicketNum = value;
		}
	}

	[ProtoMember(9, Name = "take_point_reward_list")]
	public uint[] TakePointRewardLists { get; set; }

	[ProtoMember(10, Name = "global_war_point_list")]
	public List<GlobalWarPoint> GlobalWarPointLists { get; } = new List<GlobalWarPoint>();


	[ProtoMember(11, Name = "member_list")]
	public List<GlobalWarMember> MemberLists { get; } = new List<GlobalWarMember>();


	[ProtoMember(13, Name = "buff_level")]
	public uint BuffLevel
	{
		get
		{
			return __pbn__BuffLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffLevel = value;
		}
	}

	[ProtoMember(14, Name = "global_war_isolate_point_list")]
	public List<GlobalWarIsolatePoint> GlobalWarIsolatePointLists { get; } = new List<GlobalWarIsolatePoint>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurScheduleId()
	{
		return __pbn__CurScheduleId.HasValue;
	}

	public void ResetCurScheduleId()
	{
		__pbn__CurScheduleId = null;
	}

	public bool ShouldSerializeDisplayBeginTime()
	{
		return __pbn__DisplayBeginTime.HasValue;
	}

	public void ResetDisplayBeginTime()
	{
		__pbn__DisplayBeginTime = null;
	}

	public bool ShouldSerializeDisplayEndTime()
	{
		return __pbn__DisplayEndTime.HasValue;
	}

	public void ResetDisplayEndTime()
	{
		__pbn__DisplayEndTime = null;
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

	public bool ShouldSerializeTotalGetCurrency()
	{
		return __pbn__TotalGetCurrency.HasValue;
	}

	public void ResetTotalGetCurrency()
	{
		__pbn__TotalGetCurrency = null;
	}

	public bool ShouldSerializeLastDailyGetCurrency()
	{
		return __pbn__LastDailyGetCurrency.HasValue;
	}

	public void ResetLastDailyGetCurrency()
	{
		__pbn__LastDailyGetCurrency = null;
	}

	public bool ShouldSerializeTotalCostTicketNum()
	{
		return __pbn__TotalCostTicketNum.HasValue;
	}

	public void ResetTotalCostTicketNum()
	{
		__pbn__TotalCostTicketNum = null;
	}

	public bool ShouldSerializeBuffLevel()
	{
		return __pbn__BuffLevel.HasValue;
	}

	public void ResetBuffLevel()
	{
		__pbn__BuffLevel = null;
	}
}
