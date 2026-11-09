using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarPhasePlayerData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurPointId;

	private uint? __pbn__TodayStep;

	private bool? __pbn__IsTakePhaseReward;

	private uint? __pbn__VotePoint;

	private uint? __pbn__YesterdayScore;

	private uint? __pbn__TodayFinishStageNum_;

	private uint? __pbn__LastPointId;

	private uint? __pbn__TodayScore;

	private uint? __pbn__ChangeStageNum;

	[ProtoMember(1, Name = "cur_point_id")]
	public uint CurPointId
	{
		get
		{
			return __pbn__CurPointId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurPointId = value;
		}
	}

	[ProtoMember(2, Name = "today_step")]
	public uint TodayStep
	{
		get
		{
			return __pbn__TodayStep.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayStep = value;
		}
	}

	[ProtoMember(3, Name = "is_take_phase_reward")]
	public bool IsTakePhaseReward
	{
		get
		{
			return __pbn__IsTakePhaseReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakePhaseReward = value;
		}
	}

	[ProtoMember(4, Name = "vote_point")]
	public uint VotePoint
	{
		get
		{
			return __pbn__VotePoint.GetValueOrDefault();
		}
		set
		{
			__pbn__VotePoint = value;
		}
	}

	[ProtoMember(5, Name = "yesterday_score")]
	public uint YesterdayScore
	{
		get
		{
			return __pbn__YesterdayScore.GetValueOrDefault();
		}
		set
		{
			__pbn__YesterdayScore = value;
		}
	}

	[ProtoMember(6, Name = "today_finish_stage_num_")]
	public uint TodayFinishStageNum_
	{
		get
		{
			return __pbn__TodayFinishStageNum_.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayFinishStageNum_ = value;
		}
	}

	[ProtoMember(7, Name = "last_point_id")]
	public uint LastPointId
	{
		get
		{
			return __pbn__LastPointId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastPointId = value;
		}
	}

	[ProtoMember(8, Name = "today_score")]
	public uint TodayScore
	{
		get
		{
			return __pbn__TodayScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayScore = value;
		}
	}

	[ProtoMember(9, Name = "change_stage_num")]
	public uint ChangeStageNum
	{
		get
		{
			return __pbn__ChangeStageNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ChangeStageNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurPointId()
	{
		return __pbn__CurPointId.HasValue;
	}

	public void ResetCurPointId()
	{
		__pbn__CurPointId = null;
	}

	public bool ShouldSerializeTodayStep()
	{
		return __pbn__TodayStep.HasValue;
	}

	public void ResetTodayStep()
	{
		__pbn__TodayStep = null;
	}

	public bool ShouldSerializeIsTakePhaseReward()
	{
		return __pbn__IsTakePhaseReward.HasValue;
	}

	public void ResetIsTakePhaseReward()
	{
		__pbn__IsTakePhaseReward = null;
	}

	public bool ShouldSerializeVotePoint()
	{
		return __pbn__VotePoint.HasValue;
	}

	public void ResetVotePoint()
	{
		__pbn__VotePoint = null;
	}

	public bool ShouldSerializeYesterdayScore()
	{
		return __pbn__YesterdayScore.HasValue;
	}

	public void ResetYesterdayScore()
	{
		__pbn__YesterdayScore = null;
	}

	public bool ShouldSerializeTodayFinishStageNum_()
	{
		return __pbn__TodayFinishStageNum_.HasValue;
	}

	public void ResetTodayFinishStageNum_()
	{
		__pbn__TodayFinishStageNum_ = null;
	}

	public bool ShouldSerializeLastPointId()
	{
		return __pbn__LastPointId.HasValue;
	}

	public void ResetLastPointId()
	{
		__pbn__LastPointId = null;
	}

	public bool ShouldSerializeTodayScore()
	{
		return __pbn__TodayScore.HasValue;
	}

	public void ResetTodayScore()
	{
		__pbn__TodayScore = null;
	}

	public bool ShouldSerializeChangeStageNum()
	{
		return __pbn__ChangeStageNum.HasValue;
	}

	public void ResetChangeStageNum()
	{
		__pbn__ChangeStageNum = null;
	}
}
