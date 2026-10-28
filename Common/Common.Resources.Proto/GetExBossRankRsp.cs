using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetExBossRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 528
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "RANK_ERROR")]
		RankError,
		[ProtoEnum(Name = "NOT_JOIN")]
		NotJoin,
		[ProtoEnum(Name = "SCHEDULE_DISMATCH")]
		ScheduleDismatch,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RankId;

	private bool? __pbn__IsFiltered;

	private uint? __pbn__BossId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "rank_data")]
	public RankShowData RankData { get; set; }

	[ProtoMember(3, Name = "rank_id")]
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

	[ProtoMember(4, Name = "is_filtered")]
	public bool IsFiltered
	{
		get
		{
			return __pbn__IsFiltered.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFiltered = value;
		}
	}

	[ProtoMember(5, Name = "boss_id")]
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

	public bool ShouldSerializeIsFiltered()
	{
		return __pbn__IsFiltered.HasValue;
	}

	public void ResetIsFiltered()
	{
		__pbn__IsFiltered = null;
	}

	public bool ShouldSerializeBossId()
	{
		return __pbn__BossId.HasValue;
	}

	public void ResetBossId()
	{
		__pbn__BossId = null;
	}
}
