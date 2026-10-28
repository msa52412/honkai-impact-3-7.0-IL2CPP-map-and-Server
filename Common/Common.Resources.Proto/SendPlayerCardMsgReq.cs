using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SendPlayerCardMsgReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 486
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private string __pbn__Msg;

	private uint? __pbn__MsgNumLimit;

	private bool? __pbn__IsFriend;

	[ProtoMember(1, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(2, Name = "msg")]
	[DefaultValue("")]
	public string Msg
	{
		get
		{
			return __pbn__Msg ?? "";
		}
		set
		{
			__pbn__Msg = value;
		}
	}

	[ProtoMember(3, Name = "chat_msg")]
	public ChatMsg ChatMsg { get; set; }

	[ProtoMember(4, Name = "msg_num_limit")]
	public uint MsgNumLimit
	{
		get
		{
			return __pbn__MsgNumLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__MsgNumLimit = value;
		}
	}

	[ProtoMember(5, Name = "is_friend")]
	public bool IsFriend
	{
		get
		{
			return __pbn__IsFriend.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFriend = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}

	public bool ShouldSerializeMsgNumLimit()
	{
		return __pbn__MsgNumLimit.HasValue;
	}

	public void ResetMsgNumLimit()
	{
		__pbn__MsgNumLimit = null;
	}

	public bool ShouldSerializeIsFriend()
	{
		return __pbn__IsFriend.HasValue;
	}

	public void ResetIsFriend()
	{
		__pbn__IsFriend = null;
	}
}
