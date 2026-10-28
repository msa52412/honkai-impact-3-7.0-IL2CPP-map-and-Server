using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGratuityActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4101
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

	private uint? __pbn__CurScheduleId;

	private uint? __pbn__TodayLeftFreeStageNum;

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

	[ProtoMember(3, Name = "stage_list")]
	public List<GratuityStage> StageLists { get; } = new List<GratuityStage>();


	[ProtoMember(4, Name = "today_left_free_stage_num")]
	public uint TodayLeftFreeStageNum
	{
		get
		{
			return __pbn__TodayLeftFreeStageNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayLeftFreeStageNum = value;
		}
	}

	[ProtoMember(5, Name = "invite_stage_list")]
	public List<GratuityStage> InviteStageLists { get; } = new List<GratuityStage>();


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

	public bool ShouldSerializeTodayLeftFreeStageNum()
	{
		return __pbn__TodayLeftFreeStageNum.HasValue;
	}

	public void ResetTodayLeftFreeStageNum()
	{
		__pbn__TodayLeftFreeStageNum = null;
	}
}
