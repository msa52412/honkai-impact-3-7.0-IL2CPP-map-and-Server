using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActivateTrialAvatarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 583
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SampleId;

	[ProtoMember(1, Name = "sample_id")]
	public uint SampleId
	{
		get
		{
			return __pbn__SampleId.GetValueOrDefault();
		}
		set
		{
			__pbn__SampleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSampleId()
	{
		return __pbn__SampleId.HasValue;
	}

	public void ResetSampleId()
	{
		__pbn__SampleId = null;
	}
}
