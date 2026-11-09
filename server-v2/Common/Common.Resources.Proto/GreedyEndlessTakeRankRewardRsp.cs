using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GreedyEndlessTakeRankRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3461
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__RankPercent;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

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

	[ProtoMember(3, Name = "rank_percent")]
	public uint RankPercent
	{
		get
		{
			return __pbn__RankPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__RankPercent = value;
		}
	}

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeRankPercent()
	{
		return __pbn__RankPercent.HasValue;
	}

	public void ResetRankPercent()
	{
		__pbn__RankPercent = null;
	}
}
