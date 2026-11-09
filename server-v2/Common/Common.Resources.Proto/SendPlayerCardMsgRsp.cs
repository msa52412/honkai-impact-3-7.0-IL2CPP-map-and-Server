using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SendPlayerCardMsgRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 493
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SEND_NUM_LIMIT")]
		SendNumLimit,
		[ProtoEnum(Name = "SEND_NUM_LIMIT_PLAYER")]
		SendNumLimitPlayer,
		[ProtoEnum(Name = "MSG_INVALID")]
		MsgInvalid,
		[ProtoEnum(Name = "BLACK_LIST")]
		BlackList,
		[ProtoEnum(Name = "FORBIDDEN")]
		Forbidden,
		[ProtoEnum(Name = "ONLY_FRIEND")]
		OnlyFriend,
		[ProtoEnum(Name = "MSG_CLOSED")]
		MsgClosed,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__MsgIndex;

	private uint? __pbn__ForbiddenTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_uid")]
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

	[ProtoMember(3, Name = "msg_index")]
	public uint MsgIndex
	{
		get
		{
			return __pbn__MsgIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__MsgIndex = value;
		}
	}

	[ProtoMember(4, Name = "forbidden_time")]
	public uint ForbiddenTime
	{
		get
		{
			return __pbn__ForbiddenTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ForbiddenTime = value;
		}
	}

	[ProtoMember(5, Name = "chat_msg")]
	public ChatMsg ChatMsg { get; set; }

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

	public bool ShouldSerializeMsgIndex()
	{
		return __pbn__MsgIndex.HasValue;
	}

	public void ResetMsgIndex()
	{
		__pbn__MsgIndex = null;
	}

	public bool ShouldSerializeForbiddenTime()
	{
		return __pbn__ForbiddenTime.HasValue;
	}

	public void ResetForbiddenTime()
	{
		__pbn__ForbiddenTime = null;
	}
}
