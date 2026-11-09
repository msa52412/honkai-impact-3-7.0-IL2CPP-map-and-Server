using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldMapBriefData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__Status;

	private uint? __pbn__Cycle;

	private uint? __pbn__QuestLevel;

	private uint? __pbn__HasTakeFinishRewardCycle;

	[ProtoMember(1, Name = "map_id")]
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

	[ProtoMember(2, Name = "status")]
	public uint Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(3, Name = "stat_list")]
	public List<OpenworldStat> StatLists { get; } = new List<OpenworldStat>();


	[ProtoMember(4, Name = "cycle")]
	public uint Cycle
	{
		get
		{
			return __pbn__Cycle.GetValueOrDefault();
		}
		set
		{
			__pbn__Cycle = value;
		}
	}

	[ProtoMember(5, Name = "quest_level")]
	public uint QuestLevel
	{
		get
		{
			return __pbn__QuestLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestLevel = value;
		}
	}

	[ProtoMember(6, Name = "has_take_finish_reward_cycle")]
	public uint HasTakeFinishRewardCycle
	{
		get
		{
			return __pbn__HasTakeFinishRewardCycle.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeFinishRewardCycle = value;
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

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeCycle()
	{
		return __pbn__Cycle.HasValue;
	}

	public void ResetCycle()
	{
		__pbn__Cycle = null;
	}

	public bool ShouldSerializeQuestLevel()
	{
		return __pbn__QuestLevel.HasValue;
	}

	public void ResetQuestLevel()
	{
		__pbn__QuestLevel = null;
	}

	public bool ShouldSerializeHasTakeFinishRewardCycle()
	{
		return __pbn__HasTakeFinishRewardCycle.HasValue;
	}

	public void ResetHasTakeFinishRewardCycle()
	{
		__pbn__HasTakeFinishRewardCycle = null;
	}
}
