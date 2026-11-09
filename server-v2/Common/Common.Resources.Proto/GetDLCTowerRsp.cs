using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDLCTowerRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3575
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

	private uint? __pbn__Progress;

	private uint? __pbn__TowerCostTime;

	private uint? __pbn__MaxProgress;

	private uint? __pbn__AvatarReviveTimes;

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

	[ProtoMember(4, Name = "avatar_info_list")]
	public List<DLCTowerAvatarInfo> AvatarInfoLists { get; } = new List<DLCTowerAvatarInfo>();


	[ProtoMember(5, Name = "tower_cost_time")]
	public uint TowerCostTime
	{
		get
		{
			return __pbn__TowerCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerCostTime = value;
		}
	}

	[ProtoMember(6, Name = "max_progress")]
	public uint MaxProgress
	{
		get
		{
			return __pbn__MaxProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxProgress = value;
		}
	}

	[ProtoMember(7, Name = "avatar_revive_times")]
	public uint AvatarReviveTimes
	{
		get
		{
			return __pbn__AvatarReviveTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarReviveTimes = value;
		}
	}

	[ProtoMember(8, Name = "floor_max_score_list")]
	public List<DLCTowerFloorMaxScore> FloorMaxScoreLists { get; } = new List<DLCTowerFloorMaxScore>();


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

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeTowerCostTime()
	{
		return __pbn__TowerCostTime.HasValue;
	}

	public void ResetTowerCostTime()
	{
		__pbn__TowerCostTime = null;
	}

	public bool ShouldSerializeMaxProgress()
	{
		return __pbn__MaxProgress.HasValue;
	}

	public void ResetMaxProgress()
	{
		__pbn__MaxProgress = null;
	}

	public bool ShouldSerializeAvatarReviveTimes()
	{
		return __pbn__AvatarReviveTimes.HasValue;
	}

	public void ResetAvatarReviveTimes()
	{
		__pbn__AvatarReviveTimes = null;
	}
}
