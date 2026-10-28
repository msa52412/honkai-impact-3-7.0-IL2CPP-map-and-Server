using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StepCompensation : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StepId;

	private uint? __pbn__StepRewardId;

	[ProtoMember(1, Name = "step_id")]
	public uint StepId
	{
		get
		{
			return __pbn__StepId.GetValueOrDefault();
		}
		set
		{
			__pbn__StepId = value;
		}
	}

	[ProtoMember(2, Name = "step_reward_id")]
	public uint StepRewardId
	{
		get
		{
			return __pbn__StepRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__StepRewardId = value;
		}
	}

	[ProtoMember(3, Name = "mission_list")]
	public List<StepMissionCompensation> MissionLists { get; } = new List<StepMissionCompensation>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStepId()
	{
		return __pbn__StepId.HasValue;
	}

	public void ResetStepId()
	{
		__pbn__StepId = null;
	}

	public bool ShouldSerializeStepRewardId()
	{
		return __pbn__StepRewardId.HasValue;
	}

	public void ResetStepRewardId()
	{
		__pbn__StepRewardId = null;
	}
}
