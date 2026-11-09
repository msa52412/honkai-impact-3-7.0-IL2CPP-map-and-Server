using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MissionStepCompensation : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TypeId;

	private uint? __pbn__OldStepId;

	private uint? __pbn__NewStepId;

	private uint? __pbn__OldStepCompensationRewardId;

	private uint? __pbn__OldStepRewardId;

	[ProtoMember(1, Name = "type_id")]
	public uint TypeId
	{
		get
		{
			return __pbn__TypeId.GetValueOrDefault();
		}
		set
		{
			__pbn__TypeId = value;
		}
	}

	[ProtoMember(2, Name = "old_step_id")]
	public uint OldStepId
	{
		get
		{
			return __pbn__OldStepId.GetValueOrDefault();
		}
		set
		{
			__pbn__OldStepId = value;
		}
	}

	[ProtoMember(3, Name = "new_step_id")]
	public uint NewStepId
	{
		get
		{
			return __pbn__NewStepId.GetValueOrDefault();
		}
		set
		{
			__pbn__NewStepId = value;
		}
	}

	[ProtoMember(4, Name = "old_step_compensation_reward_id")]
	public uint OldStepCompensationRewardId
	{
		get
		{
			return __pbn__OldStepCompensationRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__OldStepCompensationRewardId = value;
		}
	}

	[ProtoMember(5, Name = "old_step_reward_id")]
	public uint OldStepRewardId
	{
		get
		{
			return __pbn__OldStepRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__OldStepRewardId = value;
		}
	}

	[ProtoMember(6, Name = "mission_compensation_list")]
	public List<MissionCompensationReward> MissionCompensationLists { get; } = new List<MissionCompensationReward>();


	[ProtoMember(7, Name = "old_step_compensation_reward_id_list")]
	public uint[] OldStepCompensationRewardIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTypeId()
	{
		return __pbn__TypeId.HasValue;
	}

	public void ResetTypeId()
	{
		__pbn__TypeId = null;
	}

	public bool ShouldSerializeOldStepId()
	{
		return __pbn__OldStepId.HasValue;
	}

	public void ResetOldStepId()
	{
		__pbn__OldStepId = null;
	}

	public bool ShouldSerializeNewStepId()
	{
		return __pbn__NewStepId.HasValue;
	}

	public void ResetNewStepId()
	{
		__pbn__NewStepId = null;
	}

	public bool ShouldSerializeOldStepCompensationRewardId()
	{
		return __pbn__OldStepCompensationRewardId.HasValue;
	}

	public void ResetOldStepCompensationRewardId()
	{
		__pbn__OldStepCompensationRewardId = null;
	}

	public bool ShouldSerializeOldStepRewardId()
	{
		return __pbn__OldStepRewardId.HasValue;
	}

	public void ResetOldStepRewardId()
	{
		__pbn__OldStepRewardId = null;
	}
}
