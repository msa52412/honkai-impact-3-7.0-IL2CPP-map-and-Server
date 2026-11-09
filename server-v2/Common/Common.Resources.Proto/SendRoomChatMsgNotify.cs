using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SendRoomChatMsgNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3163
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsBroadcastMsg;

	private RoomMode? __pbn__RoomMode;

	[ProtoMember(1, Name = "chat_msg")]
	public ChatMsg ChatMsg { get; set; }

	[ProtoMember(2, Name = "is_broadcast_msg")]
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

	[ProtoMember(3, Name = "room_mode")]
	[DefaultValue(RoomMode.RoomModeInvalid)]
	public RoomMode RoomMode
	{
		get
		{
			return __pbn__RoomMode.GetValueOrDefault();
		}
		set
		{
			__pbn__RoomMode = value;
		}
	}

	[ProtoMember(4, Name = "target_uid_list")]
	public uint[] TargetUidLists { get; set; }

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

	public bool ShouldSerializeRoomMode()
	{
		return __pbn__RoomMode.HasValue;
	}

	public void ResetRoomMode()
	{
		__pbn__RoomMode = null;
	}
}
