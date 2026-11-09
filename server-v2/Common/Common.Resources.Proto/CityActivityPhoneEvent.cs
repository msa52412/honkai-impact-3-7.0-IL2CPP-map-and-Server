using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CityActivityPhoneEvent : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RoleId;

	private uint? __pbn__PhoneEventId;

	private PhoneEventStatus? __pbn__Status;

	[ProtoMember(1, Name = "role_id")]
	public uint RoleId
	{
		get
		{
			return __pbn__RoleId.GetValueOrDefault();
		}
		set
		{
			__pbn__RoleId = value;
		}
	}

	[ProtoMember(2, Name = "phone_event_id")]
	public uint PhoneEventId
	{
		get
		{
			return __pbn__PhoneEventId.GetValueOrDefault();
		}
		set
		{
			__pbn__PhoneEventId = value;
		}
	}

	[ProtoMember(3, Name = "status")]
	[DefaultValue(PhoneEventStatus.PhoneEventNone)]
	public PhoneEventStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRoleId()
	{
		return __pbn__RoleId.HasValue;
	}

	public void ResetRoleId()
	{
		__pbn__RoleId = null;
	}

	public bool ShouldSerializePhoneEventId()
	{
		return __pbn__PhoneEventId.HasValue;
	}

	public void ResetPhoneEventId()
	{
		__pbn__PhoneEventId = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}
}
