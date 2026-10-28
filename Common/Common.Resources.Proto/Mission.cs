using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Mission : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MissionId;

	private MissionStatus? __pbn__Status;

	private uint? __pbn__Progress;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__Priority;

	private uint? __pbn__FinishedTimes;

	private uint? __pbn__FinishedTimesLimit;

	private uint? __pbn__CycleId;

	[ProtoMember(1, Name = "mission_id")]
	public uint MissionId
	{
		get
		{
			return __pbn__MissionId.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionId = value;
		}
	}

	[ProtoMember(2, Name = "status")]
	[DefaultValue(MissionStatus.MissionStatusNone)]
	public MissionStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(3, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(4, Name = "begin_time")]
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

	[ProtoMember(5, Name = "end_time")]
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

	[ProtoMember(6, Name = "priority")]
	public uint Priority
	{
		get
		{
			return __pbn__Priority.GetValueOrDefault();
		}
		set
		{
			__pbn__Priority = value;
		}
	}

	[ProtoMember(7, Name = "finished_times")]
	public uint FinishedTimes
	{
		get
		{
			return __pbn__FinishedTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishedTimes = value;
		}
	}

	[ProtoMember(8, Name = "finished_times_limit")]
	public uint FinishedTimesLimit
	{
		get
		{
			return __pbn__FinishedTimesLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishedTimesLimit = value;
		}
	}

	[ProtoMember(9, Name = "cycle_id")]
	public uint CycleId
	{
		get
		{
			return __pbn__CycleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CycleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMissionId()
	{
		return __pbn__MissionId.HasValue;
	}

	public void ResetMissionId()
	{
		__pbn__MissionId = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializePriority()
	{
		return __pbn__Priority.HasValue;
	}

	public void ResetPriority()
	{
		__pbn__Priority = null;
	}

	public bool ShouldSerializeFinishedTimes()
	{
		return __pbn__FinishedTimes.HasValue;
	}

	public void ResetFinishedTimes()
	{
		__pbn__FinishedTimes = null;
	}

	public bool ShouldSerializeFinishedTimesLimit()
	{
		return __pbn__FinishedTimesLimit.HasValue;
	}

	public void ResetFinishedTimesLimit()
	{
		__pbn__FinishedTimesLimit = null;
	}

	public bool ShouldSerializeCycleId()
	{
		return __pbn__CycleId.HasValue;
	}

	public void ResetCycleId()
	{
		__pbn__CycleId = null;
	}
}
