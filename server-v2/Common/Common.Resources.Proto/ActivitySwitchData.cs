using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActivitySwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityType;

	[ProtoMember(1, Name = "activity_type")]
	public uint ActivityType
	{
		get
		{
			return __pbn__ActivityType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
	}
}
