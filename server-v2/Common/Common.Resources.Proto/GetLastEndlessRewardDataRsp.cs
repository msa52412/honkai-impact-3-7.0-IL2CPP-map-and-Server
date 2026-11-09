using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetLastEndlessRewardDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3407
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_JOIN")]
		NotJoin
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BeginTime;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__Rank;

	private uint? __pbn__ProgressOrScore;

	private EndlessRewardType? __pbn__RewardType;

	private uint? __pbn__RewardId;

	private uint? __pbn__CurGroupLevel;

	private uint? __pbn__Type;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "group_level")]
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

	[ProtoMember(5, Name = "progress_or_score")]
	public uint ProgressOrScore
	{
		get
		{
			return __pbn__ProgressOrScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ProgressOrScore = value;
		}
	}

	[ProtoMember(6, Name = "reward_type")]
	[DefaultValue(EndlessRewardType.EndlessPromote)]
	public EndlessRewardType RewardType
	{
		get
		{
			return __pbn__RewardType ?? EndlessRewardType.EndlessPromote;
		}
		set
		{
			__pbn__RewardType = value;
		}
	}

	[ProtoMember(7, Name = "reward_id")]
	public uint RewardId
	{
		get
		{
			return __pbn__RewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardId = value;
		}
	}

	[ProtoMember(8, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(9, Name = "cur_group_level")]
	public uint CurGroupLevel
	{
		get
		{
			return __pbn__CurGroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__CurGroupLevel = value;
		}
	}

	[ProtoMember(10, Name = "type")]
	public uint Type
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeProgressOrScore()
	{
		return __pbn__ProgressOrScore.HasValue;
	}

	public void ResetProgressOrScore()
	{
		__pbn__ProgressOrScore = null;
	}

	public bool ShouldSerializeRewardType()
	{
		return __pbn__RewardType.HasValue;
	}

	public void ResetRewardType()
	{
		__pbn__RewardType = null;
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}

	public bool ShouldSerializeCurGroupLevel()
	{
		return __pbn__CurGroupLevel.HasValue;
	}

	public void ResetCurGroupLevel()
	{
		__pbn__CurGroupLevel = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
