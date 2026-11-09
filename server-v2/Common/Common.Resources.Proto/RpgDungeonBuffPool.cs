using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgDungeonBuffPool : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	private uint? __pbn__LeftTimes;

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

	[ProtoMember(2, Name = "left_times")]
	public uint LeftTimes
	{
		get
		{
			return __pbn__LeftTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftTimes = value;
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

	public bool ShouldSerializeLeftTimes()
	{
		return __pbn__LeftTimes.HasValue;
	}

	public void ResetLeftTimes()
	{
		__pbn__LeftTimes = null;
	}
}
