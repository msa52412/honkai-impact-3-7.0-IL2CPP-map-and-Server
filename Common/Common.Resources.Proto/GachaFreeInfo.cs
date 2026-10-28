using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaFreeInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private GachaFreeType? __pbn__FreeType;

	private uint? __pbn__FreeTimes;

	private uint? __pbn__TotalFreeTimes;

	private uint? __pbn__NextAutoRefreshTime;

	[ProtoMember(1, Name = "free_type")]
	[DefaultValue(GachaFreeType.GachaFreeNone)]
	public GachaFreeType FreeType
	{
		get
		{
			return __pbn__FreeType.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeType = value;
		}
	}

	[ProtoMember(2, Name = "free_times")]
	public uint FreeTimes
	{
		get
		{
			return __pbn__FreeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeTimes = value;
		}
	}

	[ProtoMember(3, Name = "total_free_times")]
	public uint TotalFreeTimes
	{
		get
		{
			return __pbn__TotalFreeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalFreeTimes = value;
		}
	}

	[ProtoMember(4, Name = "next_auto_refresh_time")]
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

	public bool ShouldSerializeFreeType()
	{
		return __pbn__FreeType.HasValue;
	}

	public void ResetFreeType()
	{
		__pbn__FreeType = null;
	}

	public bool ShouldSerializeFreeTimes()
	{
		return __pbn__FreeTimes.HasValue;
	}

	public void ResetFreeTimes()
	{
		__pbn__FreeTimes = null;
	}

	public bool ShouldSerializeTotalFreeTimes()
	{
		return __pbn__TotalFreeTimes.HasValue;
	}

	public void ResetTotalFreeTimes()
	{
		__pbn__TotalFreeTimes = null;
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
