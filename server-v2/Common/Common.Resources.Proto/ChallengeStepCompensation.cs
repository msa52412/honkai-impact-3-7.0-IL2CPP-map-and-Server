using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChallengeStepCompensation : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CompensationId;

	private bool? __pbn__IsTakeCompensation;

	[ProtoMember(1, Name = "compensation_id")]
	public uint CompensationId
	{
		get
		{
			return __pbn__CompensationId.GetValueOrDefault();
		}
		set
		{
			__pbn__CompensationId = value;
		}
	}

	[ProtoMember(2, Name = "is_take_compensation")]
	public bool IsTakeCompensation
	{
		get
		{
			return __pbn__IsTakeCompensation.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeCompensation = value;
		}
	}

	[ProtoMember(3, Name = "new_challenge_step_compensation_list")]
	public List<StepCompensation> NewChallengeStepCompensationLists { get; } = new List<StepCompensation>();


	[ProtoMember(4, Name = "old_challenge_step_compensation_list")]
	public List<StepCompensation> OldChallengeStepCompensationLists { get; } = new List<StepCompensation>();


	[ProtoMember(5, Name = "mainline_step_compensation_list")]
	public List<StepCompensation> MainlineStepCompensationLists { get; } = new List<StepCompensation>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCompensationId()
	{
		return __pbn__CompensationId.HasValue;
	}

	public void ResetCompensationId()
	{
		__pbn__CompensationId = null;
	}

	public bool ShouldSerializeIsTakeCompensation()
	{
		return __pbn__IsTakeCompensation.HasValue;
	}

	public void ResetIsTakeCompensation()
	{
		__pbn__IsTakeCompensation = null;
	}
}
