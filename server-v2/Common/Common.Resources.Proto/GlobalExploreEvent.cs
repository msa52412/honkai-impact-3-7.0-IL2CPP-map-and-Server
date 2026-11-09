using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreEvent : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EventId;

	private uint? __pbn__EventType;

	private GlobalExploreHintType? __pbn__HintType;

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

	[ProtoMember(4, Name = "hint_type")]
	[DefaultValue(GlobalExploreHintType.GlobalExploreHintNone)]
	public GlobalExploreHintType HintType
	{
		get
		{
			return __pbn__HintType.GetValueOrDefault();
		}
		set
		{
			__pbn__HintType = value;
		}
	}

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

	public bool ShouldSerializeHintType()
	{
		return __pbn__HintType.HasValue;
	}

	public void ResetHintType()
	{
		__pbn__HintType = null;
	}
}
