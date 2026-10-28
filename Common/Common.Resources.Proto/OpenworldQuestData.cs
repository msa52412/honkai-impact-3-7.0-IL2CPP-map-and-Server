using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldQuestData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	private uint? __pbn__Star;

	private bool? __pbn__IsOpen;

	private uint? __pbn__DayOpenTimes;

	private uint? __pbn__RefreshLeftTimes;

	private uint? __pbn__NextRefreshCost;

	private uint? __pbn__MaxQuestLevel;

	private bool? __pbn__IsCanAbandon;

	private uint? __pbn__ChallengeScore;

	private bool? __pbn__IsQuestFinish;

	private uint? __pbn__OpenQuestTime;

	[ProtoMember(1, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(2, Name = "star")]
	public uint Star
	{
		get
		{
			return __pbn__Star.GetValueOrDefault();
		}
		set
		{
			__pbn__Star = value;
		}
	}

	[ProtoMember(3, Name = "is_open")]
	public bool IsOpen
	{
		get
		{
			return __pbn__IsOpen.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOpen = value;
		}
	}

	[ProtoMember(4, Name = "day_open_times")]
	public uint DayOpenTimes
	{
		get
		{
			return __pbn__DayOpenTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DayOpenTimes = value;
		}
	}

	[ProtoMember(5, Name = "quest_list")]
	public List<OpenworldQuest> QuestLists { get; } = new List<OpenworldQuest>();


	[ProtoMember(6, Name = "refresh_left_times")]
	public uint RefreshLeftTimes
	{
		get
		{
			return __pbn__RefreshLeftTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshLeftTimes = value;
		}
	}

	[ProtoMember(7, Name = "next_refresh_cost")]
	public uint NextRefreshCost
	{
		get
		{
			return __pbn__NextRefreshCost.GetValueOrDefault();
		}
		set
		{
			__pbn__NextRefreshCost = value;
		}
	}

	[ProtoMember(8, Name = "key_quest")]
	public OpenworldQuest KeyQuest { get; set; }

	[ProtoMember(9, Name = "max_quest_level")]
	public uint MaxQuestLevel
	{
		get
		{
			return __pbn__MaxQuestLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxQuestLevel = value;
		}
	}

	[ProtoMember(10, Name = "is_can_abandon")]
	public bool IsCanAbandon
	{
		get
		{
			return __pbn__IsCanAbandon.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCanAbandon = value;
		}
	}

	[ProtoMember(11, Name = "challenge_score")]
	public uint ChallengeScore
	{
		get
		{
			return __pbn__ChallengeScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeScore = value;
		}
	}

	[ProtoMember(12, Name = "finish_key_quest_list")]
	public uint[] FinishKeyQuestLists { get; set; }

	[ProtoMember(13, Name = "is_quest_finish")]
	public bool IsQuestFinish
	{
		get
		{
			return __pbn__IsQuestFinish.GetValueOrDefault();
		}
		set
		{
			__pbn__IsQuestFinish = value;
		}
	}

	[ProtoMember(14, Name = "open_quest_time")]
	public uint OpenQuestTime
	{
		get
		{
			return __pbn__OpenQuestTime.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenQuestTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeIsOpen()
	{
		return __pbn__IsOpen.HasValue;
	}

	public void ResetIsOpen()
	{
		__pbn__IsOpen = null;
	}

	public bool ShouldSerializeDayOpenTimes()
	{
		return __pbn__DayOpenTimes.HasValue;
	}

	public void ResetDayOpenTimes()
	{
		__pbn__DayOpenTimes = null;
	}

	public bool ShouldSerializeRefreshLeftTimes()
	{
		return __pbn__RefreshLeftTimes.HasValue;
	}

	public void ResetRefreshLeftTimes()
	{
		__pbn__RefreshLeftTimes = null;
	}

	public bool ShouldSerializeNextRefreshCost()
	{
		return __pbn__NextRefreshCost.HasValue;
	}

	public void ResetNextRefreshCost()
	{
		__pbn__NextRefreshCost = null;
	}

	public bool ShouldSerializeMaxQuestLevel()
	{
		return __pbn__MaxQuestLevel.HasValue;
	}

	public void ResetMaxQuestLevel()
	{
		__pbn__MaxQuestLevel = null;
	}

	public bool ShouldSerializeIsCanAbandon()
	{
		return __pbn__IsCanAbandon.HasValue;
	}

	public void ResetIsCanAbandon()
	{
		__pbn__IsCanAbandon = null;
	}

	public bool ShouldSerializeChallengeScore()
	{
		return __pbn__ChallengeScore.HasValue;
	}

	public void ResetChallengeScore()
	{
		__pbn__ChallengeScore = null;
	}

	public bool ShouldSerializeIsQuestFinish()
	{
		return __pbn__IsQuestFinish.HasValue;
	}

	public void ResetIsQuestFinish()
	{
		__pbn__IsQuestFinish = null;
	}

	public bool ShouldSerializeOpenQuestTime()
	{
		return __pbn__OpenQuestTime.HasValue;
	}

	public void ResetOpenQuestTime()
	{
		__pbn__OpenQuestTime = null;
	}
}
