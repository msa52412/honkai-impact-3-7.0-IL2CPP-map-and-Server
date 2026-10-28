using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PrivilegeRightInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RightId;

	private uint? __pbn__Type;

	private uint? __pbn__TotalUsedTimes;

	private uint? __pbn__MaxUseTimes;

	private uint? __pbn__UseTimes;

	private uint? __pbn__NextAutoRefreshTime;

	[ProtoMember(1, Name = "right_id")]
	public uint RightId
	{
		get
		{
			return __pbn__RightId.GetValueOrDefault();
		}
		set
		{
			__pbn__RightId = value;
		}
	}

	[ProtoMember(2, Name = "type")]
	public uint Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "total_used_times")]
	public uint TotalUsedTimes
	{
		get
		{
			return __pbn__TotalUsedTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalUsedTimes = value;
		}
	}

	[ProtoMember(4, Name = "max_use_times")]
	public uint MaxUseTimes
	{
		get
		{
			return __pbn__MaxUseTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxUseTimes = value;
		}
	}

	[ProtoMember(5, Name = "use_times")]
	public uint UseTimes
	{
		get
		{
			return __pbn__UseTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__UseTimes = value;
		}
	}

	[ProtoMember(6, Name = "next_auto_refresh_time")]
	public uint NextAutoRefreshTime
	{
		get
		{
			return __pbn__NextAutoRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextAutoRefreshTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRightId()
	{
		return __pbn__RightId.HasValue;
	}

	public void ResetRightId()
	{
		__pbn__RightId = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeTotalUsedTimes()
	{
		return __pbn__TotalUsedTimes.HasValue;
	}

	public void ResetTotalUsedTimes()
	{
		__pbn__TotalUsedTimes = null;
	}

	public bool ShouldSerializeMaxUseTimes()
	{
		return __pbn__MaxUseTimes.HasValue;
	}

	public void ResetMaxUseTimes()
	{
		__pbn__MaxUseTimes = null;
	}

	public bool ShouldSerializeUseTimes()
	{
		return __pbn__UseTimes.HasValue;
	}

	public void ResetUseTimes()
	{
		__pbn__UseTimes = null;
	}

	public bool ShouldSerializeNextAutoRefreshTime()
	{
		return __pbn__NextAutoRefreshTime.HasValue;
	}

	public void ResetNextAutoRefreshTime()
	{
		__pbn__NextAutoRefreshTime = null;
	}
}
