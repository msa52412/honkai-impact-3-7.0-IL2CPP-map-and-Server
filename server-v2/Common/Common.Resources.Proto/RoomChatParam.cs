using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RoomChatParam : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__IsBroadcastMsg;

	[ProtoMember(1, Name = "is_broadcast_msg")]
	public bool IsBroadcastMsg
	{
		get
		{
			return __pbn__IsBroadcastMsg.GetValueOrDefault();
		}
		set
		{
			__pbn__IsBroadcastMsg = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsBroadcastMsg()
	{
		return __pbn__IsBroadcastMsg.HasValue;
	}

	public void ResetIsBroadcastMsg()
	{
		__pbn__IsBroadcastMsg = null;
	}
}
