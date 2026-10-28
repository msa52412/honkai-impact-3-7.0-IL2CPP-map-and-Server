using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LoginPostData : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__AllShowTimes;

	private uint? __pbn__BeginTimeOfRecordDay;

	private int? __pbn__ShowTimesInRecordDay;

	[ProtoMember(1, Name = "all_show_times")]
	public int AllShowTimes
	{
		get
		{
			return __pbn__AllShowTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__AllShowTimes = value;
		}
	}

	[ProtoMember(2, Name = "begin_time_of_record_day")]
	public uint BeginTimeOfRecordDay
	{
		get
		{
			return __pbn__BeginTimeOfRecordDay.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTimeOfRecordDay = value;
		}
	}

	[ProtoMember(3, Name = "show_times_in_record_day")]
	public int ShowTimesInRecordDay
	{
		get
		{
			return __pbn__ShowTimesInRecordDay.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowTimesInRecordDay = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAllShowTimes()
	{
		return __pbn__AllShowTimes.HasValue;
	}

	public void ResetAllShowTimes()
	{
		__pbn__AllShowTimes = null;
	}

	public bool ShouldSerializeBeginTimeOfRecordDay()
	{
		return __pbn__BeginTimeOfRecordDay.HasValue;
	}

	public void ResetBeginTimeOfRecordDay()
	{
		__pbn__BeginTimeOfRecordDay = null;
	}

	public bool ShouldSerializeShowTimesInRecordDay()
	{
		return __pbn__ShowTimesInRecordDay.HasValue;
	}

	public void ResetShowTimesInRecordDay()
	{
		__pbn__ShowTimesInRecordDay = null;
	}
}
