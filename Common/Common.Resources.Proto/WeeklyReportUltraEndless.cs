using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WeeklyReportUltraEndless : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__MaxStageScore;

	private uint? __pbn__GroupMemberNum;

	private uint? __pbn__Rank;

	private uint? __pbn__CupNumBeforeScheduleBegin;

	private uint? __pbn__CupNumAfterScheduleSettle;

	private uint? __pbn__TopRank;

	private uint? __pbn__TopRankRewardId;

	private uint? __pbn__GroupLevelRewardId;

	private uint? __pbn__MaxMissionNum;

	private uint? __pbn__FinishedMissionNum;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__BattleConfigId;

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

	[ProtoMember(2, Name = "group_level")]
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

	[ProtoMember(3, Name = "max_stage_score")]
	public uint MaxStageScore
	{
		get
		{
			return __pbn__MaxStageScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxStageScore = value;
		}
	}

	[ProtoMember(4, Name = "group_member_num")]
	public uint GroupMemberNum
	{
		get
		{
			return __pbn__GroupMemberNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupMemberNum = value;
		}
	}

	[ProtoMember(5, Name = "rank")]
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

	[ProtoMember(6, Name = "cup_num_before_schedule_begin")]
	public uint CupNumBeforeScheduleBegin
	{
		get
		{
			return __pbn__CupNumBeforeScheduleBegin.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNumBeforeScheduleBegin = value;
		}
	}

	[ProtoMember(7, Name = "cup_num_after_schedule_settle")]
	public uint CupNumAfterScheduleSettle
	{
		get
		{
			return __pbn__CupNumAfterScheduleSettle.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNumAfterScheduleSettle = value;
		}
	}

	[ProtoMember(8, Name = "top_rank")]
	public uint TopRank
	{
		get
		{
			return __pbn__TopRank.GetValueOrDefault();
		}
		set
		{
			__pbn__TopRank = value;
		}
	}

	[ProtoMember(9, Name = "top_rank_reward_id")]
	public uint TopRankRewardId
	{
		get
		{
			return __pbn__TopRankRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__TopRankRewardId = value;
		}
	}

	[ProtoMember(10, Name = "group_level_reward_id")]
	public uint GroupLevelRewardId
	{
		get
		{
			return __pbn__GroupLevelRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupLevelRewardId = value;
		}
	}

	[ProtoMember(11, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

	[ProtoMember(12, Name = "max_mission_num")]
	public uint MaxMissionNum
	{
		get
		{
			return __pbn__MaxMissionNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxMissionNum = value;
		}
	}

	[ProtoMember(13, Name = "finished_mission_num")]
	public uint FinishedMissionNum
	{
		get
		{
			return __pbn__FinishedMissionNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishedMissionNum = value;
		}
	}

	[ProtoMember(14, Name = "mission_reward_list")]
	public uint[] MissionRewardLists { get; set; }

	[ProtoMember(15, Name = "begin_time")]
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

	[ProtoMember(16, Name = "end_time")]
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

	[ProtoMember(17, Name = "battle_config_id")]
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

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeMaxStageScore()
	{
		return __pbn__MaxStageScore.HasValue;
	}

	public void ResetMaxStageScore()
	{
		__pbn__MaxStageScore = null;
	}

	public bool ShouldSerializeGroupMemberNum()
	{
		return __pbn__GroupMemberNum.HasValue;
	}

	public void ResetGroupMemberNum()
	{
		__pbn__GroupMemberNum = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeCupNumBeforeScheduleBegin()
	{
		return __pbn__CupNumBeforeScheduleBegin.HasValue;
	}

	public void ResetCupNumBeforeScheduleBegin()
	{
		__pbn__CupNumBeforeScheduleBegin = null;
	}

	public bool ShouldSerializeCupNumAfterScheduleSettle()
	{
		return __pbn__CupNumAfterScheduleSettle.HasValue;
	}

	public void ResetCupNumAfterScheduleSettle()
	{
		__pbn__CupNumAfterScheduleSettle = null;
	}

	public bool ShouldSerializeTopRank()
	{
		return __pbn__TopRank.HasValue;
	}

	public void ResetTopRank()
	{
		__pbn__TopRank = null;
	}

	public bool ShouldSerializeTopRankRewardId()
	{
		return __pbn__TopRankRewardId.HasValue;
	}

	public void ResetTopRankRewardId()
	{
		__pbn__TopRankRewardId = null;
	}

	public bool ShouldSerializeGroupLevelRewardId()
	{
		return __pbn__GroupLevelRewardId.HasValue;
	}

	public void ResetGroupLevelRewardId()
	{
		__pbn__GroupLevelRewardId = null;
	}

	public bool ShouldSerializeMaxMissionNum()
	{
		return __pbn__MaxMissionNum.HasValue;
	}

	public void ResetMaxMissionNum()
	{
		__pbn__MaxMissionNum = null;
	}

	public bool ShouldSerializeFinishedMissionNum()
	{
		return __pbn__FinishedMissionNum.HasValue;
	}

	public void ResetFinishedMissionNum()
	{
		__pbn__FinishedMissionNum = null;
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

	public bool ShouldSerializeBattleConfigId()
	{
		return __pbn__BattleConfigId.HasValue;
	}

	public void ResetBattleConfigId()
	{
		__pbn__BattleConfigId = null;
	}
}
