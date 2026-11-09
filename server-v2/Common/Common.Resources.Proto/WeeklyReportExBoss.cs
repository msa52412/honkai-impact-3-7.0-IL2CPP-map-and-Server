using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WeeklyReportExBoss : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__RankId;

	private uint? __pbn__TimesScore;

	private uint? __pbn__RankPercent;

	private uint? __pbn__Rank;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

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

	[ProtoMember(2, Name = "rank_id")]
	public uint RankId
	{
		get
		{
			return __pbn__RankId.GetValueOrDefault();
		}
		set
		{
			__pbn__RankId = value;
		}
	}

	[ProtoMember(3, Name = "times_score")]
	public uint TimesScore
	{
		get
		{
			return __pbn__TimesScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TimesScore = value;
		}
	}

	[ProtoMember(4, Name = "boss_group_list")]
	public List<WeeklyReportExBossGroup> BossGroupLists { get; } = new List<WeeklyReportExBossGroup>();


	[ProtoMember(5, Name = "rank_percent")]
	public uint RankPercent
	{
		get
		{
			return __pbn__RankPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__RankPercent = value;
		}
	}

	[ProtoMember(6, Name = "rank")]
	public uint Rank
	{
		get
		{
			return __pbn__Rank.GetValueOrDefault();
		}
		set
		{
			__pbn__Rank = value;
		}
	}

	[ProtoMember(7, Name = "begin_time")]
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

	[ProtoMember(8, Name = "end_time")]
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

	[ProtoMember(9, Name = "reward_id_list")]
	public uint[] RewardIdLists { get; set; }

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

	public bool ShouldSerializeRankId()
	{
		return __pbn__RankId.HasValue;
	}

	public void ResetRankId()
	{
		__pbn__RankId = null;
	}

	public bool ShouldSerializeTimesScore()
	{
		return __pbn__TimesScore.HasValue;
	}

	public void ResetTimesScore()
	{
		__pbn__TimesScore = null;
	}

	public bool ShouldSerializeRankPercent()
	{
		return __pbn__RankPercent.HasValue;
	}

	public void ResetRankPercent()
	{
		__pbn__RankPercent = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
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
}
