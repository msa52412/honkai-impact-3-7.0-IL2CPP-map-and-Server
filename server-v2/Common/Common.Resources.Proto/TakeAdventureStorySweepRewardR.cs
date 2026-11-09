using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeAdventureStorySweepRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3916
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SweepId;

	private bool? __pbn__IsTakeAll;

	[ProtoMember(1, Name = "sweep_id")]
	public uint SweepId
	{
		get
		{
			return __pbn__SweepId.GetValueOrDefault();
		}
		set
		{
			__pbn__SweepId = value;
		}
	}

	[ProtoMember(2, Name = "is_take_all")]
	public bool IsTakeAll
	{
		get
		{
			return __pbn__IsTakeAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeAll = value;
		}
	}

	[ProtoMember(3, Name = "sweep_group_type_list")]
	public uint[] SweepGroupTypeLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSweepId()
	{
		return __pbn__SweepId.HasValue;
	}

	public void ResetSweepId()
	{
		__pbn__SweepId = null;
	}

	public bool ShouldSerializeIsTakeAll()
	{
		return __pbn__IsTakeAll.HasValue;
	}

	public void ResetIsTakeAll()
	{
		__pbn__IsTakeAll = null;
	}
}
[ProtoContract]
public class TakeAdventureStorySweepRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3917
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_EXIST")]
		NotExist,
		[ProtoEnum(Name = "NOT_FINISHED")]
		NotFinished
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SweepId;

	private uint? __pbn__SiegeStageId;

	private uint? __pbn__PlayerExpReward;

	private uint? __pbn__ExpConvertScoin;

	private uint? __pbn__ScoinReward;

	private uint? __pbn__RankScoin;

	private bool? __pbn__IsTakeAll;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "sweep_id")]
	public uint SweepId
	{
		get
		{
			return __pbn__SweepId.GetValueOrDefault();
		}
		set
		{
			__pbn__SweepId = value;
		}
	}

	[ProtoMember(3, Name = "siege_stage_id")]
	public uint SiegeStageId
	{
		get
		{
			return __pbn__SiegeStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiegeStageId = value;
		}
	}

	[ProtoMember(4, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(5, Name = "player_exp_reward")]
	public uint PlayerExpReward
	{
		get
		{
			return __pbn__PlayerExpReward.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerExpReward = value;
		}
	}

	[ProtoMember(6, Name = "exp_convert_scoin")]
	public uint ExpConvertScoin
	{
		get
		{
			return __pbn__ExpConvertScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpConvertScoin = value;
		}
	}

	[ProtoMember(7, Name = "scoin_reward")]
	public uint ScoinReward
	{
		get
		{
			return __pbn__ScoinReward.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinReward = value;
		}
	}

	[ProtoMember(8, Name = "rank_scoin")]
	public uint RankScoin
	{
		get
		{
			return __pbn__RankScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__RankScoin = value;
		}
	}

	[ProtoMember(9, Name = "is_take_all")]
	public bool IsTakeAll
	{
		get
		{
			return __pbn__IsTakeAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeAll = value;
		}
	}

	[ProtoMember(10, Name = "sweep_group_type_list")]
	public uint[] SweepGroupTypeLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSweepId()
	{
		return __pbn__SweepId.HasValue;
	}

	public void ResetSweepId()
	{
		__pbn__SweepId = null;
	}

	public bool ShouldSerializeSiegeStageId()
	{
		return __pbn__SiegeStageId.HasValue;
	}

	public void ResetSiegeStageId()
	{
		__pbn__SiegeStageId = null;
	}

	public bool ShouldSerializePlayerExpReward()
	{
		return __pbn__PlayerExpReward.HasValue;
	}

	public void ResetPlayerExpReward()
	{
		__pbn__PlayerExpReward = null;
	}

	public bool ShouldSerializeExpConvertScoin()
	{
		return __pbn__ExpConvertScoin.HasValue;
	}

	public void ResetExpConvertScoin()
	{
		__pbn__ExpConvertScoin = null;
	}

	public bool ShouldSerializeScoinReward()
	{
		return __pbn__ScoinReward.HasValue;
	}

	public void ResetScoinReward()
	{
		__pbn__ScoinReward = null;
	}

	public bool ShouldSerializeRankScoin()
	{
		return __pbn__RankScoin.HasValue;
	}

	public void ResetRankScoin()
	{
		__pbn__RankScoin = null;
	}

	public bool ShouldSerializeIsTakeAll()
	{
		return __pbn__IsTakeAll.HasValue;
	}

	public void ResetIsTakeAll()
	{
		__pbn__IsTakeAll = null;
	}
}
