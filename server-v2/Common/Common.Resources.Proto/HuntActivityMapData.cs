using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class HuntActivityMapData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private bool? __pbn__IsUnlock;

	private uint? __pbn__OpenTime;

	private uint? __pbn__HardLevel;

	private uint? __pbn__RemainStrongholdTakeTimes;

	private uint? __pbn__HuntingScore;

	private uint? __pbn__FinishQuestNum;

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

	[ProtoMember(2, Name = "is_unlock")]
	public bool IsUnlock
	{
		get
		{
			return __pbn__IsUnlock.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUnlock = value;
		}
	}

	[ProtoMember(3, Name = "open_time")]
	public uint OpenTime
	{
		get
		{
			return __pbn__OpenTime.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenTime = value;
		}
	}

	[ProtoMember(4, Name = "hunter")]
	public HuntActivityHunter Hunter { get; set; }

	[ProtoMember(5, Name = "machine_list")]
	public List<HuntActivityMachine> MachineLists { get; } = new List<HuntActivityMachine>();


	[ProtoMember(7, Name = "show_stronghold_id_list")]
	public uint[] ShowStrongholdIdLists { get; set; }

	[ProtoMember(8, Name = "hard_level")]
	public uint HardLevel
	{
		get
		{
			return __pbn__HardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HardLevel = value;
		}
	}

	[ProtoMember(9, Name = "remain_stronghold_take_times")]
	public uint RemainStrongholdTakeTimes
	{
		get
		{
			return __pbn__RemainStrongholdTakeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RemainStrongholdTakeTimes = value;
		}
	}

	[ProtoMember(10, Name = "quest_list")]
	public List<OpenworldQuest> QuestLists { get; } = new List<OpenworldQuest>();


	[ProtoMember(11, Name = "hunting_score")]
	public uint HuntingScore
	{
		get
		{
			return __pbn__HuntingScore.GetValueOrDefault();
		}
		set
		{
			__pbn__HuntingScore = value;
		}
	}

	[ProtoMember(12, Name = "finish_quest_num")]
	public uint FinishQuestNum
	{
		get
		{
			return __pbn__FinishQuestNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishQuestNum = value;
		}
	}

	[ProtoMember(13, Name = "finish_stronghold_id_list")]
	public uint[] FinishStrongholdIdLists { get; set; }

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

	public bool ShouldSerializeIsUnlock()
	{
		return __pbn__IsUnlock.HasValue;
	}

	public void ResetIsUnlock()
	{
		__pbn__IsUnlock = null;
	}

	public bool ShouldSerializeOpenTime()
	{
		return __pbn__OpenTime.HasValue;
	}

	public void ResetOpenTime()
	{
		__pbn__OpenTime = null;
	}

	public bool ShouldSerializeHardLevel()
	{
		return __pbn__HardLevel.HasValue;
	}

	public void ResetHardLevel()
	{
		__pbn__HardLevel = null;
	}

	public bool ShouldSerializeRemainStrongholdTakeTimes()
	{
		return __pbn__RemainStrongholdTakeTimes.HasValue;
	}

	public void ResetRemainStrongholdTakeTimes()
	{
		__pbn__RemainStrongholdTakeTimes = null;
	}

	public bool ShouldSerializeHuntingScore()
	{
		return __pbn__HuntingScore.HasValue;
	}

	public void ResetHuntingScore()
	{
		__pbn__HuntingScore = null;
	}

	public bool ShouldSerializeFinishQuestNum()
	{
		return __pbn__FinishQuestNum.HasValue;
	}

	public void ResetFinishQuestNum()
	{
		__pbn__FinishQuestNum = null;
	}
}
