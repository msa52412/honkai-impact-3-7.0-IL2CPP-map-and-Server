using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessPlayer : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__CupNum;

	private uint? __pbn__MaxStageScore;

	private uint? __pbn__ScoreUpdateTime;

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

	[ProtoMember(3, Name = "cup_num")]
	public uint CupNum
	{
		get
		{
			return __pbn__CupNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNum = value;
		}
	}

	[ProtoMember(4, Name = "max_stage_score")]
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

	[ProtoMember(5, Name = "score_update_time")]
	public uint ScoreUpdateTime
	{
		get
		{
			return __pbn__ScoreUpdateTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreUpdateTime = value;
		}
	}

	[ProtoMember(6, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

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

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeCupNum()
	{
		return __pbn__CupNum.HasValue;
	}

	public void ResetCupNum()
	{
		__pbn__CupNum = null;
	}

	public bool ShouldSerializeMaxStageScore()
	{
		return __pbn__MaxStageScore.HasValue;
	}

	public void ResetMaxStageScore()
	{
		__pbn__MaxStageScore = null;
	}

	public bool ShouldSerializeScoreUpdateTime()
	{
		return __pbn__ScoreUpdateTime.HasValue;
	}

	public void ResetScoreUpdateTime()
	{
		__pbn__ScoreUpdateTime = null;
	}
}
