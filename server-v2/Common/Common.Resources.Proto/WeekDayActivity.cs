using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WeekDayActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__EnterTimes;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__ActivityEndTime;

	private uint? __pbn__ExchangeTimes;

	private uint? __pbn__MpRaidNextRefreshTime;

	private uint? __pbn__MpRaidResetTimes;

	private uint? __pbn__MpMemberTodayEnterTimes;

	private uint? __pbn__ActivityBeginTime;

	private uint? __pbn__ForceOpenTime;

	private uint? __pbn__MpRaidScoreSum;

	private bool? __pbn__IsGetMpRaidSpecialReward;

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

	[ProtoMember(2, Name = "stage_id_list")]
	public uint[] StageIdLists { get; set; }

	[ProtoMember(3, Name = "enter_times")]
	public uint EnterTimes
	{
		get
		{
			return __pbn__EnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterTimes = value;
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

	[ProtoMember(7, Name = "exchange_times")]
	public uint ExchangeTimes
	{
		get
		{
			return __pbn__ExchangeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ExchangeTimes = value;
		}
	}

	[ProtoMember(8, Name = "mp_raid_next_refresh_time")]
	public uint MpRaidNextRefreshTime
	{
		get
		{
			return __pbn__MpRaidNextRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__MpRaidNextRefreshTime = value;
		}
	}

	[ProtoMember(9, Name = "mp_raid_reset_times")]
	public uint MpRaidResetTimes
	{
		get
		{
			return __pbn__MpRaidResetTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MpRaidResetTimes = value;
		}
	}

	[ProtoMember(10, Name = "mp_member_today_enter_times")]
	public uint MpMemberTodayEnterTimes
	{
		get
		{
			return __pbn__MpMemberTodayEnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MpMemberTodayEnterTimes = value;
		}
	}

	[ProtoMember(11, Name = "challenge_reward_granted_list")]
	public uint[] ChallengeRewardGrantedLists { get; set; }

	[ProtoMember(12, Name = "activity_begin_time")]
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

	[ProtoMember(13, Name = "force_open_time")]
	public uint ForceOpenTime
	{
		get
		{
			return __pbn__ForceOpenTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ForceOpenTime = value;
		}
	}

	[ProtoMember(14, Name = "mp_raid_score_sum")]
	public uint MpRaidScoreSum
	{
		get
		{
			return __pbn__MpRaidScoreSum.GetValueOrDefault();
		}
		set
		{
			__pbn__MpRaidScoreSum = value;
		}
	}

	[ProtoMember(15, Name = "is_get_mp_raid_special_reward")]
	public bool IsGetMpRaidSpecialReward
	{
		get
		{
			return __pbn__IsGetMpRaidSpecialReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGetMpRaidSpecialReward = value;
		}
	}

	[ProtoMember(16, Name = "raid_score_reward_list")]
	public uint[] RaidScoreRewardLists { get; set; }

	[ProtoMember(17, Name = "schedule_id")]
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

	public bool ShouldSerializeEnterTimes()
	{
		return __pbn__EnterTimes.HasValue;
	}

	public void ResetEnterTimes()
	{
		__pbn__EnterTimes = null;
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

	public bool ShouldSerializeActivityEndTime()
	{
		return __pbn__ActivityEndTime.HasValue;
	}

	public void ResetActivityEndTime()
	{
		__pbn__ActivityEndTime = null;
	}

	public bool ShouldSerializeExchangeTimes()
	{
		return __pbn__ExchangeTimes.HasValue;
	}

	public void ResetExchangeTimes()
	{
		__pbn__ExchangeTimes = null;
	}

	public bool ShouldSerializeMpRaidNextRefreshTime()
	{
		return __pbn__MpRaidNextRefreshTime.HasValue;
	}

	public void ResetMpRaidNextRefreshTime()
	{
		__pbn__MpRaidNextRefreshTime = null;
	}

	public bool ShouldSerializeMpRaidResetTimes()
	{
		return __pbn__MpRaidResetTimes.HasValue;
	}

	public void ResetMpRaidResetTimes()
	{
		__pbn__MpRaidResetTimes = null;
	}

	public bool ShouldSerializeMpMemberTodayEnterTimes()
	{
		return __pbn__MpMemberTodayEnterTimes.HasValue;
	}

	public void ResetMpMemberTodayEnterTimes()
	{
		__pbn__MpMemberTodayEnterTimes = null;
	}

	public bool ShouldSerializeActivityBeginTime()
	{
		return __pbn__ActivityBeginTime.HasValue;
	}

	public void ResetActivityBeginTime()
	{
		__pbn__ActivityBeginTime = null;
	}

	public bool ShouldSerializeForceOpenTime()
	{
		return __pbn__ForceOpenTime.HasValue;
	}

	public void ResetForceOpenTime()
	{
		__pbn__ForceOpenTime = null;
	}

	public bool ShouldSerializeMpRaidScoreSum()
	{
		return __pbn__MpRaidScoreSum.HasValue;
	}

	public void ResetMpRaidScoreSum()
	{
		__pbn__MpRaidScoreSum = null;
	}

	public bool ShouldSerializeIsGetMpRaidSpecialReward()
	{
		return __pbn__IsGetMpRaidSpecialReward.HasValue;
	}

	public void ResetIsGetMpRaidSpecialReward()
	{
		__pbn__IsGetMpRaidSpecialReward = null;
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
