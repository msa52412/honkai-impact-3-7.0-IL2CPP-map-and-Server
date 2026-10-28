using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReunionCookActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScheduleId;

	private uint? __pbn__StartTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__CookScore;

	private uint? __pbn__LastSettleRewardScore;

	private uint? __pbn__ScoreRewardGroup;

	private uint? __pbn__GroupId;

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

	[ProtoMember(4, Name = "cook_score")]
	public uint CookScore
	{
		get
		{
			return __pbn__CookScore.GetValueOrDefault();
		}
		set
		{
			__pbn__CookScore = value;
		}
	}

	[ProtoMember(5, Name = "last_settle_reward_score")]
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

	[ProtoMember(6, Name = "cook_history")]
	public List<ReunionCookHistory> CookHistories { get; } = new List<ReunionCookHistory>();


	[ProtoMember(7, Name = "score_reward_group")]
	public uint ScoreRewardGroup
	{
		get
		{
			return __pbn__ScoreRewardGroup.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreRewardGroup = value;
		}
	}

	[ProtoMember(8, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
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

	public bool ShouldSerializeCookScore()
	{
		return __pbn__CookScore.HasValue;
	}

	public void ResetCookScore()
	{
		__pbn__CookScore = null;
	}

	public bool ShouldSerializeLastSettleRewardScore()
	{
		return __pbn__LastSettleRewardScore.HasValue;
	}

	public void ResetLastSettleRewardScore()
	{
		__pbn__LastSettleRewardScore = null;
	}

	public bool ShouldSerializeScoreRewardGroup()
	{
		return __pbn__ScoreRewardGroup.HasValue;
	}

	public void ResetScoreRewardGroup()
	{
		__pbn__ScoreRewardGroup = null;
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}
}
