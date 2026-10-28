using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayGetRecommendListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5417
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private ReplayType? __pbn__Type;

	private uint? __pbn__StageOrBossId;

	private uint? __pbn__Rank;

	private uint? __pbn__StartIdx;

	private uint? __pbn__EndIdx;

	private bool? __pbn__IsRandom;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "type")]
	[DefaultValue(ReplayType.ReplayNone)]
	public ReplayType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "stage_or_boss_id")]
	public uint StageOrBossId
	{
		get
		{
			return __pbn__StageOrBossId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageOrBossId = value;
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

	[ProtoMember(5, Name = "start_idx")]
	public uint StartIdx
	{
		get
		{
			return __pbn__StartIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__StartIdx = value;
		}
	}

	[ProtoMember(6, Name = "end_idx")]
	public uint EndIdx
	{
		get
		{
			return __pbn__EndIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__EndIdx = value;
		}
	}

	[ProtoMember(7, Name = "replay_list")]
	public List<Replay> ReplayLists { get; } = new List<Replay>();


	[ProtoMember(8, Name = "is_random")]
	public bool IsRandom
	{
		get
		{
			return __pbn__IsRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRandom = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeStageOrBossId()
	{
		return __pbn__StageOrBossId.HasValue;
	}

	public void ResetStageOrBossId()
	{
		__pbn__StageOrBossId = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeStartIdx()
	{
		return __pbn__StartIdx.HasValue;
	}

	public void ResetStartIdx()
	{
		__pbn__StartIdx = null;
	}

	public bool ShouldSerializeEndIdx()
	{
		return __pbn__EndIdx.HasValue;
	}

	public void ResetEndIdx()
	{
		__pbn__EndIdx = null;
	}

	public bool ShouldSerializeIsRandom()
	{
		return __pbn__IsRandom.HasValue;
	}

	public void ResetIsRandom()
	{
		__pbn__IsRandom = null;
	}
}
