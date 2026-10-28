using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffEffect : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EffectId;

	private uint? __pbn__EndTime;

	private uint? __pbn__MaxDuring;

	private uint? __pbn__TotalContriNum;

	[ProtoMember(1, Name = "effect_id")]
	public uint EffectId
	{
		get
		{
			return __pbn__EffectId.GetValueOrDefault();
		}
		set
		{
			__pbn__EffectId = value;
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

	[ProtoMember(3, Name = "max_during")]
	public uint MaxDuring
	{
		get
		{
			return __pbn__MaxDuring.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxDuring = value;
		}
	}

	[ProtoMember(4, Name = "total_contri_num")]
	public uint TotalContriNum
	{
		get
		{
			return __pbn__TotalContriNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalContriNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEffectId()
	{
		return __pbn__EffectId.HasValue;
	}

	public void ResetEffectId()
	{
		__pbn__EffectId = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeMaxDuring()
	{
		return __pbn__MaxDuring.HasValue;
	}

	public void ResetMaxDuring()
	{
		__pbn__MaxDuring = null;
	}

	public bool ShouldSerializeTotalContriNum()
	{
		return __pbn__TotalContriNum.HasValue;
	}

	public void ResetTotalContriNum()
	{
		__pbn__TotalContriNum = null;
	}
}
