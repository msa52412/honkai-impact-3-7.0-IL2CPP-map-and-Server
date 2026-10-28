using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MissionCompensationReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MissionId;

	private uint? __pbn__MissionCompensationRewardId;

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

	[ProtoMember(2, Name = "mission_compensation_reward_id")]
	public uint MissionCompensationRewardId
	{
		get
		{
			return __pbn__MissionCompensationRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionCompensationRewardId = value;
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

	public bool ShouldSerializeMissionCompensationRewardId()
	{
		return __pbn__MissionCompensationRewardId.HasValue;
	}

	public void ResetMissionCompensationRewardId()
	{
		__pbn__MissionCompensationRewardId = null;
	}
}
