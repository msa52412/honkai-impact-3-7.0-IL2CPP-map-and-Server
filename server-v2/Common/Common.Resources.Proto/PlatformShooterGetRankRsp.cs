using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlatformShooterGetRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6963
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_RANK")]
		NoRank
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__StageId;

	private uint? __pbn__RankPercent;

	private uint? __pbn__Rank;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(4, Name = "rank_data")]
	public RankShowData RankData { get; set; }

	[ProtoMember(5, Name = "rank_percent")]
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

	[ProtoMember(6, Name = "rank")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeRankPercent()
	{
		return __pbn__RankPercent.HasValue;
	}

	public void ResetRankPercent()
	{
		__pbn__RankPercent = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}
}
