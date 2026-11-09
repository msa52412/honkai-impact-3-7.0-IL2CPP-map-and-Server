using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgGetDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6051
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "BATTLE_NOT_FOUND")]
		BattleNotFound,
		[ProtoEnum(Name = "NOT_IN_BATTLE")]
		NotInBattle,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__NextBattleBeginTime;

	private uint? __pbn__ActivityStaminaNum;

	private uint? __pbn__LastRecoverActivityStaminaTime;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__BattleScheduleId;

	private uint? __pbn__TotalScore;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

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

	[ProtoMember(3, Name = "next_battle_begin_time")]
	public uint NextBattleBeginTime
	{
		get
		{
			return __pbn__NextBattleBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextBattleBeginTime = value;
		}
	}

	[ProtoMember(4, Name = "battle_data")]
	public SlgBattleData BattleData { get; set; }

	[ProtoMember(5, Name = "activity_stamina_num")]
	public uint ActivityStaminaNum
	{
		get
		{
			return __pbn__ActivityStaminaNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityStaminaNum = value;
		}
	}

	[ProtoMember(6, Name = "last_recover_activity_stamina_time")]
	public uint LastRecoverActivityStaminaTime
	{
		get
		{
			return __pbn__LastRecoverActivityStaminaTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastRecoverActivityStaminaTime = value;
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

	[ProtoMember(9, Name = "unlock_story_id_list")]
	public uint[] UnlockStoryIdLists { get; set; }

	[ProtoMember(10, Name = "history_battle_data_list")]
	public List<SlgBriefBattleData> HistoryBattleDataLists { get; } = new List<SlgBriefBattleData>();


	[ProtoMember(11, Name = "battle_schedule_id")]
	public uint BattleScheduleId
	{
		get
		{
			return __pbn__BattleScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleScheduleId = value;
		}
	}

	[ProtoMember(12, Name = "current_enroll_data")]
	public SlgEnrollData CurrentEnrollData { get; set; }

	[ProtoMember(13, Name = "taken_score_reward_id_list")]
	public uint[] TakenScoreRewardIdLists { get; set; }

	[ProtoMember(14, Name = "total_score")]
	public uint TotalScore
	{
		get
		{
			return __pbn__TotalScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalScore = value;
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

	public bool ShouldSerializeNextBattleBeginTime()
	{
		return __pbn__NextBattleBeginTime.HasValue;
	}

	public void ResetNextBattleBeginTime()
	{
		__pbn__NextBattleBeginTime = null;
	}

	public bool ShouldSerializeActivityStaminaNum()
	{
		return __pbn__ActivityStaminaNum.HasValue;
	}

	public void ResetActivityStaminaNum()
	{
		__pbn__ActivityStaminaNum = null;
	}

	public bool ShouldSerializeLastRecoverActivityStaminaTime()
	{
		return __pbn__LastRecoverActivityStaminaTime.HasValue;
	}

	public void ResetLastRecoverActivityStaminaTime()
	{
		__pbn__LastRecoverActivityStaminaTime = null;
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

	public bool ShouldSerializeBattleScheduleId()
	{
		return __pbn__BattleScheduleId.HasValue;
	}

	public void ResetBattleScheduleId()
	{
		__pbn__BattleScheduleId = null;
	}

	public bool ShouldSerializeTotalScore()
	{
		return __pbn__TotalScore.HasValue;
	}

	public void ResetTotalScore()
	{
		__pbn__TotalScore = null;
	}
}
