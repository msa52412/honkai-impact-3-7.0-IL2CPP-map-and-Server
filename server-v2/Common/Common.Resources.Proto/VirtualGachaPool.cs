using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualGachaPool : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	private uint? __pbn__GachaTimes;

	private uint? __pbn__TotalTimes;

	[ProtoMember(1, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	[ProtoMember(2, Name = "gacha_times")]
	public uint GachaTimes
	{
		get
		{
			return __pbn__GachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaTimes = value;
		}
	}

	[ProtoMember(3, Name = "total_times")]
	public uint TotalTimes
	{
		get
		{
			return __pbn__TotalTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}

	public bool ShouldSerializeGachaTimes()
	{
		return __pbn__GachaTimes.HasValue;
	}

	public void ResetGachaTimes()
	{
		__pbn__GachaTimes = null;
	}

	public bool ShouldSerializeTotalTimes()
	{
		return __pbn__TotalTimes.HasValue;
	}

	public void ResetTotalTimes()
	{
		__pbn__TotalTimes = null;
	}
}
