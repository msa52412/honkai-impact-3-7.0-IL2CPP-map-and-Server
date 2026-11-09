using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SanctuaryActivityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__HasTakeRewardLevel;

	private uint? __pbn__ProductCounterTime;

	private uint? __pbn__ProductStorageNum;

	private bool? __pbn__IsInit;

	private uint? __pbn__PlayerLevelGroupId;

	private bool? __pbn__IsNewStage;

	[ProtoMember(1, Name = "activity_id")]
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

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(4, Name = "has_take_reward_level")]
	public uint HasTakeRewardLevel
	{
		get
		{
			return __pbn__HasTakeRewardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeRewardLevel = value;
		}
	}

	[ProtoMember(5, Name = "product_counter_time")]
	public uint ProductCounterTime
	{
		get
		{
			return __pbn__ProductCounterTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductCounterTime = value;
		}
	}

	[ProtoMember(6, Name = "product_storage_num")]
	public uint ProductStorageNum
	{
		get
		{
			return __pbn__ProductStorageNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductStorageNum = value;
		}
	}

	[ProtoMember(7, Name = "is_init")]
	public bool IsInit
	{
		get
		{
			return __pbn__IsInit.GetValueOrDefault();
		}
		set
		{
			__pbn__IsInit = value;
		}
	}

	[ProtoMember(8, Name = "player_level_group_id")]
	public uint PlayerLevelGroupId
	{
		get
		{
			return __pbn__PlayerLevelGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerLevelGroupId = value;
		}
	}

	[ProtoMember(9, Name = "stage_id_list")]
	public uint[] StageIdLists { get; set; }

	[ProtoMember(10, Name = "is_new_stage")]
	public bool IsNewStage
	{
		get
		{
			return __pbn__IsNewStage.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNewStage = value;
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

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeHasTakeRewardLevel()
	{
		return __pbn__HasTakeRewardLevel.HasValue;
	}

	public void ResetHasTakeRewardLevel()
	{
		__pbn__HasTakeRewardLevel = null;
	}

	public bool ShouldSerializeProductCounterTime()
	{
		return __pbn__ProductCounterTime.HasValue;
	}

	public void ResetProductCounterTime()
	{
		__pbn__ProductCounterTime = null;
	}

	public bool ShouldSerializeProductStorageNum()
	{
		return __pbn__ProductStorageNum.HasValue;
	}

	public void ResetProductStorageNum()
	{
		__pbn__ProductStorageNum = null;
	}

	public bool ShouldSerializeIsInit()
	{
		return __pbn__IsInit.HasValue;
	}

	public void ResetIsInit()
	{
		__pbn__IsInit = null;
	}

	public bool ShouldSerializePlayerLevelGroupId()
	{
		return __pbn__PlayerLevelGroupId.HasValue;
	}

	public void ResetPlayerLevelGroupId()
	{
		__pbn__PlayerLevelGroupId = null;
	}

	public bool ShouldSerializeIsNewStage()
	{
		return __pbn__IsNewStage.HasValue;
	}

	public void ResetIsNewStage()
	{
		__pbn__IsNewStage = null;
	}
}
