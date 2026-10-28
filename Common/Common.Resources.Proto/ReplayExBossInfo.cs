using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayExBossInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RankId;

	private uint? __pbn__RawScore;

	private bool? __pbn__IsEventBoss;

	[ProtoMember(1, Name = "rank_id")]
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

	[ProtoMember(2, Name = "finished_boss_info")]
	public FinishedExBossInfo FinishedBossInfo { get; set; }

	[ProtoMember(3, Name = "raw_score")]
	public uint RawScore
	{
		get
		{
			return __pbn__RawScore.GetValueOrDefault();
		}
		set
		{
			__pbn__RawScore = value;
		}
	}

	[ProtoMember(4, Name = "is_event_boss")]
	public bool IsEventBoss
	{
		get
		{
			return __pbn__IsEventBoss.GetValueOrDefault();
		}
		set
		{
			__pbn__IsEventBoss = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRankId()
	{
		return __pbn__RankId.HasValue;
	}

	public void ResetRankId()
	{
		__pbn__RankId = null;
	}

	public bool ShouldSerializeRawScore()
	{
		return __pbn__RawScore.HasValue;
	}

	public void ResetRawScore()
	{
		__pbn__RawScore = null;
	}

	public bool ShouldSerializeIsEventBoss()
	{
		return __pbn__IsEventBoss.HasValue;
	}

	public void ResetIsEventBoss()
	{
		__pbn__IsEventBoss = null;
	}
}
