using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarPhaseKingdomData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__KingdomId;

	private uint? __pbn__Fame;

	private ulong? __pbn__ScoreU64;

	private uint? __pbn__Rank;

	private uint? __pbn__Status;

	private uint? __pbn__BonusPoint;

	private uint? __pbn__LastFame;

	private uint? __pbn__Belief;

	private uint? __pbn__LastBelief;

	[ProtoMember(1, Name = "kingdom_id")]
	public uint KingdomId
	{
		get
		{
			return __pbn__KingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__KingdomId = value;
		}
	}

	[ProtoMember(2, Name = "fame")]
	public uint Fame
	{
		get
		{
			return __pbn__Fame.GetValueOrDefault();
		}
		set
		{
			__pbn__Fame = value;
		}
	}

	[ProtoMember(3, Name = "score_u64")]
	public ulong ScoreU64
	{
		get
		{
			return __pbn__ScoreU64.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreU64 = value;
		}
	}

	[ProtoMember(4, Name = "rank")]
	public uint Rank
	{
		get
		{
			return __pbn__Rank.GetValueOrDefault();
		}
		set
		{
			__pbn__Rank = value;
		}
	}

	[ProtoMember(5, Name = "status")]
	public uint Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(6, Name = "bonus_point")]
	public uint BonusPoint
	{
		get
		{
			return __pbn__BonusPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusPoint = value;
		}
	}

	[ProtoMember(7, Name = "last_fame")]
	public uint LastFame
	{
		get
		{
			return __pbn__LastFame.GetValueOrDefault();
		}
		set
		{
			__pbn__LastFame = value;
		}
	}

	[ProtoMember(8, Name = "record_list")]
	public List<KingdomWarOccupyRecord> RecordLists { get; } = new List<KingdomWarOccupyRecord>();


	[ProtoMember(9, Name = "belief")]
	public uint Belief
	{
		get
		{
			return __pbn__Belief.GetValueOrDefault();
		}
		set
		{
			__pbn__Belief = value;
		}
	}

	[ProtoMember(10, Name = "last_belief")]
	public uint LastBelief
	{
		get
		{
			return __pbn__LastBelief.GetValueOrDefault();
		}
		set
		{
			__pbn__LastBelief = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKingdomId()
	{
		return __pbn__KingdomId.HasValue;
	}

	public void ResetKingdomId()
	{
		__pbn__KingdomId = null;
	}

	public bool ShouldSerializeFame()
	{
		return __pbn__Fame.HasValue;
	}

	public void ResetFame()
	{
		__pbn__Fame = null;
	}

	public bool ShouldSerializeScoreU64()
	{
		return __pbn__ScoreU64.HasValue;
	}

	public void ResetScoreU64()
	{
		__pbn__ScoreU64 = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeBonusPoint()
	{
		return __pbn__BonusPoint.HasValue;
	}

	public void ResetBonusPoint()
	{
		__pbn__BonusPoint = null;
	}

	public bool ShouldSerializeLastFame()
	{
		return __pbn__LastFame.HasValue;
	}

	public void ResetLastFame()
	{
		__pbn__LastFame = null;
	}

	public bool ShouldSerializeBelief()
	{
		return __pbn__Belief.HasValue;
	}

	public void ResetBelief()
	{
		__pbn__Belief = null;
	}

	public bool ShouldSerializeLastBelief()
	{
		return __pbn__LastBelief.HasValue;
	}

	public void ResetLastBelief()
	{
		__pbn__LastBelief = null;
	}
}
