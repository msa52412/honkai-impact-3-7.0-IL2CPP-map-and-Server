using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExBossSingleBossScoreReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BossId;

	private uint? __pbn__MaxSettleScore;

	[ProtoMember(1, Name = "boss_id")]
	public uint BossId
	{
		get
		{
			return __pbn__BossId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossId = value;
		}
	}

	[ProtoMember(2, Name = "max_settle_score")]
	public uint MaxSettleScore
	{
		get
		{
			return __pbn__MaxSettleScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxSettleScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossId()
	{
		return __pbn__BossId.HasValue;
	}

	public void ResetBossId()
	{
		__pbn__BossId = null;
	}

	public bool ShouldSerializeMaxSettleScore()
	{
		return __pbn__MaxSettleScore.HasValue;
	}

	public void ResetMaxSettleScore()
	{
		__pbn__MaxSettleScore = null;
	}
}
