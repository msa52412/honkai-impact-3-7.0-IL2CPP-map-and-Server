using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TrialWarship : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SampleId;

	private uint? __pbn__EndTime;

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

	[ProtoMember(2, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
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

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}
}
