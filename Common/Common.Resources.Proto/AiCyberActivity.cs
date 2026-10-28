using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AiCyberActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__RepairProgress;

	private uint? __pbn__HyperionId;

	private uint? __pbn__MainUiId;

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

	[ProtoMember(2, Name = "repair_progress")]
	public uint RepairProgress
	{
		get
		{
			return __pbn__RepairProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__RepairProgress = value;
		}
	}

	[ProtoMember(3, Name = "area_list")]
	public List<AiCyberArea> AreaLists { get; } = new List<AiCyberArea>();


	[ProtoMember(4, Name = "daily_stage_drop_list")]
	public List<AiCyberDailyStageDrop> DailyStageDropLists { get; } = new List<AiCyberDailyStageDrop>();


	[ProtoMember(5, Name = "hyperion_id")]
	public uint HyperionId
	{
		get
		{
			return __pbn__HyperionId.GetValueOrDefault();
		}
		set
		{
			__pbn__HyperionId = value;
		}
	}

	[ProtoMember(6, Name = "main_ui_id")]
	public uint MainUiId
	{
		get
		{
			return __pbn__MainUiId.GetValueOrDefault();
		}
		set
		{
			__pbn__MainUiId = value;
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

	public bool ShouldSerializeRepairProgress()
	{
		return __pbn__RepairProgress.HasValue;
	}

	public void ResetRepairProgress()
	{
		__pbn__RepairProgress = null;
	}

	public bool ShouldSerializeHyperionId()
	{
		return __pbn__HyperionId.HasValue;
	}

	public void ResetHyperionId()
	{
		__pbn__HyperionId = null;
	}

	public bool ShouldSerializeMainUiId()
	{
		return __pbn__MainUiId.HasValue;
	}

	public void ResetMainUiId()
	{
		__pbn__MainUiId = null;
	}
}
