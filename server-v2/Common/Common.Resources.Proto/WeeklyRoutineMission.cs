using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WeeklyRoutineMission : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MissionId;

	private uint? __pbn__CurContribution;

	private uint? __pbn__WeeklyContribution;

	private uint? __pbn__MissionFinishTimes;

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

	[ProtoMember(2, Name = "cur_contribution")]
	public uint CurContribution
	{
		get
		{
			return __pbn__CurContribution.GetValueOrDefault();
		}
		set
		{
			__pbn__CurContribution = value;
		}
	}

	[ProtoMember(3, Name = "weekly_contribution")]
	public uint WeeklyContribution
	{
		get
		{
			return __pbn__WeeklyContribution.GetValueOrDefault();
		}
		set
		{
			__pbn__WeeklyContribution = value;
		}
	}

	[ProtoMember(4, Name = "mission_finish_times")]
	public uint MissionFinishTimes
	{
		get
		{
			return __pbn__MissionFinishTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionFinishTimes = value;
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

	public bool ShouldSerializeCurContribution()
	{
		return __pbn__CurContribution.HasValue;
	}

	public void ResetCurContribution()
	{
		__pbn__CurContribution = null;
	}

	public bool ShouldSerializeWeeklyContribution()
	{
		return __pbn__WeeklyContribution.HasValue;
	}

	public void ResetWeeklyContribution()
	{
		__pbn__WeeklyContribution = null;
	}

	public bool ShouldSerializeMissionFinishTimes()
	{
		return __pbn__MissionFinishTimes.HasValue;
	}

	public void ResetMissionFinishTimes()
	{
		__pbn__MissionFinishTimes = null;
	}
}
