using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScheduledMapSiteActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EnterTimes;

	[ProtoMember(1, Name = "stage_group_schedule_list")]
	public List<GeneralActivityStageGroupScheduleInfo> StageGroupScheduleLists { get; } = new List<GeneralActivityStageGroupScheduleInfo>();


	[ProtoMember(2, Name = "enter_times")]
	public uint EnterTimes
	{
		get
		{
			return __pbn__EnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEnterTimes()
	{
		return __pbn__EnterTimes.HasValue;
	}

	public void ResetEnterTimes()
	{
		__pbn__EnterTimes = null;
	}
}
