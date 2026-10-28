using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleResult : IExtensible
{
	[ProtoContract]
	public enum BattleStatus
	{
		[ProtoEnum(Name = "INVALID")]
		Invalid,
		[ProtoEnum(Name = "LEFT_WIN")]
		LeftWin,
		[ProtoEnum(Name = "RIGHT_WIN")]
		RightWin
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleStageId;

	private uint? __pbn__BeginTime;

	private BattleStatus? __pbn__battle_status;

	private uint? __pbn__BattleType;

	private bool? __pbn__IsRobotMatch;

	private uint? __pbn__EndTime;

	[ProtoMember(1, Name = "battle_stage_id")]
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

	[ProtoMember(2, Name = "begin_time")]
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

	[ProtoMember(3)]
	[DefaultValue(BattleStatus.Invalid)]
	public BattleStatus battle_status
	{
		get
		{
			return __pbn__battle_status.GetValueOrDefault();
		}
		set
		{
			__pbn__battle_status = value;
		}
	}

	[ProtoMember(4, Name = "lhs_member_stat_list")]
	public List<TvtMemberStat> LhsMemberStatLists { get; } = new List<TvtMemberStat>();


	[ProtoMember(5, Name = "rhs_member_stat_list")]
	public List<TvtMemberStat> RhsMemberStatLists { get; } = new List<TvtMemberStat>();


	[ProtoMember(6, Name = "battle_type")]
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

	[ProtoMember(7, Name = "is_robot_match")]
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

	[ProtoMember(8, Name = "end_time")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleStageId()
	{
		return __pbn__BattleStageId.HasValue;
	}

	public void ResetBattleStageId()
	{
		__pbn__BattleStageId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializebattle_status()
	{
		return __pbn__battle_status.HasValue;
	}

	public void Resetbattle_status()
	{
		__pbn__battle_status = null;
	}

	public bool ShouldSerializeBattleType()
	{
		return __pbn__BattleType.HasValue;
	}

	public void ResetBattleType()
	{
		__pbn__BattleType = null;
	}

	public bool ShouldSerializeIsRobotMatch()
	{
		return __pbn__IsRobotMatch.HasValue;
	}

	public void ResetIsRobotMatch()
	{
		__pbn__IsRobotMatch = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}
}
