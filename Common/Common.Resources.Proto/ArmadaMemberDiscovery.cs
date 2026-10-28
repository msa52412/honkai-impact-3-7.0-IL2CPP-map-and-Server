using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaMemberDiscovery : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__WeekDiscovery;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "week_discovery")]
	public uint WeekDiscovery
	{
		get
		{
			return __pbn__WeekDiscovery.GetValueOrDefault();
		}
		set
		{
			__pbn__WeekDiscovery = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeWeekDiscovery()
	{
		return __pbn__WeekDiscovery.HasValue;
	}

	public void ResetWeekDiscovery()
	{
		__pbn__WeekDiscovery = null;
	}
}
