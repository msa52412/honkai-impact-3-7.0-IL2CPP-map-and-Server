using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleStageEnterTimesLimit : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LimitId;

	private uint? __pbn__EnterTimes;

	[ProtoMember(1, Name = "limit_id")]
	public uint LimitId
	{
		get
		{
			return __pbn__LimitId.GetValueOrDefault();
		}
		set
		{
			__pbn__LimitId = value;
		}
	}

	[ProtoMember(2, Name = "enter_times")]
	public uint EnterTimes
	{
		get
		{
			return __pbn__EnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLimitId()
	{
		return __pbn__LimitId.HasValue;
	}

	public void ResetLimitId()
	{
		__pbn__LimitId = null;
	}

	public bool ShouldSerializeEnterTimes()
	{
		return __pbn__EnterTimes.HasValue;
	}

	public void ResetEnterTimes()
	{
		__pbn__EnterTimes = null;
	}
}
