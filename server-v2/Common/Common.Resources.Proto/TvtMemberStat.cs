using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtMemberStat : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TotalScore;

	private bool? __pbn__IsMvp;

	private uint? __pbn__OldDivision;

	private uint? __pbn__OldDivisionStar;

	private double? __pbn__BattlePointsInStage;

	private uint? __pbn__StageScoinNum;

	private bool? __pbn__IsTeamMatch;

	[ProtoMember(1, Name = "member")]
	public TvtMember Member { get; set; }

	[ProtoMember(2, Name = "total_score")]
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

	[ProtoMember(3, Name = "is_mvp")]
	public bool IsMvp
	{
		get
		{
			return __pbn__IsMvp.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMvp = value;
		}
	}

	[ProtoMember(4, Name = "old_division")]
	public uint OldDivision
	{
		get
		{
			return __pbn__OldDivision.GetValueOrDefault();
		}
		set
		{
			__pbn__OldDivision = value;
		}
	}

	[ProtoMember(5, Name = "old_division_star")]
	public uint OldDivisionStar
	{
		get
		{
			return __pbn__OldDivisionStar.GetValueOrDefault();
		}
		set
		{
			__pbn__OldDivisionStar = value;
		}
	}

	[ProtoMember(6, Name = "battle_points_in_stage")]
	public double BattlePointsInStage
	{
		get
		{
			return __pbn__BattlePointsInStage.GetValueOrDefault();
		}
		set
		{
			__pbn__BattlePointsInStage = value;
		}
	}

	[ProtoMember(7, Name = "stage_scoin_num")]
	public uint StageScoinNum
	{
		get
		{
			return __pbn__StageScoinNum.GetValueOrDefault();
		}
		set
		{
			__pbn__StageScoinNum = value;
		}
	}

	[ProtoMember(8, Name = "is_team_match")]
	public bool IsTeamMatch
	{
		get
		{
			return __pbn__IsTeamMatch.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTeamMatch = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTotalScore()
	{
		return __pbn__TotalScore.HasValue;
	}

	public void ResetTotalScore()
	{
		__pbn__TotalScore = null;
	}

	public bool ShouldSerializeIsMvp()
	{
		return __pbn__IsMvp.HasValue;
	}

	public void ResetIsMvp()
	{
		__pbn__IsMvp = null;
	}

	public bool ShouldSerializeOldDivision()
	{
		return __pbn__OldDivision.HasValue;
	}

	public void ResetOldDivision()
	{
		__pbn__OldDivision = null;
	}

	public bool ShouldSerializeOldDivisionStar()
	{
		return __pbn__OldDivisionStar.HasValue;
	}

	public void ResetOldDivisionStar()
	{
		__pbn__OldDivisionStar = null;
	}

	public bool ShouldSerializeBattlePointsInStage()
	{
		return __pbn__BattlePointsInStage.HasValue;
	}

	public void ResetBattlePointsInStage()
	{
		__pbn__BattlePointsInStage = null;
	}

	public bool ShouldSerializeStageScoinNum()
	{
		return __pbn__StageScoinNum.HasValue;
	}

	public void ResetStageScoinNum()
	{
		__pbn__StageScoinNum = null;
	}

	public bool ShouldSerializeIsTeamMatch()
	{
		return __pbn__IsTeamMatch.HasValue;
	}

	public void ResetIsTeamMatch()
	{
		__pbn__IsTeamMatch = null;
	}
}
