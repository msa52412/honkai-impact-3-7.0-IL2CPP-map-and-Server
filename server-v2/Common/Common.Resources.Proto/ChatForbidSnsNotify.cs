using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatForbidSnsNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1161
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CreditBound;

	private uint? __pbn__ChatForbidFinishTime;

	private uint? __pbn__ChatForbidTimes;

	[ProtoMember(1, Name = "credit_bound")]
	public uint CreditBound
	{
		get
		{
			return __pbn__CreditBound.GetValueOrDefault();
		}
		set
		{
			__pbn__CreditBound = value;
		}
	}

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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCreditBound()
	{
		return __pbn__CreditBound.HasValue;
	}

	public void ResetCreditBound()
	{
		__pbn__CreditBound = null;
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
}
