using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PictureActivityChooseReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4268
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__StepId;

	private uint? __pbn__ChoiceId;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "step_id")]
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

	[ProtoMember(3, Name = "choice_id")]
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

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
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
