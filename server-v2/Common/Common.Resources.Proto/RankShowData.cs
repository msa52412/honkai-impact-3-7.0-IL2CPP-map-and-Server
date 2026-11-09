using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RankShowData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MyRankType;

	private uint? __pbn__MyRank;

	private uint? __pbn__MyScore;

	private bool? __pbn__IsFeatureClosed;

	[ProtoMember(1, Name = "my_rank_type")]
	public uint MyRankType
	{
		get
		{
			return __pbn__MyRankType.GetValueOrDefault();
		}
		set
		{
			__pbn__MyRankType = value;
		}
	}

	[ProtoMember(2, Name = "my_rank")]
	public uint MyRank
	{
		get
		{
			return __pbn__MyRank.GetValueOrDefault();
		}
		set
		{
			__pbn__MyRank = value;
		}
	}

	[ProtoMember(3, Name = "rank_list")]
	public List<UserRankData> RankLists { get; } = new List<UserRankData>();


	[ProtoMember(4, Name = "my_score")]
	public uint MyScore
	{
		get
		{
			return __pbn__MyScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MyScore = value;
		}
	}

	[ProtoMember(5, Name = "is_feature_closed")]
	public bool IsFeatureClosed
	{
		get
		{
			return __pbn__IsFeatureClosed.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFeatureClosed = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMyRankType()
	{
		return __pbn__MyRankType.HasValue;
	}

	public void ResetMyRankType()
	{
		__pbn__MyRankType = null;
	}

	public bool ShouldSerializeMyRank()
	{
		return __pbn__MyRank.HasValue;
	}

	public void ResetMyRank()
	{
		__pbn__MyRank = null;
	}

	public bool ShouldSerializeMyScore()
	{
		return __pbn__MyScore.HasValue;
	}

	public void ResetMyScore()
	{
		__pbn__MyScore = null;
	}

	public bool ShouldSerializeIsFeatureClosed()
	{
		return __pbn__IsFeatureClosed.HasValue;
	}

	public void ResetIsFeatureClosed()
	{
		__pbn__IsFeatureClosed = null;
	}
}
