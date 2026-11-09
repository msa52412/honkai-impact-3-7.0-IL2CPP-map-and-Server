using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RecoverChatCreditNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1162
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LostChatCredit;

	[ProtoMember(1, Name = "lost_chat_credit")]
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

	public bool ShouldSerializeLostChatCredit()
	{
		return __pbn__LostChatCredit.HasValue;
	}

	public void ResetLostChatCredit()
	{
		__pbn__LostChatCredit = null;
	}
}
