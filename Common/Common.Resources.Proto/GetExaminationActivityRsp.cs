using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetExaminationActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4324
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

	private uint? __pbn__ScheduleId;

	private uint? __pbn__CurExaminationIdx;

	private uint? __pbn__FinishedRound;

	private uint? __pbn__ExaminationStageId;

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

	[ProtoMember(3, Name = "cur_examination_idx")]
	public uint CurExaminationIdx
	{
		get
		{
			return __pbn__CurExaminationIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__CurExaminationIdx = value;
		}
	}

	[ProtoMember(4, Name = "finished_round")]
	public uint FinishedRound
	{
		get
		{
			return __pbn__FinishedRound.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishedRound = value;
		}
	}

	[ProtoMember(5, Name = "examination_list")]
	public List<Examination> ExaminationLists { get; } = new List<Examination>();


	[ProtoMember(6, Name = "examination_stage_id")]
	public uint ExaminationStageId
	{
		get
		{
			return __pbn__ExaminationStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExaminationStageId = value;
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

	public bool ShouldSerializeCurExaminationIdx()
	{
		return __pbn__CurExaminationIdx.HasValue;
	}

	public void ResetCurExaminationIdx()
	{
		__pbn__CurExaminationIdx = null;
	}

	public bool ShouldSerializeFinishedRound()
	{
		return __pbn__FinishedRound.HasValue;
	}

	public void ResetFinishedRound()
	{
		__pbn__FinishedRound = null;
	}

	public bool ShouldSerializeExaminationStageId()
	{
		return __pbn__ExaminationStageId.HasValue;
	}

	public void ResetExaminationStageId()
	{
		__pbn__ExaminationStageId = null;
	}
}
