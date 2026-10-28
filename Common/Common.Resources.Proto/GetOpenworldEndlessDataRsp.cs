using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOpenworldEndlessDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3420
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "NO_ENDLESS")]
		NoEndless,
		[ProtoEnum(Name = "URGENCY_STOP")]
		UrgencyStop,
		[ProtoEnum(Name = "ULTRA_ENDLESS")]
		UltraEndless,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__CloseTime;

	private uint? __pbn__RandomSeed;

	private uint? __pbn__BattleConfigId;

	private uint? __pbn__BossExtraScore;

	private uint? __pbn__IsJustJoin;

	private uint? __pbn__CurTopGroupLevel;

	private uint? __pbn__TopGroupPlayerNum;

	private uint? __pbn__TopGroupPromoteUnlockPlayerNum;

	private uint? __pbn__InvasionId;

	private uint? __pbn__HardLevel;

	private EndlessType? __pbn__Type;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(3, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(4, Name = "group_level")]
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

	[ProtoMember(5, Name = "begin_time")]
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

	[ProtoMember(6, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(7, Name = "close_time")]
	public uint CloseTime
	{
		get
		{
			return __pbn__CloseTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CloseTime = value;
		}
	}

	[ProtoMember(8, Name = "endless_data_list")]
	public List<OpenworldEndlessPlayer> EndlessDataLists { get; } = new List<OpenworldEndlessPlayer>();


	[ProtoMember(9, Name = "brief_data_list")]
	public List<PlayerFriendBriefData> BriefDataLists { get; } = new List<PlayerFriendBriefData>();


	[ProtoMember(10, Name = "random_seed")]
	public uint RandomSeed
	{
		get
		{
			return __pbn__RandomSeed.GetValueOrDefault();
		}
		set
		{
			__pbn__RandomSeed = value;
		}
	}

	[ProtoMember(11, Name = "item_list")]
	public List<OpenworldEndlessItem> ItemLists { get; } = new List<OpenworldEndlessItem>();


	[ProtoMember(12, Name = "finish_monster_group_list")]
	public uint[] FinishMonsterGroupLists { get; set; }

	[ProtoMember(13, Name = "battle_config_id")]
	public uint BattleConfigId
	{
		get
		{
			return __pbn__BattleConfigId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleConfigId = value;
		}
	}

	[ProtoMember(14, Name = "effect_item_list")]
	public List<OpenworldEndlessItem> EffectItemLists { get; } = new List<OpenworldEndlessItem>();


	[ProtoMember(15, Name = "trap_add_item_list")]
	public List<OpenworldEndlessItem> TrapAddItemLists { get; } = new List<OpenworldEndlessItem>();


	[ProtoMember(16, Name = "boss_extra_score")]
	public uint BossExtraScore
	{
		get
		{
			return __pbn__BossExtraScore.GetValueOrDefault();
		}
		set
		{
			__pbn__BossExtraScore = value;
		}
	}

	[ProtoMember(17, Name = "is_just_join")]
	public uint IsJustJoin
	{
		get
		{
			return __pbn__IsJustJoin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsJustJoin = value;
		}
	}

	[ProtoMember(18, Name = "cur_top_group_level")]
	public uint CurTopGroupLevel
	{
		get
		{
			return __pbn__CurTopGroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__CurTopGroupLevel = value;
		}
	}

	[ProtoMember(19, Name = "top_group_player_num")]
	public uint TopGroupPlayerNum
	{
		get
		{
			return __pbn__TopGroupPlayerNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TopGroupPlayerNum = value;
		}
	}

	[ProtoMember(20, Name = "top_group_promote_unlock_player_num")]
	public uint TopGroupPromoteUnlockPlayerNum
	{
		get
		{
			return __pbn__TopGroupPromoteUnlockPlayerNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TopGroupPromoteUnlockPlayerNum = value;
		}
	}

	[ProtoMember(21, Name = "invasion_id")]
	public uint InvasionId
	{
		get
		{
			return __pbn__InvasionId.GetValueOrDefault();
		}
		set
		{
			__pbn__InvasionId = value;
		}
	}

	[ProtoMember(22, Name = "hard_level")]
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

	[ProtoMember(23, Name = "war_info_list")]
	public List<OpenworldEndlessWarInfo> WarInfoLists { get; } = new List<OpenworldEndlessWarInfo>();


	[ProtoMember(24, Name = "event_sub_type_id_list")]
	public uint[] EventSubTypeIdLists { get; set; }

	[ProtoMember(25, Name = "trap_item_show_id_list")]
	public uint[] TrapItemShowIdLists { get; set; }

	[ProtoMember(26, Name = "monster_group_list")]
	public List<OpenworldEndlessMonsterGroup> MonsterGroupLists { get; } = new List<OpenworldEndlessMonsterGroup>();


	[ProtoMember(27, Name = "type")]
	[DefaultValue(EndlessType.EndlessTypeNone)]
	public EndlessType Type
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

	[ProtoMember(28, Name = "quantum_endless")]
	public QuantumEndless QuantumEndless { get; set; }

	[ProtoMember(29, Name = "greedy_endless")]
	public GreedyEndless GreedyEndless { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeCloseTime()
	{
		return __pbn__CloseTime.HasValue;
	}

	public void ResetCloseTime()
	{
		__pbn__CloseTime = null;
	}

	public bool ShouldSerializeRandomSeed()
	{
		return __pbn__RandomSeed.HasValue;
	}

	public void ResetRandomSeed()
	{
		__pbn__RandomSeed = null;
	}

	public bool ShouldSerializeBattleConfigId()
	{
		return __pbn__BattleConfigId.HasValue;
	}

	public void ResetBattleConfigId()
	{
		__pbn__BattleConfigId = null;
	}

	public bool ShouldSerializeBossExtraScore()
	{
		return __pbn__BossExtraScore.HasValue;
	}

	public void ResetBossExtraScore()
	{
		__pbn__BossExtraScore = null;
	}

	public bool ShouldSerializeIsJustJoin()
	{
		return __pbn__IsJustJoin.HasValue;
	}

	public void ResetIsJustJoin()
	{
		__pbn__IsJustJoin = null;
	}

	public bool ShouldSerializeCurTopGroupLevel()
	{
		return __pbn__CurTopGroupLevel.HasValue;
	}

	public void ResetCurTopGroupLevel()
	{
		__pbn__CurTopGroupLevel = null;
	}

	public bool ShouldSerializeTopGroupPlayerNum()
	{
		return __pbn__TopGroupPlayerNum.HasValue;
	}

	public void ResetTopGroupPlayerNum()
	{
		__pbn__TopGroupPlayerNum = null;
	}

	public bool ShouldSerializeTopGroupPromoteUnlockPlayerNum()
	{
		return __pbn__TopGroupPromoteUnlockPlayerNum.HasValue;
	}

	public void ResetTopGroupPromoteUnlockPlayerNum()
	{
		__pbn__TopGroupPromoteUnlockPlayerNum = null;
	}

	public bool ShouldSerializeInvasionId()
	{
		return __pbn__InvasionId.HasValue;
	}

	public void ResetInvasionId()
	{
		__pbn__InvasionId = null;
	}

	public bool ShouldSerializeHardLevel()
	{
		return __pbn__HardLevel.HasValue;
	}

	public void ResetHardLevel()
	{
		__pbn__HardLevel = null;
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
