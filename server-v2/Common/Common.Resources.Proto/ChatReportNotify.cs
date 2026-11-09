using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatReportNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1152
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ReportUid;

	private uint? __pbn__TargetUid;

	private uint? __pbn__ReportType;

	private uint? __pbn__LostChatCredit;

	private uint? __pbn__ChatForbidTimes;

	[ProtoMember(1, Name = "report_uid")]
	public uint ReportUid
	{
		get
		{
			return __pbn__ReportUid.GetValueOrDefault();
		}
		set
		{
			__pbn__ReportUid = value;
		}
	}

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

	[ProtoMember(3, Name = "report_type")]
	public uint ReportType
	{
		get
		{
			return __pbn__ReportType.GetValueOrDefault();
		}
		set
		{
			__pbn__ReportType = value;
		}
	}

	[ProtoMember(4, Name = "lost_chat_credit")]
	public uint LostChatCredit
	{
		get
		{
			return __pbn__LostChatCredit.GetValueOrDefault();
		}
		set
		{
			__pbn__LostChatCredit = value;
		}
	}

	[ProtoMember(5, Name = "chat_forbid_times")]
	public uint ChatForbidTimes
	{
		get
		{
			return __pbn__ChatForbidTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatForbidTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReportUid()
	{
		return __pbn__ReportUid.HasValue;
	}

	public void ResetReportUid()
	{
		__pbn__ReportUid = null;
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeReportType()
	{
		return __pbn__ReportType.HasValue;
	}

	public void ResetReportType()
	{
		__pbn__ReportType = null;
	}

	public bool ShouldSerializeLostChatCredit()
	{
		return __pbn__LostChatCredit.HasValue;
	}

	public void ResetLostChatCredit()
	{
		__pbn__LostChatCredit = null;
	}

	public bool ShouldSerializeChatForbidTimes()
	{
		return __pbn__ChatForbidTimes.HasValue;
	}

	public void ResetChatForbidTimes()
	{
		__pbn__ChatForbidTimes = null;
	}
}
