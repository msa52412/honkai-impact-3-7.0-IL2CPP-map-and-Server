using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChallengeMissionStep : IExtensible
{
	[ProtoContract]
	public enum StepStatus
	{
		[ProtoEnum(Name = "UNLOCK")]
		Unlock,
		[ProtoEnum(Name = "DOING")]
		Doing,
		[ProtoEnum(Name = "REWARDING")]
		Rewarding,
		[ProtoEnum(Name = "FINISH")]
		Finish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StepId;

	private StepStatus? __pbn__Status;

	private bool? __pbn__HasTakeBonus;

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

	[ProtoMember(2, Name = "status")]
	[DefaultValue(StepStatus.Unlock)]
	public StepStatus Status
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

	[ProtoMember(3, Name = "has_take_bonus")]
	public bool HasTakeBonus
	{
		get
		{
			return __pbn__HasTakeBonus.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeBonus = value;
		}
	}

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

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeHasTakeBonus()
	{
		return __pbn__HasTakeBonus.HasValue;
	}

	public void ResetHasTakeBonus()
	{
		__pbn__HasTakeBonus = null;
	}
}
