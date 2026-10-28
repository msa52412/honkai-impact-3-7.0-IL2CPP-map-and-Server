using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WaveRushActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__ActiveBuffSuiteId;

	private uint? __pbn__DailyDropMaterialNum;

	private uint? __pbn__DailyDropLimitNum;

	[ProtoMember(1, Name = "schedule_id")]
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

	[ProtoMember(2, Name = "stage_score_list")]
	public List<WaveRushStageScore> StageScoreLists { get; } = new List<WaveRushStageScore>();


	[ProtoMember(3, Name = "buff_list")]
	public List<WaveRushBuff> BuffLists { get; } = new List<WaveRushBuff>();


	[ProtoMember(4, Name = "buff_suite_list")]
	public List<WaveRushBuffSuite> BuffSuiteLists { get; } = new List<WaveRushBuffSuite>();


	[ProtoMember(5, Name = "active_buff_suite_id")]
	public uint ActiveBuffSuiteId
	{
		get
		{
			return __pbn__ActiveBuffSuiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveBuffSuiteId = value;
		}
	}

	[ProtoMember(6, Name = "cur_daily_stage_id_list")]
	public uint[] CurDailyStageIdLists { get; set; }

	[ProtoMember(7, Name = "daily_drop_material_num")]
	public uint DailyDropMaterialNum
	{
		get
		{
			return __pbn__DailyDropMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyDropMaterialNum = value;
		}
	}

	[ProtoMember(8, Name = "daily_drop_limit_num")]
	public uint DailyDropLimitNum
	{
		get
		{
			return __pbn__DailyDropLimitNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyDropLimitNum = value;
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

	public bool ShouldSerializeActiveBuffSuiteId()
	{
		return __pbn__ActiveBuffSuiteId.HasValue;
	}

	public void ResetActiveBuffSuiteId()
	{
		__pbn__ActiveBuffSuiteId = null;
	}

	public bool ShouldSerializeDailyDropMaterialNum()
	{
		return __pbn__DailyDropMaterialNum.HasValue;
	}

	public void ResetDailyDropMaterialNum()
	{
		__pbn__DailyDropMaterialNum = null;
	}

	public bool ShouldSerializeDailyDropLimitNum()
	{
		return __pbn__DailyDropLimitNum.HasValue;
	}

	public void ResetDailyDropLimitNum()
	{
		__pbn__DailyDropLimitNum = null;
	}
}
