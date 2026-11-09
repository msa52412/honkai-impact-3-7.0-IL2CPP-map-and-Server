using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScheduleId;

	private uint? __pbn__StartTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__OpenDayTime;

	private uint? __pbn__CloseDayTime;

	private uint? __pbn__BattleTimes;

	private uint? __pbn__CurBattleUniqId;

	private uint? __pbn__PlayerGroupId;

	private uint? __pbn__LastSettleRewardScore;

	[ProtoMember(1, Name = "cur_schedule_id")]
	public uint CurScheduleId
	{
		get
		{
			return __pbn__CurScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "start_time")]
	public uint StartTime
	{
		get
		{
			return __pbn__StartTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StartTime = value;
		}
	}

	[ProtoMember(3, Name = "end_time")]
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

	[ProtoMember(4, Name = "open_day_time")]
	public uint OpenDayTime
	{
		get
		{
			return __pbn__OpenDayTime.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenDayTime = value;
		}
	}

	[ProtoMember(5, Name = "close_day_time")]
	public uint CloseDayTime
	{
		get
		{
			return __pbn__CloseDayTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CloseDayTime = value;
		}
	}

	[ProtoMember(6, Name = "battle_times")]
	public uint BattleTimes
	{
		get
		{
			return __pbn__BattleTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleTimes = value;
		}
	}

	[ProtoMember(7, Name = "cur_battle_uniq_id")]
	public uint CurBattleUniqId
	{
		get
		{
			return __pbn__CurBattleUniqId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurBattleUniqId = value;
		}
	}

	[ProtoMember(8, Name = "stage_info_list")]
	public List<MassiveWarStageInfo> StageInfoLists { get; } = new List<MassiveWarStageInfo>();


	[ProtoMember(9, Name = "player_group_id")]
	public uint PlayerGroupId
	{
		get
		{
			return __pbn__PlayerGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerGroupId = value;
		}
	}

	[ProtoMember(10, Name = "last_settle_reward_score")]
	public uint LastSettleRewardScore
	{
		get
		{
			return __pbn__LastSettleRewardScore.GetValueOrDefault();
		}
		set
		{
			__pbn__LastSettleRewardScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurScheduleId()
	{
		return __pbn__CurScheduleId.HasValue;
	}

	public void ResetCurScheduleId()
	{
		__pbn__CurScheduleId = null;
	}

	public bool ShouldSerializeStartTime()
	{
		return __pbn__StartTime.HasValue;
	}

	public void ResetStartTime()
	{
		__pbn__StartTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeOpenDayTime()
	{
		return __pbn__OpenDayTime.HasValue;
	}

	public void ResetOpenDayTime()
	{
		__pbn__OpenDayTime = null;
	}

	public bool ShouldSerializeCloseDayTime()
	{
		return __pbn__CloseDayTime.HasValue;
	}

	public void ResetCloseDayTime()
	{
		__pbn__CloseDayTime = null;
	}

	public bool ShouldSerializeBattleTimes()
	{
		return __pbn__BattleTimes.HasValue;
	}

	public void ResetBattleTimes()
	{
		__pbn__BattleTimes = null;
	}

	public bool ShouldSerializeCurBattleUniqId()
	{
		return __pbn__CurBattleUniqId.HasValue;
	}

	public void ResetCurBattleUniqId()
	{
		__pbn__CurBattleUniqId = null;
	}

	public bool ShouldSerializePlayerGroupId()
	{
		return __pbn__PlayerGroupId.HasValue;
	}

	public void ResetPlayerGroupId()
	{
		__pbn__PlayerGroupId = null;
	}

	public bool ShouldSerializeLastSettleRewardScore()
	{
		return __pbn__LastSettleRewardScore.HasValue;
	}

	public void ResetLastSettleRewardScore()
	{
		__pbn__LastSettleRewardScore = null;
	}
}
