using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatgroupSystemNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2211
	}

	private IExtension __pbn__extensionData;

	private ChatgroupSystemNotifyType? __pbn__Type;

	private uint? __pbn__ChatgroupId;

	private string __pbn__ChatgroupName;

	private uint? __pbn__Time;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ChatgroupSystemNotifyType.ChatgroupNotifyNone)]
	public ChatgroupSystemNotifyType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "chatgroup_id")]
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

	[ProtoMember(3, Name = "chatgroup")]
	public Chatgroup Chatgroup { get; set; }

	[ProtoMember(4, Name = "join_msg")]
	public ChatgroupJoinMsg JoinMsg { get; set; }

	[ProtoMember(5, Name = "leave_msg")]
	public ChatgroupLeaveMsg LeaveMsg { get; set; }

	[ProtoMember(6, Name = "setting_msg")]
	public ChatgroupSettingMsg SettingMsg { get; set; }

	[ProtoMember(7, Name = "invite")]
	public ChatgroupInvite Invite { get; set; }

	[ProtoMember(8, Name = "target_uid_list")]
	public uint[] TargetUidLists { get; set; }

	[ProtoMember(9, Name = "chatgroup_name")]
	[DefaultValue("")]
	public string ChatgroupName
	{
		get
		{
			return __pbn__ChatgroupName ?? "";
		}
		set
		{
			__pbn__ChatgroupName = value;
		}
	}

	[ProtoMember(10, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeChatgroupId()
	{
		return __pbn__ChatgroupId.HasValue;
	}

	public void ResetChatgroupId()
	{
		__pbn__ChatgroupId = null;
	}

	public bool ShouldSerializeChatgroupName()
	{
		return __pbn__ChatgroupName != null;
	}

	public void ResetChatgroupName()
	{
		__pbn__ChatgroupName = null;
	}

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
	}
}
