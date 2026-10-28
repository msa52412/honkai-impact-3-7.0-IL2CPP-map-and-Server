using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SnsShowExBossData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurRank;

	private uint? __pbn__TotalScore;

	private uint? __pbn__RankId;

	private uint? __pbn__CurRankPercent;

	[ProtoMember(1, Name = "cur_rank")]
	public uint CurRank
	{
		get
		{
			return __pbn__CurRank.GetValueOrDefault();
		}
		set
		{
			__pbn__CurRank = value;
		}
	}

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

	[ProtoMember(3, Name = "finished_boss_info_list")]
	public List<FinishedExBossInfo> FinishedBossInfoLists { get; } = new List<FinishedExBossInfo>();


	[ProtoMember(4, Name = "rank_id")]
	public uint RankId
	{
		get
		{
			return __pbn__RankId.GetValueOrDefault();
		}
		set
		{
			__pbn__RankId = value;
		}
	}

	[ProtoMember(5, Name = "cur_rank_percent")]
	public uint CurRankPercent
	{
		get
		{
			return __pbn__CurRankPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__CurRankPercent = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurRank()
	{
		return __pbn__CurRank.HasValue;
	}

	public void ResetCurRank()
	{
		__pbn__CurRank = null;
	}

	public bool ShouldSerializeTotalScore()
	{
		return __pbn__TotalScore.HasValue;
	}

	public void ResetTotalScore()
	{
		__pbn__TotalScore = null;
	}

	public bool ShouldSerializeRankId()
	{
		return __pbn__RankId.HasValue;
	}

	public void ResetRankId()
	{
		__pbn__RankId = null;
	}

	public bool ShouldSerializeCurRankPercent()
	{
		return __pbn__CurRankPercent.HasValue;
	}

	public void ResetCurRankPercent()
	{
		__pbn__CurRankPercent = null;
	}
}
