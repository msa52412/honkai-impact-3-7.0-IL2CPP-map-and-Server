using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MiniMonopolyUseItemRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5986
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_ITEM")]
		NoItem,
		[ProtoEnum(Name = "NO_DICE")]
		NoDice,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__DiceNumber;

	private bool? __pbn__IsFinishRound;

	private uint? __pbn__ActivityId;

	private uint? __pbn__FinishGridId;

	private bool? __pbn__IsCompensateReward;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(3, Name = "dice_number")]
	public uint DiceNumber
	{
		get
		{
			return __pbn__DiceNumber.GetValueOrDefault();
		}
		set
		{
			__pbn__DiceNumber = value;
		}
	}

	[ProtoMember(4, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(5, Name = "is_finish_round")]
	public bool IsFinishRound
	{
		get
		{
			return __pbn__IsFinishRound.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinishRound = value;
		}
	}

	[ProtoMember(6, Name = "activity_id")]
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

	[ProtoMember(7, Name = "finish_grid_id")]
	public uint FinishGridId
	{
		get
		{
			return __pbn__FinishGridId.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishGridId = value;
		}
	}

	[ProtoMember(8, Name = "is_compensate_reward")]
	public bool IsCompensateReward
	{
		get
		{
			return __pbn__IsCompensateReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCompensateReward = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeDiceNumber()
	{
		return __pbn__DiceNumber.HasValue;
	}

	public void ResetDiceNumber()
	{
		__pbn__DiceNumber = null;
	}

	public bool ShouldSerializeIsFinishRound()
	{
		return __pbn__IsFinishRound.HasValue;
	}

	public void ResetIsFinishRound()
	{
		__pbn__IsFinishRound = null;
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeFinishGridId()
	{
		return __pbn__FinishGridId.HasValue;
	}

	public void ResetFinishGridId()
	{
		__pbn__FinishGridId = null;
	}

	public bool ShouldSerializeIsCompensateReward()
	{
		return __pbn__IsCompensateReward.HasValue;
	}

	public void ResetIsCompensateReward()
	{
		__pbn__IsCompensateReward = null;
	}
}
