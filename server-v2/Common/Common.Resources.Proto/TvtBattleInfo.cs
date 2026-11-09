using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleInfo : IExtensible
{
	[ProtoContract]
	public enum BattlePhase
	{
		[ProtoEnum(Name = "PHASE_INVALID")]
		PhaseInvalid,
		[ProtoEnum(Name = "PHASE_CONFIRM")]
		PhaseConfirm,
		[ProtoEnum(Name = "PHASE_PREPARE")]
		PhasePrepare,
		[ProtoEnum(Name = "PHASE_FIGHT")]
		PhaseFight,
		[ProtoEnum(Name = "PHASE_FINISH")]
		PhaseFinish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleId;

	private uint? __pbn__BattleStageId;

	private BattlePhase? __pbn__Phase;

	private uint? __pbn__PhaseBeginTime;

	private uint? __pbn__PhaseEndTime;

	private bool? __pbn__IsRobotMatch;

	private uint? __pbn__BattleType;

	private uint? __pbn__BattleHardLevelGroup;

	private uint? __pbn__BattleHardLevel;

	[ProtoMember(1, Name = "battle_id")]
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

	[ProtoMember(2, Name = "battle_stage_id")]
	public uint BattleStageId
	{
		get
		{
			return __pbn__BattleStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleStageId = value;
		}
	}

	[ProtoMember(3, Name = "phase")]
	[DefaultValue(BattlePhase.PhaseInvalid)]
	public BattlePhase Phase
	{
		get
		{
			return __pbn__Phase.GetValueOrDefault();
		}
		set
		{
			__pbn__Phase = value;
		}
	}

	[ProtoMember(4, Name = "phase_begin_time")]
	public uint PhaseBeginTime
	{
		get
		{
			return __pbn__PhaseBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PhaseBeginTime = value;
		}
	}

	[ProtoMember(5, Name = "phase_end_time")]
	public uint PhaseEndTime
	{
		get
		{
			return __pbn__PhaseEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PhaseEndTime = value;
		}
	}

	[ProtoMember(6, Name = "lhs_member_list")]
	public List<TvtBattleMember> LhsMemberLists { get; } = new List<TvtBattleMember>();


	[ProtoMember(7, Name = "rhs_member_list")]
	public List<TvtBattleMember> RhsMemberLists { get; } = new List<TvtBattleMember>();


	[ProtoMember(8, Name = "is_robot_match")]
	public bool IsRobotMatch
	{
		get
		{
			return __pbn__IsRobotMatch.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRobotMatch = value;
		}
	}

	[ProtoMember(9, Name = "battle_type")]
	public uint BattleType
	{
		get
		{
			return __pbn__BattleType.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleType = value;
		}
	}

	[ProtoMember(10, Name = "battle_hard_level_group")]
	public uint BattleHardLevelGroup
	{
		get
		{
			return __pbn__BattleHardLevelGroup.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleHardLevelGroup = value;
		}
	}

	[ProtoMember(11, Name = "battle_hard_level")]
	public uint BattleHardLevel
	{
		get
		{
			return __pbn__BattleHardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleHardLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeBattleStageId()
	{
		return __pbn__BattleStageId.HasValue;
	}

	public void ResetBattleStageId()
	{
		__pbn__BattleStageId = null;
	}

	public bool ShouldSerializePhase()
	{
		return __pbn__Phase.HasValue;
	}

	public void ResetPhase()
	{
		__pbn__Phase = null;
	}

	public bool ShouldSerializePhaseBeginTime()
	{
		return __pbn__PhaseBeginTime.HasValue;
	}

	public void ResetPhaseBeginTime()
	{
		__pbn__PhaseBeginTime = null;
	}

	public bool ShouldSerializePhaseEndTime()
	{
		return __pbn__PhaseEndTime.HasValue;
	}

	public void ResetPhaseEndTime()
	{
		__pbn__PhaseEndTime = null;
	}

	public bool ShouldSerializeIsRobotMatch()
	{
		return __pbn__IsRobotMatch.HasValue;
	}

	public void ResetIsRobotMatch()
	{
		__pbn__IsRobotMatch = null;
	}

	public bool ShouldSerializeBattleType()
	{
		return __pbn__BattleType.HasValue;
	}

	public void ResetBattleType()
	{
		__pbn__BattleType = null;
	}

	public bool ShouldSerializeBattleHardLevelGroup()
	{
		return __pbn__BattleHardLevelGroup.HasValue;
	}

	public void ResetBattleHardLevelGroup()
	{
		__pbn__BattleHardLevelGroup = null;
	}

	public bool ShouldSerializeBattleHardLevel()
	{
		return __pbn__BattleHardLevel.HasValue;
	}

	public void ResetBattleHardLevel()
	{
		__pbn__BattleHardLevel = null;
	}
}
