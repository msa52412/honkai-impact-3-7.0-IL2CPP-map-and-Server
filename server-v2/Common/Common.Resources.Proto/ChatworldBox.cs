using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBox : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BoxSeriesId;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__ExpireTime;

	[ProtoMember(1, Name = "box_series_id")]
	public uint BoxSeriesId
	{
		get
		{
			return __pbn__BoxSeriesId.GetValueOrDefault();
		}
		set
		{
			__pbn__BoxSeriesId = value;
		}
	}

	[ProtoMember(3, Name = "schedule_id")]
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

	[ProtoMember(4, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBoxSeriesId()
	{
		return __pbn__BoxSeriesId.HasValue;
	}

	public void ResetBoxSeriesId()
	{
		__pbn__BoxSeriesId = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}
}
