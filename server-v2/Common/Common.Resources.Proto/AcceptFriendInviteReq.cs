using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AcceptFriendInviteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 227
	}

	private IExtension __pbn__extensionData;

	private string __pbn__InviteCode;

	private InviteeActivityType? __pbn__InviteType;

	private uint? __pbn__ScheduleId;

	[ProtoMember(1, Name = "invite_code")]
	[DefaultValue("")]
	public string InviteCode
	{
		get
		{
			return __pbn__InviteCode ?? "";
		}
		set
		{
			__pbn__InviteCode = value;
		}
	}

	[ProtoMember(2, Name = "invite_type")]
	[DefaultValue(InviteeActivityType.InviteeActivityTypeNone)]
	public InviteeActivityType InviteType
	{
		get
		{
			return __pbn__InviteType.GetValueOrDefault();
		}
		set
		{
			__pbn__InviteType = value;
		}
	}

	[ProtoMember(3, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeInviteCode()
	{
		return __pbn__InviteCode != null;
	}

	public void ResetInviteCode()
	{
		__pbn__InviteCode = null;
	}

	public bool ShouldSerializeInviteType()
	{
		return __pbn__InviteType.HasValue;
	}

	public void ResetInviteType()
	{
		__pbn__InviteType = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}
}
