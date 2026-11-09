using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchSynthesisMonsterReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5835
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__SynthesisId;

	private uint? __pbn__MainUniqueId;

	private uint? __pbn__AssistUniqueId;

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

	[ProtoMember(2, Name = "synthesis_id")]
	public uint SynthesisId
	{
		get
		{
			return __pbn__SynthesisId.GetValueOrDefault();
		}
		set
		{
			__pbn__SynthesisId = value;
		}
	}

	[ProtoMember(3, Name = "main_unique_id")]
	public uint MainUniqueId
	{
		get
		{
			return __pbn__MainUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__MainUniqueId = value;
		}
	}

	[ProtoMember(4, Name = "assist_unique_id")]
	public uint AssistUniqueId
	{
		get
		{
			return __pbn__AssistUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistUniqueId = value;
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

	public bool ShouldSerializeSynthesisId()
	{
		return __pbn__SynthesisId.HasValue;
	}

	public void ResetSynthesisId()
	{
		__pbn__SynthesisId = null;
	}

	public bool ShouldSerializeMainUniqueId()
	{
		return __pbn__MainUniqueId.HasValue;
	}

	public void ResetMainUniqueId()
	{
		__pbn__MainUniqueId = null;
	}

	public bool ShouldSerializeAssistUniqueId()
	{
		return __pbn__AssistUniqueId.HasValue;
	}

	public void ResetAssistUniqueId()
	{
		__pbn__AssistUniqueId = null;
	}
}
