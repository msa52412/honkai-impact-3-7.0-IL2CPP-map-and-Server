using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldActivitySchedule : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__BeginDayTime;

	private uint? __pbn__EndDayTime;

	[ProtoMember(1, Name = "schedule_id")]
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

	[ProtoMember(2, Name = "activity_id")]
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

	[ProtoMember(4, Name = "end_time")]
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

	[ProtoMember(5, Name = "begin_day_time")]
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

	[ProtoMember(6, Name = "end_day_time")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
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
}
