using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralActivityStage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EnterTimes;

	private uint? __pbn__ExchangeTimes;

	private uint? __pbn__CurScore;

	private uint? __pbn__BonusScoreTimes;

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

	[ProtoMember(2, Name = "exchange_times")]
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

	[ProtoMember(3, Name = "cur_score")]
	public uint CurScore
	{
		get
		{
			return __pbn__CurScore.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScore = value;
		}
	}

	[ProtoMember(4, Name = "stage_reward_taken_list")]
	public uint[] StageRewardTakenLists { get; set; }

	[ProtoMember(5, Name = "bonus_score_times")]
	public uint BonusScoreTimes
	{
		get
		{
			return __pbn__BonusScoreTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusScoreTimes = value;
		}
	}

	[ProtoMember(6, Name = "stage_group_schedule_list")]
	public List<GeneralActivityStageGroupScheduleInfo> StageGroupScheduleLists { get; } = new List<GeneralActivityStageGroupScheduleInfo>();


	[ProtoMember(7, Name = "stage_group_reward_taken_list")]
	public uint[] StageGroupRewardTakenLists { get; set; }

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

	public bool ShouldSerializeExchangeTimes()
	{
		return __pbn__ExchangeTimes.HasValue;
	}

	public void ResetExchangeTimes()
	{
		__pbn__ExchangeTimes = null;
	}

	public bool ShouldSerializeCurScore()
	{
		return __pbn__CurScore.HasValue;
	}

	public void ResetCurScore()
	{
		__pbn__CurScore = null;
	}

	public bool ShouldSerializeBonusScoreTimes()
	{
		return __pbn__BonusScoreTimes.HasValue;
	}

	public void ResetBonusScoreTimes()
	{
		__pbn__BonusScoreTimes = null;
	}
}
