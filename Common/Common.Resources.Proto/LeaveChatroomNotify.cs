using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LeaveChatroomNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2233
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__NewChatroomId;

	[ProtoMember(1, Name = "new_chatroom_id")]
	public uint NewChatroomId
	{
		get
		{
			return __pbn__NewChatroomId.GetValueOrDefault();
		}
		set
		{
			__pbn__NewChatroomId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNewChatroomId()
	{
		return __pbn__NewChatroomId.HasValue;
	}

	public void ResetNewChatroomId()
	{
		__pbn__NewChatroomId = null;
	}
}
