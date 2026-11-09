using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWeeklyRoutineActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4230
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_SCHEDULE")]
		NotInSchedule
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScheduleId;

	private uint? __pbn__WeeklyRoutineContribution;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_schedule_id")]
	public uint CurScheduleId
	{
		get
		{
			return __pbn__CurScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "weekly_routine_mission_list")]
	public List<WeeklyRoutineMission> WeeklyRoutineMissionLists { get; } = new List<WeeklyRoutineMission>();


	[ProtoMember(4, Name = "has_take_routine_reward_id_list")]
	public uint[] HasTakeRoutineRewardIdLists { get; set; }

	[ProtoMember(5, Name = "weekly_routine_contribution")]
	public uint WeeklyRoutineContribution
	{
		get
		{
			return __pbn__WeeklyRoutineContribution.GetValueOrDefault();
		}
		set
		{
			__pbn__WeeklyRoutineContribution = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurScheduleId()
	{
		return __pbn__CurScheduleId.HasValue;
	}

	public void ResetCurScheduleId()
	{
		__pbn__CurScheduleId = null;
	}

	public bool ShouldSerializeWeeklyRoutineContribution()
	{
		return __pbn__WeeklyRoutineContribution.HasValue;
	}

	public void ResetWeeklyRoutineContribution()
	{
		__pbn__WeeklyRoutineContribution = null;
	}
}
