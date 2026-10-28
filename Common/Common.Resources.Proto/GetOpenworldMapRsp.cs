using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOpenworldMapRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4499
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

	private uint? __pbn__MapId;

	private uint? __pbn__Cycle;

	private uint? __pbn__EventRandomSeed;

	private string __pbn__SpawnPoint;

	private uint? __pbn__Status;

	private uint? __pbn__HasTakeFinishRewardCycle;

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

	[ProtoMember(3, Name = "cycle")]
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

	[ProtoMember(4, Name = "event_list")]
	public List<OpenworldEvent> EventLists { get; } = new List<OpenworldEvent>();


	[ProtoMember(5, Name = "event_random_seed")]
	public uint EventRandomSeed
	{
		get
		{
			return __pbn__EventRandomSeed.GetValueOrDefault();
		}
		set
		{
			__pbn__EventRandomSeed = value;
		}
	}

	[ProtoMember(6, Name = "spawn_point")]
	[DefaultValue("")]
	public string SpawnPoint
	{
		get
		{
			return __pbn__SpawnPoint ?? "";
		}
		set
		{
			__pbn__SpawnPoint = value;
		}
	}

	[ProtoMember(7, Name = "over_view_list")]
	public List<OpenworldOverView> OverViewLists { get; } = new List<OpenworldOverView>();


	[ProtoMember(8, Name = "stat_list")]
	public List<OpenworldStat> StatLists { get; } = new List<OpenworldStat>();


	[ProtoMember(9, Name = "status")]
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

	[ProtoMember(11, Name = "quest_data")]
	public OpenworldQuestData QuestData { get; set; }

	[ProtoMember(12, Name = "tech_data")]
	public OpenworldTechData TechData { get; set; }

	[ProtoMember(13, Name = "has_take_finish_reward_cycle")]
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

	[ProtoMember(14, Name = "activity_data")]
	public OpenworldActivity ActivityData { get; set; }

	[ProtoMember(15, Name = "boss_hunt")]
	public OpenworldBossHunt BossHunt { get; set; }

	[ProtoMember(16, Name = "area_info_list")]
	public List<OpenworldAreaInfo> AreaInfoLists { get; } = new List<OpenworldAreaInfo>();


	[ProtoMember(17, Name = "area_entity_state_list")]
	public List<OpenworldAreaEntityState> AreaEntityStateLists { get; } = new List<OpenworldAreaEntityState>();


	[ProtoMember(19, Name = "finish_once_event_list")]
	public uint[] FinishOnceEventLists { get; set; }

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

	public bool ShouldSerializeCycle()
	{
		return __pbn__Cycle.HasValue;
	}

	public void ResetCycle()
	{
		__pbn__Cycle = null;
	}

	public bool ShouldSerializeEventRandomSeed()
	{
		return __pbn__EventRandomSeed.HasValue;
	}

	public void ResetEventRandomSeed()
	{
		__pbn__EventRandomSeed = null;
	}

	public bool ShouldSerializeSpawnPoint()
	{
		return __pbn__SpawnPoint != null;
	}

	public void ResetSpawnPoint()
	{
		__pbn__SpawnPoint = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
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
