using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CityActivitySite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__SiteBeginTime;

	private uint? __pbn__SiteEndTime;

	private uint? __pbn__ActivityScheduleId;

	private uint? __pbn__ActivityBeginTime;

	private uint? __pbn__ActivityEndTime;

	[ProtoMember(1, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
		}
	}

	[ProtoMember(2, Name = "site_begin_time")]
	public uint SiteBeginTime
	{
		get
		{
			return __pbn__SiteBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteBeginTime = value;
		}
	}

	[ProtoMember(3, Name = "site_end_time")]
	public uint SiteEndTime
	{
		get
		{
			return __pbn__SiteEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteEndTime = value;
		}
	}

	[ProtoMember(4, Name = "activity_schedule_id")]
	public uint ActivityScheduleId
	{
		get
		{
			return __pbn__ActivityScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityScheduleId = value;
		}
	}

	[ProtoMember(5, Name = "activity_begin_time")]
	public uint ActivityBeginTime
	{
		get
		{
			return __pbn__ActivityBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityBeginTime = value;
		}
	}

	[ProtoMember(6, Name = "activity_end_time")]
	public uint ActivityEndTime
	{
		get
		{
			return __pbn__ActivityEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
	}

	public bool ShouldSerializeSiteBeginTime()
	{
		return __pbn__SiteBeginTime.HasValue;
	}

	public void ResetSiteBeginTime()
	{
		__pbn__SiteBeginTime = null;
	}

	public bool ShouldSerializeSiteEndTime()
	{
		return __pbn__SiteEndTime.HasValue;
	}

	public void ResetSiteEndTime()
	{
		__pbn__SiteEndTime = null;
	}

	public bool ShouldSerializeActivityScheduleId()
	{
		return __pbn__ActivityScheduleId.HasValue;
	}

	public void ResetActivityScheduleId()
	{
		__pbn__ActivityScheduleId = null;
	}

	public bool ShouldSerializeActivityBeginTime()
	{
		return __pbn__ActivityBeginTime.HasValue;
	}

	public void ResetActivityBeginTime()
	{
		__pbn__ActivityBeginTime = null;
	}

	public bool ShouldSerializeActivityEndTime()
	{
		return __pbn__ActivityEndTime.HasValue;
	}

	public void ResetActivityEndTime()
	{
		__pbn__ActivityEndTime = null;
	}
}
