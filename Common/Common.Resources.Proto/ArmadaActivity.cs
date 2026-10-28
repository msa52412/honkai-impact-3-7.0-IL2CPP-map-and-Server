using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private ArmadaActivityType? __pbn__Type;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__OpenDayTime;

	private uint? __pbn__CloseDayTime;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ArmadaActivityType.ArmadaActivityTypeNone)]
	public ArmadaActivityType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "schedule_id")]
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

	[ProtoMember(5, Name = "open_day_time")]
	public uint OpenDayTime
	{
		get
		{
			return __pbn__OpenDayTime.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenDayTime = value;
		}
	}

	[ProtoMember(6, Name = "close_day_time")]
	public uint CloseDayTime
	{
		get
		{
			return __pbn__CloseDayTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CloseDayTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
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

	public bool ShouldSerializeOpenDayTime()
	{
		return __pbn__OpenDayTime.HasValue;
	}

	public void ResetOpenDayTime()
	{
		__pbn__OpenDayTime = null;
	}

	public bool ShouldSerializeCloseDayTime()
	{
		return __pbn__CloseDayTime.HasValue;
	}

	public void ResetCloseDayTime()
	{
		__pbn__CloseDayTime = null;
	}
}
