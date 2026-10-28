using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarScoreEventSettle : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScoreEventId;

	private uint? __pbn__SettleTimes;

	private uint? __pbn__TotalScore;

	[ProtoMember(1, Name = "score_event_id")]
	public uint ScoreEventId
	{
		get
		{
			return __pbn__ScoreEventId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreEventId = value;
		}
	}

	[ProtoMember(2, Name = "settle_times")]
	public uint SettleTimes
	{
		get
		{
			return __pbn__SettleTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__SettleTimes = value;
		}
	}

	[ProtoMember(3, Name = "total_score")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScoreEventId()
	{
		return __pbn__ScoreEventId.HasValue;
	}

	public void ResetScoreEventId()
	{
		__pbn__ScoreEventId = null;
	}

	public bool ShouldSerializeSettleTimes()
	{
		return __pbn__SettleTimes.HasValue;
	}

	public void ResetSettleTimes()
	{
		__pbn__SettleTimes = null;
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
