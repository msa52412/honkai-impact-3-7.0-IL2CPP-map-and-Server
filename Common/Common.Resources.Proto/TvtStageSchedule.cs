using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtStageSchedule : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__DayBeginTime;

	private uint? __pbn__DayEndTime;

	[ProtoMember(2, Name = "day_begin_time")]
	public uint DayBeginTime
	{
		get
		{
			return __pbn__DayBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DayBeginTime = value;
		}
	}

	[ProtoMember(3, Name = "day_end_time")]
	public uint DayEndTime
	{
		get
		{
			return __pbn__DayEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DayEndTime = value;
		}
	}

	[ProtoMember(4, Name = "stage_id_list")]
	public uint[] StageIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDayBeginTime()
	{
		return __pbn__DayBeginTime.HasValue;
	}

	public void ResetDayBeginTime()
	{
		__pbn__DayBeginTime = null;
	}

	public bool ShouldSerializeDayEndTime()
	{
		return __pbn__DayEndTime.HasValue;
	}

	public void ResetDayEndTime()
	{
		__pbn__DayEndTime = null;
	}
}
