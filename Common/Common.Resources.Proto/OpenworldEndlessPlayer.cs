using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessPlayer : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__Score;

	private uint? __pbn__ScoreTime;

	private uint? __pbn__MaxScore;

	private uint? __pbn__RewardProgress;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__UnfinishedMonsterMaxProgress;

	private uint? __pbn__GreedyMaxFinishFloor;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "score")]
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

	[ProtoMember(3, Name = "score_time")]
	public uint ScoreTime
	{
		get
		{
			return __pbn__ScoreTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreTime = value;
		}
	}

	[ProtoMember(4, Name = "invasion_list")]
	public List<OpenworldEndlessPlayerInvasionData> InvasionLists { get; } = new List<OpenworldEndlessPlayerInvasionData>();


	[ProtoMember(5, Name = "last_lineup_info")]
	public LineupBriefInfo LastLineupInfo { get; set; }

	[ProtoMember(6, Name = "effect_item_list")]
	public List<OpenworldEndlessItem> EffectItemLists { get; } = new List<OpenworldEndlessItem>();


	[ProtoMember(7, Name = "max_score")]
	public uint MaxScore
	{
		get
		{
			return __pbn__MaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxScore = value;
		}
	}

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

	[ProtoMember(9, Name = "group_level")]
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

	[ProtoMember(10, Name = "unfinished_monster_max_progress")]
	public uint UnfinishedMonsterMaxProgress
	{
		get
		{
			return __pbn__UnfinishedMonsterMaxProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__UnfinishedMonsterMaxProgress = value;
		}
	}

	[ProtoMember(11, Name = "max_score_lineup_info")]
	public LineupBriefInfo MaxScoreLineupInfo { get; set; }

	[ProtoMember(12, Name = "greedy_max_finish_floor")]
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

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeScoreTime()
	{
		return __pbn__ScoreTime.HasValue;
	}

	public void ResetScoreTime()
	{
		__pbn__ScoreTime = null;
	}

	public bool ShouldSerializeMaxScore()
	{
		return __pbn__MaxScore.HasValue;
	}

	public void ResetMaxScore()
	{
		__pbn__MaxScore = null;
	}

	public bool ShouldSerializeRewardProgress()
	{
		return __pbn__RewardProgress.HasValue;
	}

	public void ResetRewardProgress()
	{
		__pbn__RewardProgress = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeUnfinishedMonsterMaxProgress()
	{
		return __pbn__UnfinishedMonsterMaxProgress.HasValue;
	}

	public void ResetUnfinishedMonsterMaxProgress()
	{
		__pbn__UnfinishedMonsterMaxProgress = null;
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
