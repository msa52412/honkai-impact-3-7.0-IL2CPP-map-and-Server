using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PictureStep : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StepId;

	private uint? __pbn__ChoiceId;

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

	[ProtoMember(2, Name = "choice_id")]
	public uint ChoiceId
	{
		get
		{
			return __pbn__ChoiceId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChoiceId = value;
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

	public bool ShouldSerializeChoiceId()
	{
		return __pbn__ChoiceId.HasValue;
	}

	public void ResetChoiceId()
	{
		__pbn__ChoiceId = null;
	}
}
