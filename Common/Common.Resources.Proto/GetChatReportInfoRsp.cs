using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetChatReportInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1160
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatForbidFinishTime;

	private uint? __pbn__ChatForbidTimes;

	private uint? __pbn__LostChatCredit;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chat_forbid_finish_time")]
	public uint ChatForbidFinishTime
	{
		get
		{
			return __pbn__ChatForbidFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatForbidFinishTime = value;
		}
	}

	[ProtoMember(3, Name = "chat_forbid_times")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatForbidFinishTime()
	{
		return __pbn__ChatForbidFinishTime.HasValue;
	}

	public void ResetChatForbidFinishTime()
	{
		__pbn__ChatForbidFinishTime = null;
	}

	public bool ShouldSerializeChatForbidTimes()
	{
		return __pbn__ChatForbidTimes.HasValue;
	}

	public void ResetChatForbidTimes()
	{
		__pbn__ChatForbidTimes = null;
	}

	public bool ShouldSerializeLostChatCredit()
	{
		return __pbn__LostChatCredit.HasValue;
	}

	public void ResetLostChatCredit()
	{
		__pbn__LostChatCredit = null;
	}
}
