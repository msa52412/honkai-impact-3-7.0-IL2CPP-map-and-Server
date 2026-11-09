using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaReunionMission : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MissionId;

	private uint? __pbn__FinishTimes;

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

	[ProtoMember(2, Name = "finish_times")]
	public uint FinishTimes
	{
		get
		{
			return __pbn__FinishTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishTimes = value;
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

	public bool ShouldSerializeFinishTimes()
	{
		return __pbn__FinishTimes.HasValue;
	}

	public void ResetFinishTimes()
	{
		__pbn__FinishTimes = null;
	}
}
