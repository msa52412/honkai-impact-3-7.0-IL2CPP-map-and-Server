using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GreedyEndlessReportFloorEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3456
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "NOT_CUR_FLOOR")]
		NotCurFloor
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__NextFloor;

	private uint? __pbn__MaxFinishFloor;

	private uint? __pbn__TotalScore;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(3, Name = "next_floor")]
	public uint NextFloor
	{
		get
		{
			return __pbn__NextFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__NextFloor = value;
		}
	}

	[ProtoMember(4, Name = "max_finish_floor")]
	public uint MaxFinishFloor
	{
		get
		{
			return __pbn__MaxFinishFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFinishFloor = value;
		}
	}

	[ProtoMember(5, Name = "total_score")]
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

	[ProtoMember(6, Name = "finish_challenge_id_list")]
	public uint[] FinishChallengeIdLists { get; set; }

	[ProtoMember(7, Name = "max_score_lineup_info")]
	public LineupBriefInfo MaxScoreLineupInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNextFloor()
	{
		return __pbn__NextFloor.HasValue;
	}

	public void ResetNextFloor()
	{
		__pbn__NextFloor = null;
	}

	public bool ShouldSerializeMaxFinishFloor()
	{
		return __pbn__MaxFinishFloor.HasValue;
	}

	public void ResetMaxFinishFloor()
	{
		__pbn__MaxFinishFloor = null;
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
