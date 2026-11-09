using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EndlessStatus : IExtensible
{
	private IExtension __pbn__extensionData;

	private EndlessType? __pbn__EndlessType;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__CloseTime;

	private uint? __pbn__SinType;

	private uint? __pbn__ProgressOrScore;

	private uint? __pbn__GroupLevel;

	private bool? __pbn__CanJoinIn;

	private uint? __pbn__PreGeneralActivityId;

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

	[ProtoMember(4, Name = "close_time")]
	public uint CloseTime
	{
		get
		{
			return __pbn__CloseTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CloseTime = value;
		}
	}

	[ProtoMember(5, Name = "sin_type")]
	public uint SinType
	{
		get
		{
			return __pbn__SinType.GetValueOrDefault();
		}
		set
		{
			__pbn__SinType = value;
		}
	}

	[ProtoMember(6, Name = "progress_or_score")]
	public uint ProgressOrScore
	{
		get
		{
			return __pbn__ProgressOrScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ProgressOrScore = value;
		}
	}

	[ProtoMember(7, Name = "group_level")]
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

	[ProtoMember(8, Name = "last_lineup_info")]
	public LineupBriefInfo LastLineupInfo { get; set; }

	[ProtoMember(9, Name = "can_join_in")]
	public bool CanJoinIn
	{
		get
		{
			return __pbn__CanJoinIn.GetValueOrDefault();
		}
		set
		{
			__pbn__CanJoinIn = value;
		}
	}

	[ProtoMember(10, Name = "pre_general_activity_id")]
	public uint PreGeneralActivityId
	{
		get
		{
			return __pbn__PreGeneralActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__PreGeneralActivityId = value;
		}
	}

	[ProtoMember(11, Name = "replace_endless_group_list")]
	public uint[] ReplaceEndlessGroupLists { get; set; }

	[ProtoMember(12, Name = "max_score_lineup_info")]
	public LineupBriefInfo MaxScoreLineupInfo { get; set; }

	[ProtoMember(13, Name = "greedy_max_finish_floor")]
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

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeCloseTime()
	{
		return __pbn__CloseTime.HasValue;
	}

	public void ResetCloseTime()
	{
		__pbn__CloseTime = null;
	}

	public bool ShouldSerializeSinType()
	{
		return __pbn__SinType.HasValue;
	}

	public void ResetSinType()
	{
		__pbn__SinType = null;
	}

	public bool ShouldSerializeProgressOrScore()
	{
		return __pbn__ProgressOrScore.HasValue;
	}

	public void ResetProgressOrScore()
	{
		__pbn__ProgressOrScore = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeCanJoinIn()
	{
		return __pbn__CanJoinIn.HasValue;
	}

	public void ResetCanJoinIn()
	{
		__pbn__CanJoinIn = null;
	}

	public bool ShouldSerializePreGeneralActivityId()
	{
		return __pbn__PreGeneralActivityId.HasValue;
	}

	public void ResetPreGeneralActivityId()
	{
		__pbn__PreGeneralActivityId = null;
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
