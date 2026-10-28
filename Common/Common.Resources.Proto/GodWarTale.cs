using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarTale : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__CurSiteId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__AvatarScheduleId;

	private bool? __pbn__IsLocked;

	[ProtoMember(1, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
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

	[ProtoMember(3, Name = "site_list")]
	public List<GodWarSite> SiteLists { get; } = new List<GodWarSite>();


	[ProtoMember(4, Name = "cur_site_id")]
	public uint CurSiteId
	{
		get
		{
			return __pbn__CurSiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurSiteId = value;
		}
	}

	[ProtoMember(5, Name = "begin_time")]
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

	[ProtoMember(6, Name = "end_time")]
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

	[ProtoMember(7, Name = "challenge")]
	public GodWarChallenge Challenge { get; set; }

	[ProtoMember(8, Name = "buff_list")]
	public List<GodWarBuff> BuffLists { get; } = new List<GodWarBuff>();


	[ProtoMember(9, Name = "avatar_schedule_id")]
	public uint AvatarScheduleId
	{
		get
		{
			return __pbn__AvatarScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarScheduleId = value;
		}
	}

	[ProtoMember(10, Name = "challenge_history_list")]
	public List<GodWarChallengeHistory> ChallengeHistoryLists { get; } = new List<GodWarChallengeHistory>();


	[ProtoMember(11, Name = "is_locked")]
	public bool IsLocked
	{
		get
		{
			return __pbn__IsLocked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLocked = value;
		}
	}

	[ProtoMember(12, Name = "overall_list")]
	public List<GodWarOverall> OverallLists { get; } = new List<GodWarOverall>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeCurSiteId()
	{
		return __pbn__CurSiteId.HasValue;
	}

	public void ResetCurSiteId()
	{
		__pbn__CurSiteId = null;
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

	public bool ShouldSerializeAvatarScheduleId()
	{
		return __pbn__AvatarScheduleId.HasValue;
	}

	public void ResetAvatarScheduleId()
	{
		__pbn__AvatarScheduleId = null;
	}

	public bool ShouldSerializeIsLocked()
	{
		return __pbn__IsLocked.HasValue;
	}

	public void ResetIsLocked()
	{
		__pbn__IsLocked = null;
	}
}
