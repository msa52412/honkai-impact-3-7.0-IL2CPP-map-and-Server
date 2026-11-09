using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrantOuterRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4247
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EventType;

	private uint? __pbn__EventId;

	private string __pbn__Ticket;

	[ProtoMember(1, Name = "event_type")]
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

	[ProtoMember(2, Name = "event_id")]
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

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(4, Name = "ticket")]
	[DefaultValue("")]
	public string Ticket
	{
		get
		{
			return __pbn__Ticket ?? "";
		}
		set
		{
			__pbn__Ticket = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEventType()
	{
		return __pbn__EventType.HasValue;
	}

	public void ResetEventType()
	{
		__pbn__EventType = null;
	}

	public bool ShouldSerializeEventId()
	{
		return __pbn__EventId.HasValue;
	}

	public void ResetEventId()
	{
		__pbn__EventId = null;
	}

	public bool ShouldSerializeTicket()
	{
		return __pbn__Ticket != null;
	}

	public void ResetTicket()
	{
		__pbn__Ticket = null;
	}
}
