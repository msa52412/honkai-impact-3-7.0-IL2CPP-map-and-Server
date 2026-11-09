using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessCommonNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5298
	}

	[ProtoContract]
	public enum EventType
	{
		[ProtoEnum(Name = "EVENT_NONE")]
		EventNone,
		[ProtoEnum(Name = "EVENT_IN_SETTLE")]
		EventInSettle
	}

	private IExtension __pbn__extensionData;

	private EventType? __pbn__event_type;

	[ProtoMember(1)]
	[DefaultValue(EventType.EventNone)]
	public EventType event_type
	{
		get
		{
			return __pbn__event_type.GetValueOrDefault();
		}
		set
		{
			__pbn__event_type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeevent_type()
	{
		return __pbn__event_type.HasValue;
	}

	public void Resetevent_type()
	{
		__pbn__event_type = null;
	}
}
