using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BurdenAlleviation : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AlleviationId;

	private uint? __pbn__ShowBeginTime;

	private uint? __pbn__ShowEndTime;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__AlleviateTimes;

	[ProtoMember(1, Name = "alleviation_id")]
	public uint AlleviationId
	{
		get
		{
			return __pbn__AlleviationId.GetValueOrDefault();
		}
		set
		{
			__pbn__AlleviationId = value;
		}
	}

	[ProtoMember(2, Name = "show_begin_time")]
	public uint ShowBeginTime
	{
		get
		{
			return __pbn__ShowBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowBeginTime = value;
		}
	}

	[ProtoMember(3, Name = "show_end_time")]
	public uint ShowEndTime
	{
		get
		{
			return __pbn__ShowEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowEndTime = value;
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

	[ProtoMember(6, Name = "alleviate_times")]
	public uint AlleviateTimes
	{
		get
		{
			return __pbn__AlleviateTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__AlleviateTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAlleviationId()
	{
		return __pbn__AlleviationId.HasValue;
	}

	public void ResetAlleviationId()
	{
		__pbn__AlleviationId = null;
	}

	public bool ShouldSerializeShowBeginTime()
	{
		return __pbn__ShowBeginTime.HasValue;
	}

	public void ResetShowBeginTime()
	{
		__pbn__ShowBeginTime = null;
	}

	public bool ShouldSerializeShowEndTime()
	{
		return __pbn__ShowEndTime.HasValue;
	}

	public void ResetShowEndTime()
	{
		__pbn__ShowEndTime = null;
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

	public bool ShouldSerializeAlleviateTimes()
	{
		return __pbn__AlleviateTimes.HasValue;
	}

	public void ResetAlleviateTimes()
	{
		__pbn__AlleviateTimes = null;
	}
}
