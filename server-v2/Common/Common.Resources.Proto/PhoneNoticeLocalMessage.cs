using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PhoneNoticeLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__NoticeScheduleId;

	private uint? __pbn__LastNoticeTime;

	[ProtoMember(1, Name = "notice_schedule_id")]
	public int NoticeScheduleId
	{
		get
		{
			return __pbn__NoticeScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__NoticeScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "last_notice_time")]
	public uint LastNoticeTime
	{
		get
		{
			return __pbn__LastNoticeTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastNoticeTime = value;
		}
	}

	[ProtoMember(3, Name = "finished_perform_list")]
	public int[] FinishedPerformLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNoticeScheduleId()
	{
		return __pbn__NoticeScheduleId.HasValue;
	}

	public void ResetNoticeScheduleId()
	{
		__pbn__NoticeScheduleId = null;
	}

	public bool ShouldSerializeLastNoticeTime()
	{
		return __pbn__LastNoticeTime.HasValue;
	}

	public void ResetLastNoticeTime()
	{
		__pbn__LastNoticeTime = null;
	}
}
