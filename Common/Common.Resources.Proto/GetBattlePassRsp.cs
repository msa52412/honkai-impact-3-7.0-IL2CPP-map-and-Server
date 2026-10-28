using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetBattlePassRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3751
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__HasTakeRewardLevel;

	private uint? __pbn__PhaseMaxExp;

	private uint? __pbn__PhaseExp;

	private bool? __pbn__IsTakePhaseFreeExp;

	private uint? __pbn__PhaseEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "level")]
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

	[ProtoMember(4, Name = "exp")]
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

	[ProtoMember(5, Name = "has_take_reward_level")]
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

	[ProtoMember(6, Name = "has_got_ticket_list")]
	public uint[] HasGotTicketLists { get; set; }

	[ProtoMember(7, Name = "phase_max_exp")]
	public uint PhaseMaxExp
	{
		get
		{
			return __pbn__PhaseMaxExp.GetValueOrDefault();
		}
		set
		{
			__pbn__PhaseMaxExp = value;
		}
	}

	[ProtoMember(8, Name = "phase_exp")]
	public uint PhaseExp
	{
		get
		{
			return __pbn__PhaseExp.GetValueOrDefault();
		}
		set
		{
			__pbn__PhaseExp = value;
		}
	}

	[ProtoMember(9, Name = "is_take_phase_free_exp")]
	public bool IsTakePhaseFreeExp
	{
		get
		{
			return __pbn__IsTakePhaseFreeExp.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakePhaseFreeExp = value;
		}
	}

	[ProtoMember(10, Name = "battle_pass_mission_list")]
	public List<BattlePassMission> BattlePassMissionLists { get; } = new List<BattlePassMission>();


	[ProtoMember(11, Name = "phase_end_time")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
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

	public bool ShouldSerializePhaseMaxExp()
	{
		return __pbn__PhaseMaxExp.HasValue;
	}

	public void ResetPhaseMaxExp()
	{
		__pbn__PhaseMaxExp = null;
	}

	public bool ShouldSerializePhaseExp()
	{
		return __pbn__PhaseExp.HasValue;
	}

	public void ResetPhaseExp()
	{
		__pbn__PhaseExp = null;
	}

	public bool ShouldSerializeIsTakePhaseFreeExp()
	{
		return __pbn__IsTakePhaseFreeExp.HasValue;
	}

	public void ResetIsTakePhaseFreeExp()
	{
		__pbn__IsTakePhaseFreeExp = null;
	}

	public bool ShouldSerializePhaseEndTime()
	{
		return __pbn__PhaseEndTime.HasValue;
	}

	public void ResetPhaseEndTime()
	{
		__pbn__PhaseEndTime = null;
	}
}
