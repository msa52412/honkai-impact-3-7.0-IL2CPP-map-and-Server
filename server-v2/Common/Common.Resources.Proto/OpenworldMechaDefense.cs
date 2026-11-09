using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldMechaDefense : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LastEnterTime;

	private uint? __pbn__LeftEnterTimes;

	private uint? __pbn__LastRating;

	[ProtoMember(1, Name = "last_enter_time")]
	public uint LastEnterTime
	{
		get
		{
			return __pbn__LastEnterTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastEnterTime = value;
		}
	}

	[ProtoMember(2, Name = "left_enter_times")]
	public uint LeftEnterTimes
	{
		get
		{
			return __pbn__LeftEnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftEnterTimes = value;
		}
	}

	[ProtoMember(3, Name = "last_rating")]
	public uint LastRating
	{
		get
		{
			return __pbn__LastRating.GetValueOrDefault();
		}
		set
		{
			__pbn__LastRating = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLastEnterTime()
	{
		return __pbn__LastEnterTime.HasValue;
	}

	public void ResetLastEnterTime()
	{
		__pbn__LastEnterTime = null;
	}

	public bool ShouldSerializeLeftEnterTimes()
	{
		return __pbn__LeftEnterTimes.HasValue;
	}

	public void ResetLeftEnterTimes()
	{
		__pbn__LeftEnterTimes = null;
	}

	public bool ShouldSerializeLastRating()
	{
		return __pbn__LastRating.HasValue;
	}

	public void ResetLastRating()
	{
		__pbn__LastRating = null;
	}
}
