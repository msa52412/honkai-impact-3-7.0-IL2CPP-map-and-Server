using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WeeklyReportEndless : IExtensible
{
	private IExtension __pbn__extensionData;

	private EndlessType? __pbn__EndlessType;

	private uint? __pbn__BeginTime;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__FinalBossId;

	private uint? __pbn__Score;

	private uint? __pbn__RewardProgress;

	private EndlessRewardType? __pbn__RewardType;

	private uint? __pbn__SingleModeScore;

	private uint? __pbn__BattleConfigId;

	private uint? __pbn__SingleModeActivityId;

	private uint? __pbn__EndTime;

	private uint? __pbn__GreedyMaxFinishFloor;

	[ProtoMember(1, Name = "endless_type")]
	[DefaultValue(EndlessType.EndlessTypeNone)]
	public EndlessType EndlessType
	{
		get
		{
			return __pbn__EndlessType.GetValueOrDefault();
		}
		set
		{
			__pbn__EndlessType = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
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

	[ProtoMember(3, Name = "group_level")]
	public uint GroupLevel
	{
		get
		{
			return __pbn__GroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupLevel = value;
		}
	}

	[ProtoMember(4, Name = "final_boss_id")]
	public uint FinalBossId
	{
		get
		{
			return __pbn__FinalBossId.GetValueOrDefault();
		}
		set
		{
			__pbn__FinalBossId = value;
		}
	}

	[ProtoMember(6, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(7, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

	[ProtoMember(8, Name = "reward_progress")]
	public uint RewardProgress
	{
		get
		{
			return __pbn__RewardProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardProgress = value;
		}
	}

	[ProtoMember(9, Name = "reward_type")]
	[DefaultValue(EndlessRewardType.EndlessPromote)]
	public EndlessRewardType RewardType
	{
		get
		{
			return __pbn__RewardType ?? EndlessRewardType.EndlessPromote;
		}
		set
		{
			__pbn__RewardType = value;
		}
	}

	[ProtoMember(10, Name = "single_mode_score")]
	public uint SingleModeScore
	{
		get
		{
			return __pbn__SingleModeScore.GetValueOrDefault();
		}
		set
		{
			__pbn__SingleModeScore = value;
		}
	}

	[ProtoMember(11, Name = "battle_config_id")]
	public uint BattleConfigId
	{
		get
		{
			return __pbn__BattleConfigId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleConfigId = value;
		}
	}

	[ProtoMember(12, Name = "single_mode_activity_id")]
	public uint SingleModeActivityId
	{
		get
		{
			return __pbn__SingleModeActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__SingleModeActivityId = value;
		}
	}

	[ProtoMember(13, Name = "end_time")]
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

	[ProtoMember(14, Name = "greedy_reward_data")]
	public RewardData GreedyRewardData { get; set; }

	[ProtoMember(15, Name = "greedy_max_finish_floor")]
	public uint GreedyMaxFinishFloor
	{
		get
		{
			return __pbn__GreedyMaxFinishFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__GreedyMaxFinishFloor = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndlessType()
	{
		return __pbn__EndlessType.HasValue;
	}

	public void ResetEndlessType()
	{
		__pbn__EndlessType = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeFinalBossId()
	{
		return __pbn__FinalBossId.HasValue;
	}

	public void ResetFinalBossId()
	{
		__pbn__FinalBossId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeRewardProgress()
	{
		return __pbn__RewardProgress.HasValue;
	}

	public void ResetRewardProgress()
	{
		__pbn__RewardProgress = null;
	}

	public bool ShouldSerializeRewardType()
	{
		return __pbn__RewardType.HasValue;
	}

	public void ResetRewardType()
	{
		__pbn__RewardType = null;
	}

	public bool ShouldSerializeSingleModeScore()
	{
		return __pbn__SingleModeScore.HasValue;
	}

	public void ResetSingleModeScore()
	{
		__pbn__SingleModeScore = null;
	}

	public bool ShouldSerializeBattleConfigId()
	{
		return __pbn__BattleConfigId.HasValue;
	}

	public void ResetBattleConfigId()
	{
		__pbn__BattleConfigId = null;
	}

	public bool ShouldSerializeSingleModeActivityId()
	{
		return __pbn__SingleModeActivityId.HasValue;
	}

	public void ResetSingleModeActivityId()
	{
		__pbn__SingleModeActivityId = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeGreedyMaxFinishFloor()
	{
		return __pbn__GreedyMaxFinishFloor.HasValue;
	}

	public void ResetGreedyMaxFinishFloor()
	{
		__pbn__GreedyMaxFinishFloor = null;
	}
}
