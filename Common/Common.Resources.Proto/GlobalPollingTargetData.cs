using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalPollingTargetData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private ulong? __pbn__ScoreU64;

	private uint? __pbn__VoteNum;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "score_u64")]
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

	[ProtoMember(3, Name = "vote_num")]
	public uint VoteNum
	{
		get
		{
			return __pbn__VoteNum.GetValueOrDefault();
		}
		set
		{
			__pbn__VoteNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeScoreU64()
	{
		return __pbn__ScoreU64.HasValue;
	}

	public void ResetScoreU64()
	{
		__pbn__ScoreU64 = null;
	}

	public bool ShouldSerializeVoteNum()
	{
		return __pbn__VoteNum.HasValue;
	}

	public void ResetVoteNum()
	{
		__pbn__VoteNum = null;
	}
}
