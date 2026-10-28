using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralActivityScheduleInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ShowTime;

	private uint? __pbn__BeginTime;

	private uint? __pbn__SettleTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__BeginDayTime;

	private uint? __pbn__EndDayTime;

	private uint? __pbn__LeftTimes;

	private uint? __pbn__ScheduleId;

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

	[ProtoMember(2, Name = "show_time")]
	public uint ShowTime
	{
		get
		{
			return __pbn__ShowTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowTime = value;
		}
	}

	[ProtoMember(3, Name = "begin_time")]
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

	[ProtoMember(4, Name = "settle_time")]
	public uint SettleTime
	{
		get
		{
			return __pbn__SettleTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SettleTime = value;
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

	[ProtoMember(6, Name = "begin_day_time")]
	public uint BeginDayTime
	{
		get
		{
			return __pbn__BeginDayTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginDayTime = value;
		}
	}

	[ProtoMember(7, Name = "end_day_time")]
	public uint EndDayTime
	{
		get
		{
			return __pbn__EndDayTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndDayTime = value;
		}
	}

	[ProtoMember(8, Name = "left_times")]
	public uint LeftTimes
	{
		get
		{
			return __pbn__LeftTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftTimes = value;
		}
	}

	[ProtoMember(9, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

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

	public bool ShouldSerializeShowTime()
	{
		return __pbn__ShowTime.HasValue;
	}

	public void ResetShowTime()
	{
		__pbn__ShowTime = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeSettleTime()
	{
		return __pbn__SettleTime.HasValue;
	}

	public void ResetSettleTime()
	{
		__pbn__SettleTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeBeginDayTime()
	{
		return __pbn__BeginDayTime.HasValue;
	}

	public void ResetBeginDayTime()
	{
		__pbn__BeginDayTime = null;
	}

	public bool ShouldSerializeEndDayTime()
	{
		return __pbn__EndDayTime.HasValue;
	}

	public void ResetEndDayTime()
	{
		__pbn__EndDayTime = null;
	}

	public bool ShouldSerializeLeftTimes()
	{
		return __pbn__LeftTimes.HasValue;
	}

	public void ResetLeftTimes()
	{
		__pbn__LeftTimes = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}
}
