using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExBossInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EnterTimes;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__RankId;

	private uint? __pbn__NowScheduleId;

	private uint? __pbn__MaxSweepLevel;

	[ProtoMember(1, Name = "enter_times")]
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

	[ProtoMember(3, Name = "rank_id")]
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

	[ProtoMember(4, Name = "boss_id_list")]
	public List<ExBossIdInfo> BossIdLists { get; } = new List<ExBossIdInfo>();


	[ProtoMember(5, Name = "now_schedule_id")]
	public uint NowScheduleId
	{
		get
		{
			return __pbn__NowScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__NowScheduleId = value;
		}
	}

	[ProtoMember(6, Name = "ex_boss_list")]
	public List<ExBossIdInfo> ExBossLists { get; } = new List<ExBossIdInfo>();


	[ProtoMember(7, Name = "max_sweep_level")]
	public uint MaxSweepLevel
	{
		get
		{
			return __pbn__MaxSweepLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxSweepLevel = value;
		}
	}

	[ProtoMember(8, Name = "single_boss_score_reward_list")]
	public List<ExBossSingleBossScoreReward> SingleBossScoreRewardLists { get; } = new List<ExBossSingleBossScoreReward>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEnterTimes()
	{
		return __pbn__EnterTimes.HasValue;
	}

	public void ResetEnterTimes()
	{
		__pbn__EnterTimes = null;
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

	public bool ShouldSerializeNowScheduleId()
	{
		return __pbn__NowScheduleId.HasValue;
	}

	public void ResetNowScheduleId()
	{
		__pbn__NowScheduleId = null;
	}

	public bool ShouldSerializeMaxSweepLevel()
	{
		return __pbn__MaxSweepLevel.HasValue;
	}

	public void ResetMaxSweepLevel()
	{
		__pbn__MaxSweepLevel = null;
	}
}
