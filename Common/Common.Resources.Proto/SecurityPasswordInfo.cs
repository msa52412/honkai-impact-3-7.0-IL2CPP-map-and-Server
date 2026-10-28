using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SecurityPasswordInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__RemainFailTimes;

	[ProtoMember(1, Name = "remain_fail_times")]
	public int RemainFailTimes
	{
		get
		{
			return __pbn__RemainFailTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RemainFailTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRemainFailTimes()
	{
		return __pbn__RemainFailTimes.HasValue;
	}

	public void ResetRemainFailTimes()
	{
		__pbn__RemainFailTimes = null;
	}
}
