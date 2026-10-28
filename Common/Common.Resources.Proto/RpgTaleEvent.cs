using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleEvent : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EventId;

	private uint? __pbn__EventType;

	[ProtoMember(1, Name = "event_id")]
	public uint EventId
	{
		get
		{
			return __pbn__EventId.GetValueOrDefault();
		}
		set
		{
			__pbn__EventId = value;
		}
	}

	[ProtoMember(2, Name = "event_type")]
	public uint EventType
	{
		get
		{
			return __pbn__EventType.GetValueOrDefault();
		}
		set
		{
			__pbn__EventType = value;
		}
	}

	[ProtoMember(3, Name = "event_param_list")]
	public uint[] EventParamLists { get; set; }

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEventId()
	{
		return __pbn__EventId.HasValue;
	}

	public void ResetEventId()
	{
		__pbn__EventId = null;
	}

	public bool ShouldSerializeEventType()
	{
		return __pbn__EventType.HasValue;
	}

	public void ResetEventType()
	{
		__pbn__EventType = null;
	}
}
