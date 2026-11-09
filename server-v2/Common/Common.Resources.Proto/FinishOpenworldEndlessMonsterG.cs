using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishOpenworldEndlessMonsterGroupReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3424
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__ExtraScore;

	private uint? __pbn__BattleId;

	private uint? __pbn__CostTime;

	private uint? __pbn__StageId;

	private string __pbn__TransactionStr;

	private EndlessType? __pbn__Type;

	private uint? __pbn__QuantumMonsterProgress;

	private bool? __pbn__IsSkipLevel;

	private uint? __pbn__FinalBossId;

	[ProtoMember(1, Name = "group_id")]
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

	[ProtoMember(2, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

	[ProtoMember(3, Name = "extra_score")]
	public uint ExtraScore
	{
		get
		{
			return __pbn__ExtraScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraScore = value;
		}
	}

	[ProtoMember(4, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(5, Name = "cost_time")]
	public uint CostTime
	{
		get
		{
			return __pbn__CostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CostTime = value;
		}
	}

	[ProtoMember(6, Name = "cheat_data_list")]
	public List<StageCheatData> CheatDataLists { get; } = new List<StageCheatData>();


	[ProtoMember(7, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(8, Name = "transaction_str")]
	[DefaultValue("")]
	public string TransactionStr
	{
		get
		{
			return __pbn__TransactionStr ?? "";
		}
		set
		{
			__pbn__TransactionStr = value;
		}
	}

	[ProtoMember(9, Name = "type")]
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

	[ProtoMember(10, Name = "quantum_monster_progress")]
	public uint QuantumMonsterProgress
	{
		get
		{
			return __pbn__QuantumMonsterProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__QuantumMonsterProgress = value;
		}
	}

	[ProtoMember(11, Name = "is_skip_level")]
	public bool IsSkipLevel
	{
		get
		{
			return __pbn__IsSkipLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSkipLevel = value;
		}
	}

	[ProtoMember(12, Name = "final_boss_id")]
	public uint FinalBossId
	{
		get
		{
			return __pbn__FinalBossId.GetValueOrDefault();
		}
		set
		{
			__pbn__FinalBossId = value;
		}
	}

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

	public bool ShouldSerializeExtraScore()
	{
		return __pbn__ExtraScore.HasValue;
	}

	public void ResetExtraScore()
	{
		__pbn__ExtraScore = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeTransactionStr()
	{
		return __pbn__TransactionStr != null;
	}

	public void ResetTransactionStr()
	{
		__pbn__TransactionStr = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeQuantumMonsterProgress()
	{
		return __pbn__QuantumMonsterProgress.HasValue;
	}

	public void ResetQuantumMonsterProgress()
	{
		__pbn__QuantumMonsterProgress = null;
	}

	public bool ShouldSerializeIsSkipLevel()
	{
		return __pbn__IsSkipLevel.HasValue;
	}

	public void ResetIsSkipLevel()
	{
		__pbn__IsSkipLevel = null;
	}

	public bool ShouldSerializeFinalBossId()
	{
		return __pbn__FinalBossId.HasValue;
	}

	public void ResetFinalBossId()
	{
		__pbn__FinalBossId = null;
	}
}
[ProtoContract]
public class FinishOpenworldEndlessMonsterGroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3425
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

	private uint? __pbn__GroupId;

	private uint? __pbn__Score;

	private uint? __pbn__BattleId;

	private uint? __pbn__OldScore;

	private bool? __pbn__IsBoss;

	private string __pbn__TransactionStr;

	private uint? __pbn__GroupLevel;

	private EndlessType? __pbn__Type;

	private bool? __pbn__IsSkipLevel;

	private uint? __pbn__FinalBossId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "finish_monster_group_list")]
	public uint[] FinishMonsterGroupLists { get; set; }

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

	[ProtoMember(4, Name = "clear_item_list")]
	public List<OpenworldEndlessItem> ClearItemLists { get; } = new List<OpenworldEndlessItem>();


	[ProtoMember(5, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(6, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(7, Name = "old_score")]
	public uint OldScore
	{
		get
		{
			return __pbn__OldScore.GetValueOrDefault();
		}
		set
		{
			__pbn__OldScore = value;
		}
	}

	[ProtoMember(8, Name = "is_boss")]
	public bool IsBoss
	{
		get
		{
			return __pbn__IsBoss.GetValueOrDefault();
		}
		set
		{
			__pbn__IsBoss = value;
		}
	}

	[ProtoMember(9, Name = "transaction_str")]
	[DefaultValue("")]
	public string TransactionStr
	{
		get
		{
			return __pbn__TransactionStr ?? "";
		}
		set
		{
			__pbn__TransactionStr = value;
		}
	}

	[ProtoMember(10, Name = "monster_group_list")]
	public List<OpenworldEndlessMonsterGroup> MonsterGroupLists { get; } = new List<OpenworldEndlessMonsterGroup>();


	[ProtoMember(11, Name = "group_level")]
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

	[ProtoMember(12, Name = "lineup_info")]
	public LineupBriefInfo LineupInfo { get; set; }

	[ProtoMember(13, Name = "type")]
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

	[ProtoMember(14, Name = "quantum_endless")]
	public QuantumEndless QuantumEndless { get; set; }

	[ProtoMember(15, Name = "is_skip_level")]
	public bool IsSkipLevel
	{
		get
		{
			return __pbn__IsSkipLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSkipLevel = value;
		}
	}

	[ProtoMember(16, Name = "final_boss_id")]
	public uint FinalBossId
	{
		get
		{
			return __pbn__FinalBossId.GetValueOrDefault();
		}
		set
		{
			__pbn__FinalBossId = value;
		}
	}

	[ProtoMember(17, Name = "max_score_lineup_info")]
	public LineupBriefInfo MaxScoreLineupInfo { get; set; }

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

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeOldScore()
	{
		return __pbn__OldScore.HasValue;
	}

	public void ResetOldScore()
	{
		__pbn__OldScore = null;
	}

	public bool ShouldSerializeIsBoss()
	{
		return __pbn__IsBoss.HasValue;
	}

	public void ResetIsBoss()
	{
		__pbn__IsBoss = null;
	}

	public bool ShouldSerializeTransactionStr()
	{
		return __pbn__TransactionStr != null;
	}

	public void ResetTransactionStr()
	{
		__pbn__TransactionStr = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeIsSkipLevel()
	{
		return __pbn__IsSkipLevel.HasValue;
	}

	public void ResetIsSkipLevel()
	{
		__pbn__IsSkipLevel = null;
	}

	public bool ShouldSerializeFinalBossId()
	{
		return __pbn__FinalBossId.HasValue;
	}

	public void ResetFinalBossId()
	{
		__pbn__FinalBossId = null;
	}
}
