using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterAchievementMission : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MissionId;

	private uint? __pbn__FinishTime;

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

	[ProtoMember(2, Name = "finish_time")]
	public uint FinishTime
	{
		get
		{
			return __pbn__FinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishTime = value;
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

	public bool ShouldSerializeFinishTime()
	{
		return __pbn__FinishTime.HasValue;
	}

	public void ResetFinishTime()
	{
		__pbn__FinishTime = null;
	}
}
