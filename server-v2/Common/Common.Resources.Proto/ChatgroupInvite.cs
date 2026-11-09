using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatgroupInvite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatgroupId;

	private string __pbn__Name;

	private string __pbn__Notice;

	private uint? __pbn__IconId;

	private uint? __pbn__InviterUid;

	private uint? __pbn__InviteTime;

	private uint? __pbn__ChatgroupMemberNum;

	[ProtoMember(1, Name = "chatgroup_id")]
	public uint ChatgroupId
	{
		get
		{
			return __pbn__ChatgroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatgroupId = value;
		}
	}

	[ProtoMember(2, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(3, Name = "notice")]
	[DefaultValue("")]
	public string Notice
	{
		get
		{
			return __pbn__Notice ?? "";
		}
		set
		{
			__pbn__Notice = value;
		}
	}

	[ProtoMember(4, Name = "icon_id")]
	public uint IconId
	{
		get
		{
			return __pbn__IconId.GetValueOrDefault();
		}
		set
		{
			__pbn__IconId = value;
		}
	}

	[ProtoMember(5, Name = "inviter_uid")]
	public uint InviterUid
	{
		get
		{
			return __pbn__InviterUid.GetValueOrDefault();
		}
		set
		{
			__pbn__InviterUid = value;
		}
	}

	[ProtoMember(6, Name = "inviter_info")]
	public PlayerFriendBriefData InviterInfo { get; set; }

	[ProtoMember(7, Name = "invite_time")]
	public uint InviteTime
	{
		get
		{
			return __pbn__InviteTime.GetValueOrDefault();
		}
		set
		{
			__pbn__InviteTime = value;
		}
	}

	[ProtoMember(8, Name = "chatgroup_member_num")]
	public uint ChatgroupMemberNum
	{
		get
		{
			return __pbn__ChatgroupMemberNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatgroupMemberNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatgroupId()
	{
		return __pbn__ChatgroupId.HasValue;
	}

	public void ResetChatgroupId()
	{
		__pbn__ChatgroupId = null;
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeNotice()
	{
		return __pbn__Notice != null;
	}

	public void ResetNotice()
	{
		__pbn__Notice = null;
	}

	public bool ShouldSerializeIconId()
	{
		return __pbn__IconId.HasValue;
	}

	public void ResetIconId()
	{
		__pbn__IconId = null;
	}

	public bool ShouldSerializeInviterUid()
	{
		return __pbn__InviterUid.HasValue;
	}

	public void ResetInviterUid()
	{
		__pbn__InviterUid = null;
	}

	public bool ShouldSerializeInviteTime()
	{
		return __pbn__InviteTime.HasValue;
	}

	public void ResetInviteTime()
	{
		__pbn__InviteTime = null;
	}

	public bool ShouldSerializeChatgroupMemberNum()
	{
		return __pbn__ChatgroupMemberNum.HasValue;
	}

	public void ResetChatgroupMemberNum()
	{
		__pbn__ChatgroupMemberNum = null;
	}
}
